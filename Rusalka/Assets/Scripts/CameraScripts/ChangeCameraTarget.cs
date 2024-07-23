using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Needs a collider to detect Trigger
/// Used to change what the camera is focusing on (either a differnent gameObject or a static point)
/// </summary>
public class ChangeCameraTarget : MonoBehaviour
{
    [SerializeField] private CameraOperator.Target typeOfTargeting;
    [SerializeField] private Transform target; 
    [SerializeField] private Vector3 staticPoint;

    public void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("Player") && CameraOperator.Instance != null){
            switch(typeOfTargeting){
                case CameraOperator.Target.Dynamic:
                    CameraOperator.Instance.SetDynamicTarget(target);
                    break;
                case CameraOperator.Target.Static:
                    CameraOperator.Instance.SetStaticPoint(staticPoint);
                    break;
                default: 
                    break;    
            }
            CameraOperator.Instance.SetCameraTarget(typeOfTargeting);
        }
    }
    public void OnTriggerExit2D(Collider2D collider) {
        if(collider.CompareTag("Player") && CameraOperator.Instance != null){
            CameraOperator.Instance.SetCameraTarget(CameraOperator.Target.Dynamic);
            if (GameObject.FindWithTag("Player") != null) 
                CameraOperator.Instance.SetDynamicTarget(GameObject.FindWithTag("Player").transform);
        }
    }    
}