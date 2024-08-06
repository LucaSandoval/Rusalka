using System;
using System.Collections;
using UnityEngine;

public class CameraZone : MonoBehaviour
{
    private Transform player;
    private PlayerController playerController;
    [SerializeField] private int priority;
    void Start(){
        player = GameObject.FindWithTag("Player").transform;
        staticPoint.z = -10f;
        playerController = player.GetComponent<PlayerController>();
        currNumTimes = 0;
    }
    [Header("Camera Boundary")]
    [SerializeField] private bool changeCameraBoundaries = false;    
    [SerializeField] private float minXBoundary;
    [SerializeField] private float maxXBoundary;
    [SerializeField] private float minYBoundary;
    [SerializeField] private float maxYBoundary;
    /*[Header("X/Y Distance")]
    [SerializeField] private bool changeXYDistance = false;
    [SerializeField] private float xDistance;
    [SerializeField] private float yDistance;
    private float ogXDistance;
    private float ogYDistance;*/
    [Header("Camera Mode")]
    [SerializeField] private bool changeCameraMode = false;
    [SerializeField] private CameraOperator.CameraMode mode;
    [Header("Camera Size")]
    [SerializeField] private bool changeCameraSize = false;
    [SerializeField] private float Size;
    [Header("Camera Target")]
    [SerializeField] private bool changeCameraTarget = false;
    [SerializeField] private CameraOperator.Target camTarget;
    [SerializeField] private Transform dynamicTarget;
    [SerializeField] private Vector3 staticPoint;
    [Header("Camera Offset")]
    [SerializeField] private bool changeCameraOffset = false;
    [SerializeField] private float xAxisOffset;
    [SerializeField] private float yAxisOffset;
    [Header("Camera Shake")]
    [SerializeField] private bool enableCameraShake = false;
    [SerializeField] private bool xAxisShake;
    [SerializeField] private bool yAxisShake;
    [SerializeField] private float xShakeStrength;
    [SerializeField] private float yShakeStrength;
    [Header("Lock Camera")]
    [SerializeField] private bool lockXAxis = false;
    [SerializeField] private bool lockYAxis = false;
    [Header("Camera Speed")]
    [SerializeField] private bool changeCameraSpeed = false;
    [SerializeField] private bool changeZoomSpeed = false;
    [SerializeField] private float camSpeed;
    [SerializeField] private float zoomSpeed;  
    [Header("Is the Zone Temporary")]
    [SerializeField] private bool Temporary = false;
    [SerializeField] private int howManyTimes = 1;
    private int currNumTimes;
    public void ChangeCameraSpeed(){
        CameraOperator.Instance.SetCameraSpeed(camSpeed);
    }
    public void ChangeZoomSpeed(){
        CameraOperator.Instance.SetZoomSpeed(zoomSpeed);
    }
    public void ChangeCameraMode(){
        CameraOperator.Instance.SetCameraMode(mode);
            
    }
    public void ChangeCameraSize(){
        switch(mode){
            case CameraOperator.CameraMode.Normal:
                CameraOperator.Instance.SetNormalSize(Size);
                break;
            case CameraOperator.CameraMode.VistaPoint:
                CameraOperator.Instance.SetVistaSize(Size);
                break;
            default:
                break;
        }
    }
    public void ChangeCameraTarget(){
        CameraOperator.Instance.SetDynamicTarget(dynamicTarget);
        CameraOperator.Instance.SetStaticPoint(staticPoint);
        CameraOperator.Instance.SetCameraTarget(camTarget);
    }
    public void ChangeCameraShake(){
        CameraOperator.Instance.SetIsShaking(true);
        CameraOperator.Instance.SetXAxisShakeEnabled(xAxisShake);
        CameraOperator.Instance.SetYAxisShakeEnabled(yAxisShake);
        CameraOperator.Instance.SetXShakeStrength(xShakeStrength);
        CameraOperator.Instance.SetYShakeStrength(yShakeStrength);
    }
    public void ChangeCameraOffset(){
        CameraOperator.Instance.SetXAxisOffset(xAxisOffset);
        CameraOperator.Instance.SetYAxisOffset(yAxisOffset);
    }
    public void ChangeCameraLock(){
        if (lockXAxis) CameraOperator.Instance.SetXAxisMoveEnabled(false);
        if (lockYAxis) CameraOperator.Instance.SetYAxisMoveEnabled(false);
    }
    public void ChangeCameraBoundaries(){
        CameraOperator.Instance.SetMinXBoundary(minXBoundary);
        CameraOperator.Instance.SetMaxXBoundary(maxXBoundary);
        CameraOperator.Instance.SetMinYBoundary(minYBoundary);
        CameraOperator.Instance.SetMaxYBoundary(maxYBoundary); 
    }
    /*public void ChangeXYDistance(){
        ogXDistance = CameraOperator.Instance.GetXDistance();
        ogYDistance = CameraOperator.Instance.GetYDistance();
        CameraOperator.Instance.SetXDistance(xDistance);
        CameraOperator.Instance.SetYDistance(yDistance);
    }*/
   /* public void ResetCameraShake(){
        CameraOperator.Instance.SetIsShaking(false);
    }   
    public void ResetCameraSize(){
        switch(mode){
            case CameraOperator.CameraMode.Normal:
                break;
            case CameraOperator.CameraMode.VistaPoint:
                break;
            default:
                break;
        }        
    }
    public void ResetCameraMode(){
        
    }
    public void ResetCameraLock(){
        if (lockXAxis) CameraOperator.Instance.SetXAxisMoveEnabled(true);
        if (lockYAxis) CameraOperator.Instance.SetYAxisMoveEnabled(true);
    }
    public void ResetCameraOffset(){
    }
    public void ResetCameraTarget(){
    }
    public void ResetCameraSpeed(){
    }
    public void ResetZoomSpeed(){
    }
    public void ResetCameraBoundaries(){
    }*/
    /*public void ResetXYDistance(){
        CameraOperator.Instance.SetXDistance(ogXDistance);
        CameraOperator.Instance.SetYDistance(ogYDistance);
    }*/
   /* public void ResetChanges(){
        if(changeCameraMode) ResetCameraMode();
        if(changeCameraSize) ResetCameraSize();            
        if(changeCameraTarget) ResetCameraTarget();
        if(enableCameraShake) ResetCameraShake();
        if(changeCameraOffset) ResetCameraOffset();
        if(lockXAxis || lockYAxis) ResetCameraLock();
        if(changeCameraSpeed) ResetCameraSpeed();
        if(changeZoomSpeed) ResetZoomSpeed();
        //if(changeXYDistance) ResetXYDistance();
        if(changeCameraBoundaries) ResetCameraBoundaries();       
    }*/
    public void ApplyChanges()
    {
        if (changeCameraMode) ChangeCameraMode();
        if (changeCameraSize) ChangeCameraSize();
        if (changeCameraTarget) ChangeCameraTarget();
        if (enableCameraShake) ChangeCameraShake();
        if (changeCameraOffset) ChangeCameraOffset();
        ChangeCameraLock();
        if (changeCameraSpeed) ChangeCameraSpeed();
        if (changeZoomSpeed) ChangeZoomSpeed();
        //if (changeXYDistance) ChangeXYDistance();
        if (changeCameraBoundaries) ChangeCameraBoundaries();
        }
    public void OnTriggerEnter2D(Collider2D collider){
       if(collider.CompareTag("Player") && CameraOperator.Instance != null){
            CameraOperator.Instance.addCameraZone(this);
       }
    }
    public void OnTriggerExit2D(Collider2D collider){
        if(collider.CompareTag("Player") && CameraOperator.Instance != null){ 
            CameraOperator.Instance.removeCameraZone(this);
            if(Temporary){
                currNumTimes++;
                if (currNumTimes >= howManyTimes) Destroy(gameObject);
            }
        }
    }
    public int getPriority() { return priority; }
}