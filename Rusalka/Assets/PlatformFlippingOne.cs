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
        //Debug.Log("Starting");
        //StartCoroutine(FlipPlatformRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && player.IsGrounded())
        {
            Flipped = !Flipped;
            Sprite.enabled = Flipped;
            Collider.enabled = Flipped;
            Debug.Log(player.IsGrounded());
        }
    }

    IEnumerator FlipPlatformRoutine()
    {
        while (true) 
        {
            yield return new WaitForSeconds(FlipInterval); 
            //Flipped = !Flipped;
            //Debug.Log("Flipping");
            //Debug.Log(Flipped);

            // Apply the flipped state to the game object
            Sprite.enabled = Flipped;
            Collider.enabled = Flipped;

        }
    }
}
