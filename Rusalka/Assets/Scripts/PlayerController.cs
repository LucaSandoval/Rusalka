using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float MovementSpeed;
    [SerializeField] private float UpGravityForce;
    [SerializeField] private float DownGravityForce;
    [SerializeField] private float FloatGravityForce;
    [SerializeField] private float JumpForce;
    [SerializeField] private float ReleaseSpeed;
    private Vector2 velocity;
    private Rigidbody2D rb;
    private BoxCollider2D collide;
    private bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        velocity = new Vector2(0, 0);
        grounded = false;
        collide = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity.x = Input.GetAxisRaw("Horizontal") * MovementSpeed;
        if (!grounded)
        {
            float currGrav = DownGravityForce;
            if (velocity.y > 0)
            {
                currGrav = UpGravityForce;
            }
            else if (Input.GetButton("Jump")) {
                currGrav = FloatGravityForce;
            }
            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = 0;
            }
            velocity.y -= currGrav;
        }
        else
        {
            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = JumpForce;
            }
            else if(velocity.y < 0){
                velocity.y = 0;
            }
        }

        if (Input.GetButtonUp("Jump") && velocity.y > 0) {
            velocity.y = ReleaseSpeed;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        grounded = Physics2D.OverlapArea(new Vector2(transform.position.x - collide.bounds.extents.x + .01f, transform.position.y - collide.bounds.extents.y), new Vector2(transform.position.x + collide.bounds.extents.x - .01f, transform.position.y - collide.bounds.extents.y - .001f),  LayerMask.GetMask("Floor"));
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        grounded = Physics2D.OverlapArea(new Vector2(transform.position.x - collide.bounds.extents.x + .01f, transform.position.y - collide.bounds.extents.y), new Vector2(transform.position.x + collide.bounds.extents.x - .01f, transform.position.y - collide.bounds.extents.y - .001f), LayerMask.GetMask("Floor"));
    }
}