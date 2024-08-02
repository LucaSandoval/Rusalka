using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenBridge : MonoBehaviour
{
    public float TimeToTrigger = 2.5f;
    [SerializeField] private GameObject parent;
    //[SerializeField] private Transform parentTransform;

    [SerializeField] private GameObject[] stones;

    private IBridgeCallable[] interactables;
    
    private bool isActivated = false;
    private float speedWatch = 0.0f;

    private void Start()
    {
        interactables = new IBridgeCallable[stones.Length];
        
        for (int i = 0; i < stones.Length; i++)
        {
            interactables[i] = stones[i].GetComponent<IBridgeCallable>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated)
        {
            speedWatch += Time.deltaTime;
            if (speedWatch >= TimeToTrigger)
            {
                foreach (var interactable in interactables)
                {
                    interactable?.FireBreak();
                }
                CameraOperator.Instance.removeCameraZone(GetComponent<CameraZone>());
                Destroy(parent);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isActivated = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isActivated = false;
            speedWatch = 0.0f;
        }
    }
}
