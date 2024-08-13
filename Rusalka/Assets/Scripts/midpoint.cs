using System;
using UnityEngine;

public class midpoint : MonoBehaviour
{
    [SerializeField] private float convergenceSpeed = 0.1f;

    [SerializeField] private Vector3 plusTargetLocation;
    
    private bool hasFired = false;

    void Update()
    {
        if(!hasFired || Input.GetAxisRaw("Horizontal") == 0) return;
        transform.position = Vector3.Lerp(transform.position, transform.position + plusTargetLocation, convergenceSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        hasFired = true;
    }
}
