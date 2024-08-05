using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScroll : MonoBehaviour
{
    private bool CanScroll;
    private float scrollSpeed;

    private const float scrollSpeedMin = 90f;
    private const float scrollSpeedMax = 750f;

    void Start()
    {
        CanScroll = false;
        Invoke("StartScroll", 4.5f);
    }

    private void StartScroll()
    {
        CanScroll = true;
    }

    private void Update()
    {
        if (CanScroll)
        {
            transform.position += new Vector3(0, scrollSpeed * Time.deltaTime, 0);
        }

        if (Input.GetButton("Jump"))
        {
            scrollSpeed = scrollSpeedMax;
        } else
        {
            scrollSpeed = scrollSpeedMin;
        }
    }
}
