using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraTarget : MonoBehaviour
{
    public CameraOperator.Follow TypeOfTargeting;
    public Transform DefaultTarget;
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
            CameraOperator.Instance.DynamicTarget = DefaultTarget;
        }
    }    
}