using System;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private float movingSpeed = 0.1f;

    [SerializeField] private float TravelDistance;
    private Vector3 StartingPos;


    private void Start()
    {
        StartingPos = transform.position;
    }

    void FixedUpdate()
    {
        Vector3 newPosition = transform.position + Vector3.right * movingSpeed;
        transform.position = newPosition;



        if (Vector3.Distance(transform.position, StartingPos) > TravelDistance)
        {
            Destroy(gameObject);
        }
    }
}