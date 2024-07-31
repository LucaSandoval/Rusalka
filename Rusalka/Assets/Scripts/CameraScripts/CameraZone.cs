using System;
using System.Collections;
using UnityEngine;

public class CameraZone : MonoBehaviour
{
    private Transform player;
    private PlayerController playerController;
    void Start(){
        player = GameObject.FindWithTag("Player").transform;
        staticPoint.z = -10f;
        playerController = player.GetComponent<PlayerController>();
        currNumTimes = 0;
    }
    private bool hasReset = false;
    [Header("Camera Boundary")]
    [SerializeField] private bool changeCameraBoundaries = false;    
    [SerializeField] private float minXBoundary;
    [SerializeField] private float maxXBoundary;
    [SerializeField] private float minYBoundary;
    [SerializeField] private float maxYBoundary;
    private float ogMinXBoundary;
    private float ogMaxXBoundary;
    private float ogMinYBoundary;
    private float ogMaxYBoundary;
    [Header("X/Y Distance")]
    [SerializeField] private bool changeXYDistance = false;
    [SerializeField] private float xDistance;
    [SerializeField] private float yDistance;
    private float ogXDistance;
    private float ogYDistance;
    [Header("Camera Mode")]
    [SerializeField] private bool changeCameraMode = false;
    [SerializeField] private CameraOperator.CameraMode mode;
    private CameraOperator.CameraMode ogMode;
    [Header("Camera Size")]
    [SerializeField] private bool changeCameraSize = false;
    [SerializeField] private float Size;
    private float ogSize;
    [Header("Camera Target")]
    [SerializeField] private bool changeCameraTarget = false;
    [SerializeField] private CameraOperator.Target camTarget;
    [SerializeField] private Transform dynamicTarget;
    [SerializeField] private Vector3 staticPoint;
    private CameraOperator.Target ogTarget;
    private Transform ogDynamicTarget;
    private Vector3 ogPoint;
    [Header("Camera Offset")]
    [SerializeField] private bool changeCameraOffset = false;
    [SerializeField] private float xAxisOffset;
    [SerializeField] private float yAxisOffset;
    private float ogXOffset;
    private float ogYOffset;
    [Header("Camera Shake")]
    [SerializeField] private bool enableCameraShake = false;
    [SerializeField] private bool xAxisShake;
    [SerializeField] private bool yAxisShake;
    [SerializeField] private float xShakeStrength;
    [SerializeField] private float yShakeStrength;
    private float ogXShakeStrength;
    private float ogYShakeStrength;
    private bool ogXShake;
    private bool ogYShake;
    [Header("Lock Camera")]
    [SerializeField] private bool lockXAxis = false;
    [SerializeField] private bool lockYAxis = false;
    [Header("Camera Speed")]
    [SerializeField] private bool changeCameraSpeed = false;
    [SerializeField] private bool changeZoomSpeed = false;
    [SerializeField] private float camSpeed;
    [SerializeField] private float zoomSpeed;
    private float ogCamSpeed;
    private float ogZoomSpeed;    
    [Header("Pause Player Movement")]
    [SerializeField] private bool stopPlayerMovement = false;
    [SerializeField] private float seconds = 0f;
    [SerializeField] private bool resetAfterwards = false;
    [Header("Is the Zone Temporary")]
    [SerializeField] private bool Temporary = false;
    [SerializeField] private int howManyTimes = 1;
    private int currNumTimes;
    public void ChangeCameraSpeed(){
        ogCamSpeed = CameraOperator.Instance.GetCameraSpeed();
        CameraOperator.Instance.SetCameraSpeed(camSpeed);
    }
    public void ChangeZoomSpeed(){
        ogZoomSpeed = CameraOperator.Instance.GetZoomSpeed();
        CameraOperator.Instance.SetZoomSpeed(zoomSpeed);
    }
    public void ChangeCameraMode(){
        ogMode = CameraOperator.Instance.GetCameraMode();
        CameraOperator.Instance.SetCameraMode(mode);
            
    }
    public void ChangeCameraSize(){
        switch(mode){
            case CameraOperator.CameraMode.Normal:
                ogSize = CameraOperator.Instance.GetNormalSize();
                CameraOperator.Instance.SetNormalSize(Size);
                break;
            case CameraOperator.CameraMode.VistaPoint:
                ogSize = CameraOperator.Instance.GetVistaSize();
                CameraOperator.Instance.SetVistaSize(Size);
                break;
            default:
                break;
        }
    }
    public void ChangeCameraTarget(){
        ogMode = CameraOperator.Instance.GetCameraMode();
        ogDynamicTarget = CameraOperator.Instance.GetDynamicTarget();
        ogPoint = CameraOperator.Instance.GetStaticPoint();
        CameraOperator.Instance.SetDynamicTarget(dynamicTarget);
        CameraOperator.Instance.SetStaticPoint(staticPoint);
        CameraOperator.Instance.SetCameraTarget(camTarget);
    }
    public void ChangeCameraShake(){
        CameraOperator.Instance.SetIsShaking(true);
        ogXShake = CameraOperator.Instance.GetXAxisShakeEnabled();
        CameraOperator.Instance.SetXAxisShakeEnabled(xAxisShake);
        ogYShake = CameraOperator.Instance.GetYAxisShakeEnabled();
        CameraOperator.Instance.SetYAxisShakeEnabled(yAxisShake);
        ogXShakeStrength = CameraOperator.Instance.GetXShakeStrength();
        CameraOperator.Instance.SetXShakeStrength(xShakeStrength);
        ogYShakeStrength = CameraOperator.Instance.GetYShakeStrength();
        CameraOperator.Instance.SetYShakeStrength(yShakeStrength);
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
    public void ChangeCameraBoundaries(){
        ogMinXBoundary = CameraOperator.Instance.GetMinXBoundary();
        ogMaxXBoundary = CameraOperator.Instance.GetMaxXBoundary();
        ogMinYBoundary = CameraOperator.Instance.GetMinYBoundary();
        ogMaxYBoundary = CameraOperator.Instance.GetMaxYBoundary();
        CameraOperator.Instance.SetMinXBoundary(minXBoundary);
        CameraOperator.Instance.SetMaxXBoundary(maxXBoundary);
        CameraOperator.Instance.SetMinYBoundary(minYBoundary);
        CameraOperator.Instance.SetMaxYBoundary(maxYBoundary); 
    }
    public void ChangeXYDistance(){
        ogXDistance = CameraOperator.Instance.GetXDistance();
        ogYDistance = CameraOperator.Instance.GetYDistance();
        CameraOperator.Instance.SetXDistance(xDistance);
        CameraOperator.Instance.SetYDistance(yDistance);
    }
    public void ResetCameraShake(){
        CameraOperator.Instance.SetXShakeStrength(ogXShakeStrength);
        CameraOperator.Instance.SetYShakeStrength(ogYShakeStrength);
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
        CameraOperator.Instance.SetCameraMode(ogMode);
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
        CameraOperator.Instance.SetDynamicTarget(ogDynamicTarget);
        CameraOperator.Instance.SetStaticPoint(ogPoint);
        CameraOperator.Instance.SetCameraTarget(ogTarget);
    }
    public void ResetCameraSpeed(){
        CameraOperator.Instance.SetCameraSpeed(ogCamSpeed);
    }
    public void ResetZoomSpeed(){
        CameraOperator.Instance.SetZoomSpeed(ogZoomSpeed);
    }
    public void ResetCameraBoundaries(){
        CameraOperator.Instance.SetMinXBoundary(ogMinXBoundary);
        CameraOperator.Instance.SetMaxXBoundary(ogMaxXBoundary);
        CameraOperator.Instance.SetMinYBoundary(ogMinYBoundary);
        CameraOperator.Instance.SetMaxYBoundary(ogMaxYBoundary); 
    }
    public void ResetXYDistance(){
        CameraOperator.Instance.SetXDistance(ogXDistance);
        CameraOperator.Instance.SetYDistance(ogYDistance);
    }
    public void Reset(){
        if(changeCameraMode) ResetCameraMode();
        if(changeCameraSize) ResetCameraSize();            
        if(changeCameraTarget) ResetCameraTarget();
        if(enableCameraShake) ResetCameraShake();
        if(changeCameraOffset) ResetCameraOffset();
        if(lockXAxis || lockYAxis) ResetCameraLock();
        if(changeCameraSpeed) ResetCameraSpeed();
        if(changeZoomSpeed) ResetZoomSpeed();
        if(changeXYDistance) ResetXYDistance();
        if(changeCameraBoundaries) ResetCameraBoundaries();
        hasReset = true;        
    }
    public IEnumerator StopPlayer(){
        playerController.SetCanMove(false);
        yield return new WaitForSeconds(seconds);
        playerController.SetCanMove(true);
        if (resetAfterwards) Reset();
    }    
    public void OnTriggerEnter2D(Collider2D collider){
       if(collider.CompareTag("Player") && CameraOperator.Instance != null)
       {
            hasReset = false;
            if(changeCameraMode) ChangeCameraMode();
            if(changeCameraSize) ChangeCameraSize();
            if(changeCameraTarget) ChangeCameraTarget();
            if(enableCameraShake) ChangeCameraShake();
            if(changeCameraOffset) ChangeCameraOffset();
            if(lockXAxis || lockYAxis) ChangeCameraLock();
            if(changeCameraSpeed) ChangeCameraSpeed();
            if(changeZoomSpeed) ChangeZoomSpeed();
            if(changeCameraBoundaries) ChangeCameraBoundaries();
            if(changeXYDistance) ChangeXYDistance();
            if(stopPlayerMovement) StartCoroutine(StopPlayer());
       }
    }
    public void OnTriggerExit2D(Collider2D collider){
        if(collider.CompareTag("Player") && CameraOperator.Instance != null){ 
            if(!hasReset) Reset();
            if(Temporary){
                currNumTimes++;
                if (currNumTimes >= howManyTimes) Destroy(gameObject);
            }
        }
    }
}