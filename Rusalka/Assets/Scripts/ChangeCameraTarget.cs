using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// IMPORTANT : Set DefaultTarget to Player object
/// </summary>
public class ChangeCameraTarget : MonoBehaviour
{
    public CameraOperator.Follow TypeOfTargeting;
    public Transform Target; 
    public Vector3 _StaticPoint;

    public void OnTriggerEnter2D(Collider2D collider) {
        if(collider.tag == "Player" && CameraOperator.Instance != null){
            switch(TypeOfTargeting){
                case CameraOperator.Follow.Dynamic:
                    CameraOperator.Instance.DynamicTarget = Target;
                    break;
                case CameraOperator.Follow.Static:
                    CameraOperator.Instance.StaticPoint = _StaticPoint;
                    break;
                default: 
                    break;    
            }
            CameraOperator.Instance._FollowTarget = TypeOfTargeting;
        }
    }
    public void OnTriggerExit2D(Collider2D collider) {
        if(collider.tag == "Player" && CameraOperator.Instance != null){
            CameraOperator.Instance._FollowTarget = CameraOperator.Follow.Dynamic;
            if (GameObject.FindWithTag("Player") != null) 
                CameraOperator.Instance.DynamicTarget = GameObject.FindWithTag("Player").transform;
        }
    }    
}