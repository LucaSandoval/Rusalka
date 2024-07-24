using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFlippingTwo : MonoBehaviour
{
    public float FlipInterval;
    public bool Flipped;
    private SpriteRenderer Sprite;
    private BoxCollider2D Collider;
    
    // Start is called before the first frame update
    void Start()
    {
        Sprite = GetComponent<SpriteRenderer>();
        Collider = GetComponent<BoxCollider2D>();

        Sprite.enabled = Flipped;
        Collider.enabled = Flipped;

        StartCoroutine(FlipPlatformRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FlipPlatformRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(FlipInterval);
            Flipped = !Flipped;

            // Apply the flipped state to the SpriteRenderer and BoxCollider
            Sprite.enabled = !Flipped;
            Collider.enabled = !Flipped;
        }
    }


}
