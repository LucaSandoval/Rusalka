using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurBirdCollider : MonoBehaviour
{

    private bool hasFired = false;
    private void OnTriggerEnter2D(Collider2D other)
    { 
        if(hasFired) return;
        BlurBird.BirdFired?.Invoke();
        hasFired = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        BlurBird.BirdExitFired?.Invoke();
        enabled = false;
        Destroy(gameObject);
    }
}
