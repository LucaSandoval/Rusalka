using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float startPos;
    private Camera cam;
    public float parallaxEffect;
    public bool PlayerDependent = false;

    void Start()
    {
        cam = Camera.main;
        startPos = transform.position.x;
    }

    void FixedUpdate()
    {
        float dist;
        if (PlayerDependent) dist = GameObject.FindWithTag("Player").transform.position.x * parallaxEffect;
        else dist = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3((startPos + dist), transform.position.y, transform.position.z);
    }
}
