using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float MovementSpeed; // The horizontal movement speed
    [SerializeField] private float UpGravityForce; // How hard gravity affects the player when they are moving upwards
    [SerializeField] private float DownGravityForce; // How hard gravity affects the player when they are moving downwards
    [SerializeField] private float FloatGravityForce; // How hard gravity affects the player when they are floating
    [SerializeField] private float JumpForce; //How hard the player jumps
    [SerializeField] private float ReleaseSpeed; //The y velocity of the player when they release jump prematurely. Should be a positive number.
    [SerializeField] private float CoyoteTime; // the amount of inair time in seconds the player can still jump

    private int facing; // the direction the player is facing
    private bool grounded; // whether or not the player is standing on the ground
    private float currCoyoteTime; // the amount of inair time in seconds the player has left while they can still jump
    private bool isFloat; //whether or not the player should be floating

    private Vector2 velocity; // the current x and y velocity of the player
    private Rigidbody2D rb; // the player's rigidbody
    private BoxCollider2D collide; //the player's collider
    private SpriteRenderer spr; // the player's sprite
    private bool inGrapple; // Is the player currently in the Grapple

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        velocity = new Vector2(0, 0);
        grounded = false;
        collide = GetComponent<BoxCollider2D>();
        spr = GetComponent<SpriteRenderer>();
        facing = 1;

        //scales values based on the size of the character
        currCoyoteTime = CoyoteTime;
        MovementSpeed *= transform.localScale.x;
        UpGravityForce *= transform.localScale.y;
        DownGravityForce *= transform.localScale.y;
        FloatGravityForce *= transform.localScale.y;
        JumpForce *= transform.localScale.y;
        ReleaseSpeed *= transform.localScale.y;

        inGrapple = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (!inGrapple)
        {
            if ((grounded) || Mathf.Abs(velocity.x) <= MovementSpeed)
            {
                velocity.x = Input.GetAxisRaw("Horizontal") * MovementSpeed;
            }
            else
            {
                if (Input.GetAxisRaw("Horizontal") * velocity.x < 0)
                {
                    velocity.x -= Input.GetAxisRaw("Horizontal") * Time.deltaTime;
                }
                velocity.x -= Time.deltaTime * -Mathf.Sign(velocity.x);
            }
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
        

        if (!grounded)
        {
            float currGrav = DownGravityForce;
            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = 0;
                isFloat = true;
            }
            if (Input.GetButtonUp("Jump"))
            {
                isFloat = false;
            }

            if (velocity.y > 0)
            {
                currGrav = UpGravityForce;
            }
            else if (isFloat)
            {
                currGrav = FloatGravityForce;
            }

            velocity.y -= currGrav * Time.deltaTime;
            currCoyoteTime -= Time.deltaTime;
        }
        else
        {
            isFloat = false;
            currCoyoteTime = CoyoteTime;
            if (velocity.y < 0)
            {
                velocity.y = 0;
            }
        }

        if (Input.GetButtonDown("Jump") && currCoyoteTime >= 0)
        {
            velocity.y = JumpForce;
        }
        else

        if (Input.GetButtonUp("Jump") && velocity.y > 0)
        {
            velocity.y = Mathf.Min(velocity.y, ReleaseSpeed);
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
}