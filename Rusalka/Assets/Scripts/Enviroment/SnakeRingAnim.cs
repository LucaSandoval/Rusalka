using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeRingAnim : MonoBehaviour
{

    [SerializeField] private Animator snakeAnim;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            snakeAnim.SetTrigger("Ring");
        }
    }
}
