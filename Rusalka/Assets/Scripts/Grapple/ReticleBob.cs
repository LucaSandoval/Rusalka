using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleBob : MonoBehaviour
{
    // Speed of rotation in degrees per second
    public float rotationSpeed = 100f;

    // Scale animation parameters
    public float minScale = 1f;
    public float maxScale = 1.5f;
    public float scaleSpeed = 1f;

    private Vector3 initialScale;
    private bool growing = true;

    void Start()
    {
        // Save the initial scale
        initialScale = transform.localScale;
    }

    void Update()
    {
        // Rotate the reticle
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

        // Scale the reticle up and down
        float scaleChange = scaleSpeed * Time.deltaTime;

        if (growing)
        {
            transform.localScale += Vector3.one * scaleChange;
            if (transform.localScale.x >= maxScale)
            {
                transform.localScale = new Vector3(maxScale, maxScale, initialScale.z);
                growing = false;
            }
        }
        else
        {
            transform.localScale -= Vector3.one * scaleChange;
            if (transform.localScale.x <= minScale)
            {
                transform.localScale = new Vector3(minScale, minScale, initialScale.z);
                growing = true;
            }
        }
    }
}
