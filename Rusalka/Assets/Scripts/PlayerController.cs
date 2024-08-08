using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;
using System;

public class PlayerController : MonoBehaviour
{
    [Header("Basic")]
    [SerializeField] private float MaxClimbableSlopeAngle;
    [SerializeField] private float MovementSpeed; // The horizontal movement speed
    [SerializeField] private float UpGravityForce; // How hard gravity affects the player when they are moving upwards
    [SerializeField] private float DownGravityForce; // How hard gravity affects the player when they are moving downwards
    [SerializeField] private float FloatGravityForce; // How hard gravity affects the player when they are floating
    [SerializeField] private float JumpForce; //How hard the player jumps
    [SerializeField] private float ReleaseSpeed; //The y velocity of the player when they release jump prematurely. Should be a positive number.
    [SerializeField] private float AirResistance; //Applies when the player is moving very fast.
    [SerializeField] private float CoyoteTime; // the amount of inair time in seconds the player can still jump
    
    [Header("Swim")]
    [SerializeField] private float SwimSpeed; // The top movement speed while swimming
    [SerializeField] private float SwimSink; // How hard gravity affects you in the water
    [SerializeField] private float SwimExitForce; // How hard the player jumps out of water
    [SerializeField] private float SwimAcceleration; // how quickly the player accelerates to top speed underwater
    [SerializeField] private float SwimDecceleration; // how quickly the player decelerates to a stop underwater

    [Header("Physics Materials")]
    public PhysicsMaterial2D NoFriction;
    public PhysicsMaterial2D HighFriction;

    private int facing; // the direction the player is facing
    private bool grounded; // whether or not the player is standing on the ground
    private float currCoyoteTime; // the amount of inair time in seconds the player has left while they can still jump
    private bool isFloat; //whether or not the player should be floating
    private float currFloatGrav; // the last float gravity saved
    private bool hasJumped; // whether or not the player has jumped yet

    private Vector2 velocity; // the current x and y velocity of the player
    private Rigidbody2D rb; // the player's rigidbody
    private Collider2D collide; //the player's collider
    private SpriteRenderer spr; // the player's sprite
    private PlayerFootstepController footstepController;
    private bool inGrapple; // Is the player currently in the Grapple
    private bool inWater; // Whether or not the player is currently in the water
    private Vector2 currSwimSpeed;
    private bool canMove;
    private bool canFloat;
    private bool OnSlope;
    private Vector2 normal;

    public delegate void PlayerJumpEvent();
    public event PlayerJumpEvent OnPlayerJumped;

    private float xCheckPadding = 0.001f; //0.01f
    private float yCheckPadding = 0.09f;

    // Start is called before the first frame update
    void Start()
    {
        // set base values
        rb = GetComponent<Rigidbody2D>();
        velocity = new Vector2(0, 0);
        grounded = false;
        collide = GetComponent<Collider2D>();
        spr = GetComponent<SpriteRenderer>();
        footstepController = GetComponent<PlayerFootstepController>();
        facing = 1;
        inGrapple = false;
        currFloatGrav = 0;
        currSwimSpeed = Vector2.zero;

        // scales values based on the size of the character
        currCoyoteTime = CoyoteTime;
        MovementSpeed *= transform.localScale.x;
        UpGravityForce *= transform.localScale.y;
        DownGravityForce *= transform.localScale.y;
        FloatGravityForce *= transform.localScale.y;
        JumpForce *= transform.localScale.y;
        ReleaseSpeed *= transform.localScale.y;
        AirResistance *= transform.localScale.y;
        SwimSpeed *= transform.localScale.x;
        SwimSink *= transform.localScale.y;
        SwimExitForce *= transform.localScale.y;
        canMove = true;
        canFloat = true;
        hasJumped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameIsNotPaused())
        {
            DoMovement();
        }
    }

    private bool GameIsNotPaused()
    {
        return (PauseController.Instance == null) ||
            PauseController.Instance != null && !PauseController.Instance.IsGamePaused();
    }

    // Performs a slope raycast at a certain point, returning info about the hit (if it connected, is a slope, and the perp. vector)
    private Tuple<Tuple<bool, bool>, Vector2> PerformSlopeCast(Vector2 origin, float distance)
    {
        bool HitAnything = false;
        bool HitOnSlope = false;
        Vector2 slopeNormalPerp = new Vector2();
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, distance, LayerMask.GetMask("Floor"));
        if (hit && hit.transform.gameObject.layer == 6)
        {
            Debug.DrawRay(hit.point, hit.normal, Color.green);
            slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;
            Debug.DrawRay(hit.point, slopeNormalPerp, Color.red);
            float slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);
            HitOnSlope = slopeDownAngle > 0.1f;
            HitAnything = slopeDownAngle <= MaxClimbableSlopeAngle;
        }
        normal = slopeNormalPerp;
        return new Tuple<Tuple<bool, bool>, Vector2>(new Tuple<bool, bool>(HitAnything, HitOnSlope), slopeNormalPerp);
    }

    private void DoMovement()
    {
        // BASIC MOVEMENT! (ground and air)
        if (!inGrapple && !inWater)
        {
            // Horizontal movement adapts to speed (for grappling purposes)
            if ((grounded || Mathf.Abs(velocity.x) <= MovementSpeed) && canMove)
            {
                // Slope check
                Vector2 slopeNormalPerp = new Vector2();
                float slopeCheckDistance = 0.1f;
                Vector2 leftFootCast = transform.position - new Vector3(collide.bounds.extents.x + xCheckPadding, collide.bounds.extents.y);
                Vector2 rightFootCast = transform.position + new Vector3(collide.bounds.extents.x - xCheckPadding, -collide.bounds.extents.y);
                if (grounded)
                {
                    // Left foot cast
                    Tuple<Tuple<bool, bool>, Vector2> leftFootResult = PerformSlopeCast(leftFootCast, slopeCheckDistance);
                    // Right foot cast
                    Tuple<Tuple<bool, bool>, Vector2> rightFootResult = PerformSlopeCast(rightFootCast, slopeCheckDistance);

                    if (leftFootResult.Item1.Item1)
                    {

                        OnSlope = leftFootResult.Item1.Item2;
                        slopeNormalPerp = leftFootResult.Item2;
                    }
                    else if (rightFootResult.Item1.Item1)
                    {
                        OnSlope = rightFootResult.Item1.Item2;
                        slopeNormalPerp = rightFootResult.Item2;
                    }
                    else
                    {
                        grounded = false;
                        OnSlope = false;
                    }
                }
                else
                {
                    OnSlope = false;
                }

                if (OnSlope)
                {
                    velocity.x = -slopeNormalPerp.x * MovementSpeed * Input.GetAxisRaw("Horizontal");
                    velocity.y = -slopeNormalPerp.y * MovementSpeed * Input.GetAxisRaw("Horizontal");
                }
                else
                {
                    velocity.x = Input.GetAxisRaw("Horizontal") * MovementSpeed;
                }
            }
            else
            {
                if (Input.GetAxisRaw("Horizontal") * velocity.x < 0 && canMove)
                {
                    velocity.x -= Time.deltaTime * AirResistance * Mathf.Sign(velocity.x);
                }
                velocity.x -= Time.deltaTime * AirResistance * Mathf.Sign(velocity.x);
            }

            // JUMP/GRAVITY CODE
            if (!grounded)
            {
                float currGrav = DownGravityForce;
                // Setting the float
                if (Input.GetButtonDown("Jump") && canMove && canFloat && currCoyoteTime <= 0)
                {
                    if (velocity.y <= 0)
                    {
                        velocity.y = currFloatGrav;
                    };
                    isFloat = true;
                    SoundController.Instance?.PlaySoundRandomPitch("StartFloat", 0.05f);
                }
                // Unsetting the float
                if (Input.GetButtonUp("Jump") && canMove && canFloat)
                {
                    currFloatGrav = Mathf.Min(0, velocity.y);
                    isFloat = false;
                }

                // Changes gravity force
                if (velocity.y > 0)
                {
                    currGrav = UpGravityForce;
                }
                else if (isFloat && canFloat && canMove)
                {
                    currGrav = FloatGravityForce;
                }

                // Subtracts gravity from Y velocity
                velocity.y -= currGrav * Time.deltaTime;

                // Subtracts time from coyote time
                currCoyoteTime -= Time.deltaTime;
            }
            else
            {
                // Resets if grounded
                isFloat = false;
                hasJumped = false;
                currFloatGrav = 0;
                currCoyoteTime = CoyoteTime;
                if (!OnSlope && velocity.y <= 0)
                {
                    velocity.y = 0;
                }
            }

            // Jump
            if (Input.GetButtonDown("Jump") && currCoyoteTime >= 0 && canMove)
            {
                velocity.y = JumpForce;
                grounded = false;
                OnPlayerJumped?.Invoke();
                OnSlope = false;
                currCoyoteTime = 0;
                SoundController.Instance?.PlaySoundRandomPitch("Jump", 0.05f);
            }
            else

            if (Input.GetButtonUp("Jump") && velocity.y > 0 && canMove && !hasJumped)
            {
                hasJumped = true;
                velocity.y = Mathf.Min(velocity.y, ReleaseSpeed);
            }
        }
        // SWIMMING CODE
        else if (inWater && !inGrapple)
        {
            currFloatGrav = 0;
            isFloat = false;
            float swimx = 0;
            float swimy = 0;
            // Gather input
            if (canMove)
            {
                swimx = Input.GetAxisRaw("Horizontal");
                swimy = Input.GetAxisRaw("Vertical");
            }

            float xAccel = 0;
            float yAccel = 0;
            // Seperate accelerate & decelerate on both the x and y axis 
            if (Mathf.Abs(swimx) > 0)
            {
                xAccel = swimx * Time.deltaTime * SwimAcceleration;
            }
            else
            {
                xAccel = (currSwimSpeed.x > 0) ? -(Time.deltaTime * SwimDecceleration) : (Time.deltaTime * SwimDecceleration);
            }
            if (Mathf.Abs(swimy) > 0)
            {
                yAccel = swimy * Time.deltaTime * SwimAcceleration;
            }
            else
            {
                yAccel = (currSwimSpeed.y > 0) ? -(Time.deltaTime * SwimDecceleration) : (Time.deltaTime * SwimDecceleration);
            }

            currSwimSpeed += new Vector2(xAccel, yAccel);

            // Sink
            float currentSinkFactor = 0;
            currentSinkFactor = Mathf.Lerp(0, -SwimSink, Mathf.InverseLerp(1f, 0, currSwimSpeed.y));
            Vector2 sinkVector = new Vector2(0, currentSinkFactor);

            // Clamp swim speed to max
            currSwimSpeed = new Vector2(Mathf.Clamp(currSwimSpeed.x, -SwimSpeed, SwimSpeed), Mathf.Clamp(currSwimSpeed.y, -SwimSpeed, SwimSpeed));
            // Apply velocity
            velocity = currSwimSpeed + sinkVector;
        }
        else currFloatGrav = 0;
        // Facing direction
        if (Math.Abs(velocity.x) >= 0.5f)
        {
            facing = (int)Mathf.Sign(velocity.x);
            if (facing == 1)
            {
                spr.flipX = false;
            }
            else if (facing == -1)
            {
                spr.flipX = true;
            }
        }

        if (!inWater)
        {
            currSwimSpeed = Vector2.zero;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        // Physics Material
        if ((Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 0) && grounded)
        {
            rb.sharedMaterial = HighFriction;
        }
        else
        {
            rb.sharedMaterial = NoFriction;
        }

        if (inGrapple)
        {
            isFloat = false;
        }

        // Grounded check 
        if (inWater == false)
        {
            if (velocity.y <= 0)
            {
                bool oldGrounded = grounded;
                bool newGrounded = Physics2D.OverlapArea(new Vector2(transform.position.x - collide.bounds.extents.x, transform.position.y - collide.bounds.extents.y),
                new Vector2(transform.position.x + collide.bounds.extents.x, transform.position.y - collide.bounds.extents.y - yCheckPadding), LayerMask.GetMask("Floor"));
                grounded = newGrounded;
                if (!oldGrounded && newGrounded)
                {
                    footstepController?.PlayerLand();
                }

            }
        }
        else
        {
            grounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            // Detect vertical bonk
            if (collision.gameObject.GetComponent<PlatformEffector2D>() == null)
            {
                if (contact.normal.y <= -0.5f)
                {
                    velocity.y = 0;
                    break;
                }
            }

            // Detect horizontal bonk
            if (Mathf.Abs(contact.normal.x) >= 0.5f)
            {
                velocity.x = 0;
                break;
            }
        }
    }

    private void FixedUpdate()
    {
        if (GameIsNotPaused())
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Water") {
            inWater = true;
            if (velocity.y < 0)
            {
                // Give an entry force into water
                currSwimSpeed.x = velocity.x * 2.5f;
                currSwimSpeed.y = -JumpForce;
                velocity = currSwimSpeed;
                SoundController.Instance?.PlaySoundRandomPitch("DiveInWater", 0.05f);
            }
        }

        if (collision.tag == "Water Ceiling")
        {
            inWater = true;
            if (velocity.y > 0)
            {
                // Give an entry force into water
                currSwimSpeed.x = velocity.x * 2.5f;
                currSwimSpeed.y = JumpForce;
                velocity = currSwimSpeed;
                SoundController.Instance?.PlaySoundRandomPitch("DiveInWater", 0.05f);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Water")
        {
            velocity.y = Mathf.Abs(SwimExitForce * (currSwimSpeed.y / SwimSpeed));
            // Min exit force to smooth out water to normal transitions
            if (velocity.y <= JumpForce)
            {
                velocity.y = JumpForce;
            }

            SoundController.Instance?.PlaySoundRandomPitch("SurfaceFromWater", 0.05f);
            inWater = false;
        }

        if (collision.tag == "Water Ceiling")
        {
            SoundController.Instance?.PlaySoundRandomPitch("SurfaceFromWater", 0.05f);
            inWater = false;
        }
    }

    // Returns 1 when facing right and -1 when facing left
    public Vector2 Facing()
    {
        return Vector2.right * facing;
    }

    // Returns true when grounded
    public bool IsGrounded() {
        return grounded;
    }

    // Returns true when grounded
    public void SetGrounded(bool grounded)
    {
        this.grounded = grounded;
    }

    // Set the velocity of the player
    public void SetVelocity(Vector2 velocity) {
        this.velocity = velocity;
    }

    // Set the velocity of the player and whether or not the velocity came from a grapple
    public void SetVelocity(Vector2 velocity, bool inGrapple)
    {
        this.velocity = velocity;
        this.inGrapple = inGrapple;
    }

    // Set the value for inGrapple
    public void SetInGrapple(bool inGrapple)
    {
        this.inGrapple = inGrapple;
    }

    // Set the value for inGrapple
    public void SetInWater(bool inWater)
    {
        this.inWater = inWater;
    }

    // Get the value for inWater
    public bool IsInWater()
    {
        return inWater;
    }
    public void SetCanMove(bool canMove) {
        this.canMove = canMove;
    }
    public void SetCanFloat(bool canFloat)
    {
        this.canFloat = canFloat;
    }

    //Get the value of character speed
    public float GetMovementSpeed()
    {
        return Mathf.Abs(velocity.x);
    }
    
    public Vector2 GetMovementVelocity()
    {
        return velocity;
    }

    public bool GetIsFloat()
    {
        return isFloat;
    }

    public float GetSwimmingMovementVelocity()
    {
        return velocity.magnitude;
    }
    
    public float GetMaxSwimmingSpeed()
    {
        return SwimSpeed;
    }

    public bool GetInGrapple()
    {
        return inGrapple;
    }

    public Vector2 getSlopeNormal() {
        return normal;
    }
}