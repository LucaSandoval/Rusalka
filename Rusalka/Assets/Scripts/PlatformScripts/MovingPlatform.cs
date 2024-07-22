using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform Player;
    public float moveSpeed = 0.01f; 
    private bool isMoving = false;
    private Vector3 targetPosition;

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
            Debug.Log("Start Moving");
            targetPosition = transform.position + new Vector3(-15, 0, 0);
            isMoving = true;
        }

        if (isMoving)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Check if the platform has reached the target position
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                Debug.Log("Reached Target");
                isMoving = false;
            }
        }
    }
}
