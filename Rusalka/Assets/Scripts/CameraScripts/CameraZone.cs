using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZone : MonoBehaviour
{
    private Transform player;
    void Start(){
        player = GameObject.FindWithTag("Player").transform;
        staticPoint.z = -10f;
    }
    [SerializeField] private bool changeCameraMode = false;
    [SerializeField] private bool changeCameraSize = false;
    [SerializeField] private bool changeCameraTarget = false;
    [SerializeField] private bool changeCameraOffset = false;
    [SerializeField] private bool lockXAxis = false;
    [SerializeField] private bool lockYAxis = false;
    [SerializeField] private bool enableCameraShake = false;
    [SerializeField] private bool changeCameraSpeed = false;
    [SerializeField] private bool changeZoomSpeed = false;
    [SerializeField] private float newSize;
    private float ogSize;
    private float ogShakeStrength;
    private bool ogXShake;
    private bool ogYShake;
    private float ogXOffset;
    private float ogYOffset;
    private float ogCamSpeed;
    private float ogZoomSpeed;
    public void ChangeCameraSpeed(){
        ogCamSpeed = CameraOperator.Instance.GetCameraSpeed();
        CameraOperator.Instance.SetCameraSpeed(camSpeed);
    }
    public void ChangeZoomSpeed(){
        ogZoomSpeed = CameraOperator.Instance.GetZoomSpeed();
        CameraOperator.Instance.SetZoomSpeed(zoomSpeed);
    }
    public void ChangeCameraMode(){
        CameraOperator.Instance.SetCameraMode(mode);
            
    }
    public void ChangeCameraSize(){
        switch(mode){
            case CameraOperator.CameraMode.Normal:
                ogSize = CameraOperator.Instance.GetNormalSize();
                CameraOperator.Instance.SetNormalSize(newSize);
                break;
            case CameraOperator.CameraMode.VistaPoint:
                ogSize = CameraOperator.Instance.GetVistaSize();
                CameraOperator.Instance.SetVistaSize(newSize);
                break;
            default:
                break;
        }
    }
    public void ChangeCameraTarget(){
        switch(camTarget){
                case CameraOperator.Target.Dynamic:
                    CameraOperator.Instance.SetDynamicTarget(dynamicTarget);
                    break;
                case CameraOperator.Target.Static:
                    CameraOperator.Instance.SetStaticPoint(staticPoint);
                    break;
                default: 
                    break;    
            }
            CameraOperator.Instance.SetCameraTarget(camTarget);
    }
    public void ChangeCameraShake(){
        CameraOperator.Instance.SetIsShaking(true);
        ogXShake = CameraOperator.Instance.GetXAxisShakeEnabled();
        CameraOperator.Instance.SetXAxisShakeEnabled(xAxisShake);
        ogYShake = CameraOperator.Instance.GetYAxisShakeEnabled();
        CameraOperator.Instance.SetYAxisShakeEnabled(yAxisShake);
        ogShakeStrength = CameraOperator.Instance.GetShakeStrength();
        CameraOperator.Instance.SetShakeStrength(shakeStrength);
    }
    public void ChangeCameraOffset(){
        ogXOffset = CameraOperator.Instance.GetXAxisOffset();
        CameraOperator.Instance.SetXAxisOffset(xAxisOffset);
        ogYOffset = CameraOperator.Instance.GetYAxisOffset();
        CameraOperator.Instance.SetYAxisOffset(yAxisOffset);
    }
    public void ChangeCameraLock(){
        if (lockXAxis) CameraOperator.Instance.SetXAxisMoveEnabled(false);
        if (lockYAxis) CameraOperator.Instance.SetYAxisMoveEnabled(false);
    }
    public void OnTriggerEnter2D(Collider2D collider){
       if(collider.CompareTag("Player") && CameraOperator.Instance != null)
       {
            if(changeCameraMode) ChangeCameraMode();
            if(changeCameraSize) ChangeCameraSize();
            if(changeCameraTarget) ChangeCameraTarget();
            if(enableCameraShake) ChangeCameraShake();
            if(changeCameraOffset) ChangeCameraOffset();
            if(lockXAxis || lockYAxis) ChangeCameraLock();
            if(changeCameraSpeed) ChangeCameraSpeed();
            if(changeZoomSpeed) ChangeZoomSpeed();
       }
    }
    public void ResetCameraShake(){
        CameraOperator.Instance.SetShakeStrength(ogShakeStrength);
        CameraOperator.Instance.SetXAxisShakeEnabled(ogXShake);
        CameraOperator.Instance.SetYAxisShakeEnabled(ogYShake);
        CameraOperator.Instance.SetIsShaking(false);
    }   
    public void ResetCameraSize(){
        switch(mode){
            case CameraOperator.CameraMode.Normal:
                CameraOperator.Instance.SetNormalSize(ogSize);
                break;
            case CameraOperator.CameraMode.VistaPoint:
                CameraOperator.Instance.SetVistaSize(ogSize);
                break;
            default:
                break;
        }        
    }
    public void ResetCameraMode(){

        CameraOperator.Instance.SetCameraMode(CameraOperator.CameraMode.Normal);
    }
    public void ResetCameraLock(){
        if (lockXAxis) CameraOperator.Instance.SetXAxisMoveEnabled(true);
        if (lockYAxis) CameraOperator.Instance.SetYAxisMoveEnabled(true);
    }
    public void ResetCameraOffset(){
        CameraOperator.Instance.SetXAxisOffset(ogXOffset);
        CameraOperator.Instance.SetYAxisOffset(ogYOffset);
    }
    public void ResetCameraTarget(){
        CameraOperator.Instance.SetCameraTarget(CameraOperator.Target.Dynamic);
        CameraOperator.Instance.SetDynamicTarget(player);
    }
    public void ResetCameraSpeed(){
        CameraOperator.Instance.SetCameraSpeed(ogCamSpeed);
    }
    public void ResetZoomSpeed(){
        CameraOperator.Instance.SetZoomSpeed(ogZoomSpeed);
    }
    public void OnTriggerExit2D(Collider2D collider){
        if(collider.CompareTag("Player") && CameraOperator.Instance != null)
        {
            if(changeCameraMode) ResetCameraMode();
            if(changeCameraSize) ResetCameraSize();            
            if(changeCameraTarget) ResetCameraTarget();
            if(enableCameraShake) ResetCameraShake();
            if(changeCameraOffset) ResetCameraOffset();
            if(lockXAxis || lockYAxis) ResetCameraLock();
            if(changeCameraSpeed) ResetCameraSpeed();
            if(changeZoomSpeed) ResetZoomSpeed();
        }
    }
    [SerializeField] private float camSpeed;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private Transform dynamicTarget;
    [SerializeField] private Vector3 staticPoint;
    [SerializeField] private CameraOperator.Target camTarget;
    [SerializeField] private CameraOperator.CameraMode mode;
    [SerializeField] private float shakeStrength;
    [SerializeField] private bool xAxisShake;
    [SerializeField] private bool yAxisShake;
    [SerializeField] private float xAxisOffset;
    [SerializeField] private float yAxisOffset;
}
