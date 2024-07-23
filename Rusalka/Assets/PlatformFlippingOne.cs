using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFlippingOne : MonoBehaviour
{

    public float FlipInterval;
    private SpriteRenderer Sprite;
    private BoxCollider2D Collider;
    public bool Flipped;

    public PlayerController player;


    // Start is called before the first frame update
    void Start()
    {
        Sprite = GetComponent<SpriteRenderer>();
        Collider = GetComponent<BoxCollider2D>();

        Sprite.enabled = Flipped;
        Collider.enabled = Flipped;
        //Debug.Log("Starting");
        player.OnPlayerJumped += Flip;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && player.IsGrounded())
        {
            
            Debug.Log(player.IsGrounded());
            
        }
    }

    private void Flip()
    {
        Flipped = !Flipped;
        Sprite.enabled = Flipped;
        Collider.enabled = Flipped;
    }


    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    grounded = true;
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    grounded = false;
    //}
}
