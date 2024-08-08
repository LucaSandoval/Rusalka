using System;
using UnityEngine;

public class BlurBird : MonoBehaviour
{
    [SerializeField] private float movingSpeed = 0.1f;
    [SerializeField] private SpriteRenderer sprite;
    private Vector3 StartingPos;
    
    public delegate void BirdFireEnvent();
    public static BirdFireEnvent BirdFired;
    
    public delegate void BirdExitFireEnvent();
    public static BirdExitFireEnvent BirdExitFired;
    
    private void Start()
    {
        BirdFired += OnEnter;
        BirdExitFired += OnExit;
        
        StartingPos = transform.position;
        sprite.enabled = false;
        enabled = false;
    }

    void FixedUpdate()
    {
        Vector3 newPosition = transform.position + transform.right * movingSpeed;
        transform.position = newPosition;
    }

    private void OnEnter()
    {
        sprite.enabled = true;
        enabled = true;
    }

    private void OnExit()
    {
        //sprite.enabled = false;
        enabled = false;
        Destroy(gameObject);
    }
}