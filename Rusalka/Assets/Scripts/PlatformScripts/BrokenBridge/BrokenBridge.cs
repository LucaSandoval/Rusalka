using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenBridge : MonoBehaviour
{
    public float TimeToTrigger = 2.5f;
    [SerializeField] private GameObject parent;
    //[SerializeField] private Transform parentTransform;

    private bool isActivated = false;
    private float speedWatch = 0.0f;


    // Update is called once per frame
    void Update()
    {
        if (isActivated)
        {
            speedWatch += Time.deltaTime;
            if (speedWatch >= TimeToTrigger)
            {
                FirePlatformActivation();
                Destroy(parent);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isActivated = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
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
        //parentTransform.position -= new Vector3(parentTransform.position.x, parentTransform.position.y,11.0f);
    }
}
