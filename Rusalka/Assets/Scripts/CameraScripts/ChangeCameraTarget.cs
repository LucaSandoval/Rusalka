using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// IMPORTANT : Set DefaultTarget to Player object
/// </summary>
public class ChangeCameraTarget : MonoBehaviour
{
    [SerializeField] private CameraOperator.Follow TypeOfTargeting;
    [SerializeField] private Transform Target; 
    [SerializeField] private Vector3 _StaticPoint;

    public void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("Player") && CameraOperator.Instance != null){
            switch(TypeOfTargeting){
                case CameraOperator.Follow.Dynamic:
                    CameraOperator.Instance.SetDynamicTarget(Target);
                    break;
                case CameraOperator.Follow.Static:
                    CameraOperator.Instance.SetStaticPoint(_StaticPoint);
                    break;
                default: 
                    break;    
            }
            CameraOperator.Instance.Set_FollowTarget(TypeOfTargeting);
        }
    }
    public void OnTriggerExit2D(Collider2D collider) {
        if(collider.CompareTag("Player") && CameraOperator.Instance != null){
            CameraOperator.Instance.Set_FollowTarget(CameraOperator.Follow.Dynamic);
            if (GameObject.FindWithTag("Player") != null) 
                CameraOperator.Instance.SetDynamicTarget(GameObject.FindWithTag("Player").transform);
        }
    }    
}