using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;

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

    private int facing; // the direction the player is facing
    private bool grounded; // whether or not the player is standing on the ground
    private float currCoyoteTime; // the amount of inair time in seconds the player has left while they can still jump
    private bool isFloat; //whether or not the player should be floating

    private Vector2 velocity; // the current x and y velocity of the player
    private Rigidbody2D rb; // the player's rigidbody
    private BoxCollider2D collide; //the player's collider
    private SpriteRenderer spr; // the player's sprite
    private bool inGrapple; // Is the player currently in the Grapple
    private bool inWater; // Whether or not the player is currently in the water

    public delegate void PlayerJumpEvent();
    public event PlayerJumpEvent OnPlayerJumped;

    // Start is called before the first frame update
    void Start()
    {
        // set base values
        rb = GetComponent<Rigidbody2D>();
        velocity = new Vector2(0, 0);
        grounded = false;
        collide = GetComponent<BoxCollider2D>();
        spr = GetComponent<SpriteRenderer>();
        facing = 1;
        inGrapple = false;

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
    }

    // Update is called once per frame
    void Update()
    {
        // BASIC MOVEMENT! (ground and air)
        if (!inGrapple && !inWater)
        {
            // Horizontal movement adapts to speed (for grappling purposes)
            if (grounded || Mathf.Abs(velocity.x) <= MovementSpeed)
            {
                velocity.x = Input.GetAxisRaw("Horizontal") * MovementSpeed;
            }
            else
            {
                if (Input.GetAxisRaw("Horizontal") * velocity.x < 0)
                {
                    velocity.x -= Time.deltaTime * AirResistance * Mathf.Sign(velocity.x);
                }
                velocity.x -= Time.deltaTime * AirResistance * Mathf.Sign(velocity.x);
            }

            // Facing direction
            if (velocity.x != 0)
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

            // JUMP/GRAVITY CODE
            if (!grounded)
            {
                float currGrav = DownGravityForce;
                // Setting the float
                if (Input.GetButtonDown("Jump"))
                {
                    velocity.y = 0;
                    isFloat = true;
                }
                // Unsetting the float
                if (Input.GetButtonUp("Jump"))
                {
                    isFloat = false;
                }

                // Changes gravity force
                if (velocity.y > 0)
                {
                    currGrav = UpGravityForce;
                }
                else if (isFloat)
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
                currCoyoteTime = CoyoteTime;
                if (velocity.y < 0)
                {
                    velocity.y = 0;
                }
            }

            // Jump
            if (Input.GetButtonDown("Jump") && currCoyoteTime >= 0)
            {
                velocity.y = JumpForce;
                OnPlayerJumped?.Invoke();
            }
            else

            if (Input.GetButtonUp("Jump") && velocity.y > 0)
            {
                velocity.y = Mathf.Min(velocity.y, ReleaseSpeed);
            }
        }
        // SWIMMING CODE
        else if (inWater && !inGrapple) {
            isFloat = false;
            velocity.x = Input.GetAxis("Horizontal");
            velocity.y = Input.GetAxis("Vertical");
            velocity.Normalize();
            velocity *= SwimSpeed;
            if(velocity.x == 0 && velocity.y == 0) {
                velocity.y -= SwimSink * Time.deltaTime;
            }
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        grounded = Physics2D.OverlapArea(new Vector2(transform.position.x - collide.bounds.extents.x + .01f, transform.position.y - collide.bounds.extents.y), new Vector2(transform.position.x + collide.bounds.extents.x - .01f, transform.position.y - collide.bounds.extents.y - .001f),  LayerMask.GetMask("Floor"));
        if (grounded)
        {
            inGrapple = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        grounded = Physics2D.OverlapArea(new Vector2(transform.position.x - collide.bounds.extents.x + .01f, transform.position.y - collide.bounds.extents.y), new Vector2(transform.position.x + collide.bounds.extents.x - .01f, transform.position.y - collide.bounds.extents.y - .001f), LayerMask.GetMask("Floor"));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Water") {
            inWater = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Water")
        {
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
}