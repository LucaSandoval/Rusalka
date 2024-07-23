using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    public float Rotation;
    public Transform Player;
    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.position);
        Debug.Log(distance);

        if (distance < 2.0f && !isMoving)
        {
            Debug.Log("Start Rotate");
            transform.Rotate(Rotation, 0, 0, Space.Self);
            isMoving = true;
        }
    }

}
