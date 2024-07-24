using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class CameraOperator : Singleton<CameraOperator>
{
    private float shakeTime;
    void Start(){
        shakeTime = 0f;
        dynamicTarget = GameObject.FindWithTag("Player").transform;
    }
    // private variable storing camera position
    private Vector3 cameraPos = new(0,0,-10f);    
    // How fast will the camera follow player
    [SerializeField] private float cameraSpeed = 12f;
    // How fast will the camera zoom in and out
    [SerializeField] private float zoomSpeed = 4f;
    // Player
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
        Vector3 OgPos = cameraPos;
        transform.position = Vector3.Slerp(transform.position, 
        new Vector3
        (
            xAxisShakeEnabled ? cameraPos.x + UnityEngine.Random.Range(-shakeStrength, shakeStrength) : cameraPos.x, 
            yAxisShakeEnabled ? cameraPos.y + UnityEngine.Random.Range(-shakeStrength, shakeStrength) : cameraPos.y,
            cameraPos.z
        ), 
        cameraSpeed/3 * Time.fixedDeltaTime);
        cameraPos = OgPos;
    }
    /// <summary>
    /// Resets Camera Shake Strength to 0.15f and enables both axies of shake (x and y);
    /// </summary>
    public void ResetCameraShake(){
        shakeStrength = 0.15f;
        xAxisShakeEnabled = true;
        yAxisShakeEnabled = true;
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
        // moves the camera acording to the target
        switch(cameraTarget){
            case Target.Dynamic:
                cameraPos = new Vector3(dynamicTarget.position.x, dynamicTarget.position.y, -10f);
                transform.position = Vector3.Slerp(transform.position, cameraPos, cameraSpeed * Time.fixedDeltaTime);
                break;
            case Target.Static:
                cameraPos = staticPoint;
                transform.position = Vector3.Slerp(transform.position, cameraPos, cameraSpeed/3 * Time.fixedDeltaTime);
                break;
            default:
                break;
        }
        // zoomes in or out if needed
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