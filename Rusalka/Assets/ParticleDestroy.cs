using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroy : MonoBehaviour
{
    void Start()
    {
        ParticleSystem particleSystem = GetComponentInChildren<ParticleSystem>();
        if (particleSystem != null)
        {
            float destroyTime = particleSystem.main.startLifetime.constant;
            Destroy(gameObject, destroyTime);
        }
    }
}
