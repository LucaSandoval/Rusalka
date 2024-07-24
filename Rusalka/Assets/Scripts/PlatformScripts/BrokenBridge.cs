using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenBridge : MonoBehaviour
{
    public float TimeToTrigger = 2.5f;

    private bool isActivated = false;
    private float speedWatch = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated)
        {
            speedWatch += Time.deltaTime;
            if (speedWatch >= TimeToTrigger)
            {
                FirePlatformActivation();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isActivated = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isActivated = false;
            speedWatch = 0.0f;
        }
    }

    private void FirePlatformActivation()
    {
        Debug.Log("BrokenBridge::FirePlatformActivation() the platform has been triggered");
        transform.position -= new Vector3(transform.position.x, transform.position.y,11.0f);
    }
}
