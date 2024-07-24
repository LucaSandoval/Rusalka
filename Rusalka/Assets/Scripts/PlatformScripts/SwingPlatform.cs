using System;
using UnityEngine;

public class SwingPlatform : MonoBehaviour
{
    private float standingTime;
    private bool isPlayerOnThePlatform;

    public float TimeToTrigger = 5.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerOnThePlatform)
        {
            standingTime += Time.deltaTime;
            if (standingTime >= TimeToTrigger)
            {
                SwingThePlatform();
                enabled = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerOnThePlatform = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerOnThePlatform = false;
        }
    }

    private void SwingThePlatform()
    {
        Debug.Log("SwingPlatform::SwingThePlatform() the platform has been triggered");
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y,
            transform.eulerAngles.z - (35.0f * Mathf.Rad2Deg));
    }
}
