using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraTarget : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public CameraOperator.Follow TypeOfTargeting;
    public Transform Target;
    public Vector3 StaticPoint;
    // Update is called once per frame
    void Update()
    {
        if (CameraOperator.Instance._FollowTarget != TypeOfTargeting){
            switch(TypeOfTargeting){
                case CameraOperator.Follow.Dynamic:
                    CameraOperator.Instance.DynamicTarget = Target;
                    break;
                case CameraOperator.Follow.Static:
                    CameraOperator.Instance.StaticPoint = StaticPoint;
                    break;
                default: 
                    break;
            }
            CameraOperator.Instance._FollowTarget = TypeOfTargeting;
        }
       }
}

