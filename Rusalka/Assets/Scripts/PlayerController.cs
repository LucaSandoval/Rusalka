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
    [SerializeField] private float MovementSpeed; // The horizontal movement speed
    [SerializeField] private float UpGravityForce; // How hard gravity affects the player when they are moving upwards
    [SerializeField] private float DownGravityForce; // How hard gravity affects the player when they are moving downwards
    [SerializeField] private float FloatGravityForce; // How hard gravity affects the player when they are floating
    [SerializeField] private float JumpForce; //How hard the player jumps
    [SerializeField] private float ReleaseSpeed; //The y velocity of the player when they release jump prematurely. Should be a positive number.
    [SerializeField] private float AirResistance; //Applies when the player is moving very fast.
    [SerializeField] private float CoyoteTime; // the amount of inair time in seconds the player can still jump
    
    [Header("Swim")]
    [SerializeField] private float SwimSpeed; // The horizontal movement speed while swimming
    [SerializeField] private float SwimSink; // How hard gravity affects you in the water
    [SerializeField] private float SwimExitForce; // How hard the player jumps out of water

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
    private bool inGrapple; // Is the player currently in the Grapple
    private bool inWater; // Whether or not the player is currently in the water
    private float currSwimSpeed;
    private bool jumpedInWater; // Whether or not the player has jumped into the water
    private bool canMove;
    private bool canFloat;
    private bool OnSlope;

    public delegate void PlayerJumpEvent();
    public event PlayerJumpEvent OnPlayerJumped;

    // Start is called before the first frame update
    void Start()
    {
        // set base values
        rb = GetComponent<Rigidbody2D>();
        velocity = new Vector2(0, 0);
        grounded = false;
        collide = GetComponent<Collider2D>();
        spr = GetComponent<SpriteRenderer>();
        facing = 1;
        inGrapple = false;
        currFloatGrav = 0;
        currSwimSpeed = 0;

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
        // BASIC MOVEMENT! (ground and air)
        if (!inGrapple && !inWater)
        {
            // Horizontal movement adapts to speed (for grappling purposes)
            if ((grounded || Mathf.Abs(velocity.x) <= MovementSpeed) && canMove)
            {
                // Slope check
                Vector2 slopeNormalPerp = new Vector2();
                Vector2 slopeCheckPos = transform.position - new Vector3(0f, collide.bounds.extents.y);
                if (grounded)
                {
                    RaycastHit2D hit = Physics2D.Raycast(slopeCheckPos, Vector2.down, 0.5f, LayerMask.GetMask("Floor"));
                    if (hit && hit.transform.gameObject.layer == 6)
                    {
                        Debug.DrawRay(hit.point, hit.normal, Color.green);
                        slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;
                        Debug.DrawRay(hit.point, slopeNormalPerp, Color.red);
                        float slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);
                        OnSlope = slopeDownAngle > 0.1f;
                    }
                    else
                    {
                        OnSlope = false;
                        if (!hit)
                        {
                            grounded = false;
                        }
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
                    velocity.y = currFloatGrav;
                    isFloat = true;
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
            }

            // Jump
            if (Input.GetButtonDown("Jump") && currCoyoteTime >= 0 && canMove)
            {
                velocity.y = JumpForce;
                grounded = false;
                OnPlayerJumped?.Invoke();
                OnSlope = false;
                currCoyoteTime = 0;
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
            if (canMove && !jumpedInWater)
            {
                swimx = Input.GetAxisRaw("Horizontal");
                swimy = Input.GetAxisRaw("Vertical");
            }

            if (swimx == 0 && swimy == 0 && !jumpedInWater)
            {
                currSwimSpeed = 0;
                velocity.y -= SwimSink * Time.deltaTime;
            }
            else if (!jumpedInWater){
                currSwimSpeed = Mathf.Min(currSwimSpeed + Time.deltaTime * SwimSpeed, SwimSpeed);
                velocity = new Vector2(swimx, swimy);
                velocity *= currSwimSpeed;
            }
            if (jumpedInWater) {
                velocity.y += SwimSink * 100 * Time.deltaTime;
                if (velocity.y >= 0) {
                    jumpedInWater = false;
                }
            }
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

        if (!inWater) {
            currSwimSpeed = 0;
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

        if (inGrapple) {
            isFloat = false;
        }

        // Grounded check 
        if (velocity.y <= 0)
        {
            float xPadding = 0.001f; //0.01f
            float yPadding = 0.01f;
            grounded = Physics2D.OverlapArea(new Vector2(transform.position.x - collide.bounds.extents.x + xPadding, transform.position.y - collide.bounds.extents.y),
            new Vector2(transform.position.x + collide.bounds.extents.x - xPadding, transform.position.y - collide.bounds.extents.y - yPadding), LayerMask.GetMask("Floor"));
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
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Water") {
            inWater = true;
            if (velocity.y < 0 && !jumpedInWater)
            {
                velocity.x = 0;
                jumpedInWater = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Water")
        {
            if (velocity.y > 0)
            {
                velocity.y = SwimExitForce * (currSwimSpeed / SwimSpeed);
            }
            inWater = false;
            jumpedInWater = false;
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
    
    public float GetHorizontalMovementSpeed()
    {
        return velocity.y;
    }

    public bool GetIsFloat()
    {
        return isFloat;
    }
}