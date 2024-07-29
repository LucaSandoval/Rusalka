using System;
using UnityEngine;

public class SwingPlatform : MonoBehaviour
{
    public Transform ParentPlatform;
    public float multiplier = 1.0f;

    private float maxSwing = 15.0f;

    private bool collided = false;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") )
        {
            collided = true;
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") )
        {
            collided = false;
        }
    }

    public void Update()
    {
        if (collided)
        {
            Debug.Log("he" + multiplier + ParentPlatform.localRotation.z);
            //if (Mathf.Abs(ParentPlatform.rotation.z) < maxSwing){
                ParentPlatform.localRotation = Quaternion.Euler(0, 0, Mathf.LerpAngle(ParentPlatform.localRotation.z, maxSwing * multiplier, Time.deltaTime * multiplier));
            //}
        }
    }
}
