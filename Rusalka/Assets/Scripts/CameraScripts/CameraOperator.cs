using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class CameraOperator : Singleton<CameraOperator>
{
    private float shakeTime;
    // private GameObject[] focusPoints;
    void Start(){
        shakeTime = 0f;
        dynamicTarget = GameObject.FindWithTag("Player").transform;
        // focusPoints = GameObject.FindGameObjectsWithTag("FocusPoint");
    }
    // private variable storing camera position
    private Vector3 targetPos = new(0,0,-10f);
    [SerializeField] private float minXBoundary;
    [SerializeField] private float maxXBoundary;

    [SerializeField] private float minYBoundary;
    [SerializeField] private float maxYBoundary;
    // Currently feels way too clunky
    // [SerializeField] private float minXDistanceFollow;
    // [SerializeField] private float minYDistanceFollow;
    // How fast will the camera follow player
    [SerializeField] private float cameraSpeed = 12f;
    // How fast will the camera zoom in and out
    [SerializeField] private float zoomSpeed = 4f;
    [SerializeField] private Transform dynamicTarget;
    // Another object you might want the camera to lock onto
    [SerializeField] private Vector3 staticPoint = new(0f,0f,-10f);
    // Size of camera for normal view
    [SerializeField] private float normalSize = 12f;
    // Size of camera for vista points
    [SerializeField] private float vistaSize = 25f;
    // Who the camera is currently following
    public enum Target{
        Dynamic,
        Static
    }
    // Is camera zoomed in on the player or doing a vista point
    public enum CameraMode{
        Normal,
        VistaPoint
    }
    // default camera settings
    [SerializeField] private Target cameraTarget = Target.Dynamic;
    [SerializeField] private CameraMode cameraMode = CameraMode.Normal;
    [SerializeField] private bool isShaking = false;
    [SerializeField] private float shakeStrength = 0.35f;
    [SerializeField] private bool xAxisShakeEnabled = true;
    [SerializeField] private bool yAxisShakeEnabled = true;
    [SerializeField] private float xAxisOffset = 0.1f;
    [SerializeField] private float yAxisOffset = 0.1f;
    [SerializeField] private bool xAxisMoveEnabled = true;
    [SerializeField] private bool yAxisMoveEnabled = true;    
    /// <summary>
    /// Changes the size of camera to a specified size
    /// </summary>
    public void ResizeCamera(Camera C, float Size){
        C.orthographicSize = Mathf.SmoothStep(C.orthographicSize, Size, zoomSpeed * Time.fixedDeltaTime);
    }
    /// <summary>
    /// Quickly changes camera position with pre-definied strength to imitate camera shaking
    ///  <list>+Runs on Update instead of FixedUpdate</list>
    ///  <list>+Uses an "internal" timer</list>
    ///  <list>+Works with all Follow targets and Camera Modes</list>
    /// </summary>    
    public void CameraShake(){
        Vector3 OgPos = targetPos;
        transform.position = Vector3.Slerp(transform.position, 
        new Vector3
        (
            xAxisShakeEnabled ? targetPos.x + UnityEngine.Random.Range(-shakeStrength, shakeStrength) - xAxisOffset : targetPos.x, 
            yAxisShakeEnabled ? targetPos.y + UnityEngine.Random.Range(-shakeStrength, shakeStrength) - yAxisOffset : targetPos.y ,
            targetPos.z
        ), 
        cameraSpeed/3 * Time.fixedDeltaTime);
        targetPos = OgPos;
    }
    public float GetCameraSpeed(){
        return cameraSpeed;
    }
    public void SetCameraSpeed(float val){
        cameraSpeed = val;
    }

    public float GetZoomSpeed(){
        return zoomSpeed;
    }
    public void SetZoomSpeed(float val){
        zoomSpeed = val;
    }
    public Transform GetDynamicTarget(){
        return dynamicTarget;
    }
    public void SetDynamicTarget(Transform t){
        dynamicTarget = t;
    }
    public Vector3 GetStaticPoint(){
        return staticPoint;
    }
    public void SetStaticPoint(Vector3 vec){
        staticPoint = vec;
    }
    public float GetNormalSize(){
        return normalSize;
    }
    public void SetNormalSize(float f){
        normalSize = f;
    }
    public float GetVistaSize(){
        return vistaSize;
    }
    public void SetVistaSize(float f){
        vistaSize = f;
    }
    public Target GetCameraTarget(){
        return cameraTarget;
    }
    public void SetCameraTarget(Target fol){
        cameraTarget = fol;
    }
    public CameraMode GetCameraMode(){
        return cameraMode;
    }
    public void SetCameraMode(CameraMode cm){
        cameraMode = cm;
    }
    public bool GetIsShaking(){
        return isShaking;
    }
    public void SetIsShaking(bool boolean){
        isShaking = boolean;
    }
    public float GetShakeStrength(){
        return shakeStrength;
    }
    public void SetShakeStrength(float f){
        shakeStrength = f;
    }
    public bool GetXAxisShakeEnabled(){
        return xAxisShakeEnabled;
    }
    public void SetXAxisShakeEnabled(bool boolean){
        xAxisShakeEnabled = boolean;
    }
    public bool GetYAxisShakeEnabled(){
        return yAxisShakeEnabled;
    }
    public void SetYAxisShakeEnabled(bool boolean){
        yAxisShakeEnabled = boolean;
    }
    public float GetXAxisOffset(){
        return xAxisOffset;
    }
    public void SetXAxisOffset(float val){
        xAxisOffset = val;
    }
    public float GetYAxisOffset(){
        return yAxisOffset;
    }
    public void SetYAxisOffset(float val){
        yAxisOffset = val;
    }
    public bool GetXAxisMoveEnabled(){
        return xAxisMoveEnabled;
    }
    public void SetXAxisMoveEnabled(bool boolean){
        xAxisMoveEnabled = boolean;
    }
    public bool GetYAxisMoveEnabled(){
        return yAxisMoveEnabled;
    }
    public void SetYAxisMoveEnabled(bool boolean){
        yAxisMoveEnabled = boolean;
    }
    void Update(){
        shakeTime += Time.deltaTime; 
        if (isShaking && shakeTime < 1){
            CameraShake();
        }
        if (shakeTime >= 1){ 
            shakeTime = 0;
        }    
    }
    void FixedUpdate()
    {
        // fetching main camera object
        Camera Cam = Camera.main;
        // Current attempt to implement this looks way too clunky
        // float facing = 1;
        // bool grounded = true;
        // if (dynamicTarget.CompareTag("Player"))
        // {
        //     facing = dynamicTarget.GetComponent<PlayerController>().Facing().x > 0 ? 1 : -1;
        //     grounded = dynamicTarget.GetComponent<PlayerController>().IsGrounded();
        // }
        // moves the camera as specified by the target
        switch(cameraTarget){
            case Target.Dynamic:
                float x = dynamicTarget.position.x;
                // float x = dynamicTarget.position.x - xAxisOffset * facing;
                float y = dynamicTarget.position.y;
                // float y = dynamicTarget.position.y - yAxisOffset;
                targetPos = new Vector3(0,0,-10f);
                targetPos.x = xAxisMoveEnabled ? (x > maxXBoundary ? maxXBoundary : x < minXBoundary ? minXBoundary : x) : targetPos.x;
                    // (Math.Abs(x) > minXDistanceFollow ? x - minXDistanceFollow : transform.position.x ))) : 
                targetPos.y = yAxisMoveEnabled ? (y > maxYBoundary ? maxYBoundary : y < minYBoundary ? minYBoundary : y) : targetPos.y;
                    // (Math.Abs(y) > minYDistanceFollow ? y - minYDistanceFollow : transform.position.y ))) : ;
                transform.position = Vector3.Slerp(
                    transform.position, targetPos, cameraSpeed * Time.fixedDeltaTime);
                break;
            case Target.Static:
                targetPos = staticPoint;
                transform.position = Vector3.Slerp(transform.position, targetPos, cameraSpeed/3 * Time.fixedDeltaTime);
                break;
            default:
                break;
        }
        // swaps between camera modes as specified
        switch (cameraMode){
            case CameraMode.Normal:
                ResizeCamera(Cam, normalSize);
                break;
            case CameraMode.VistaPoint:
                ResizeCamera(Cam, vistaSize);
                break;
            default: 
                break;        
        }
    }
}