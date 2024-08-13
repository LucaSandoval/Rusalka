using System;
using UnityEngine;

public class midpoint : MonoBehaviour
{
    public Transform player;

    private float initialOffsetX;

    //private float distanceTraveled;

    private bool hasFired = false;

    void Start()
    {
        initialOffsetX = transform.position.x - player.position.x;
    }

    void Update()
    {
        if(!hasFired) return;
        float newXPosition = player.position.x + initialOffsetX / 9;
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);
        //distanceTraveled += player.position.x;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("herre");
        hasFired = true;
    }
}
