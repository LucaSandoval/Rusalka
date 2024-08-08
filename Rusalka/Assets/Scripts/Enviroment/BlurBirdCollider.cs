using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurBirdCollider : MonoBehaviour
{
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        BlurBird.BirdFired?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        BlurBird.BirdExitFired?.Invoke();
    }
}
