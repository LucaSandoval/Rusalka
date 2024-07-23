using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class CameraOperator : Singleton<CameraOperator>
{
    private float _Time;
    void Start(){
        _Time = 0f;
    }
    // private variable storing camera position
    private UnityEngine.Vector3 cameraPos = new(0,0,-10f);    
    // How fast will the camera follow player
    [SerializeField] private float CameraSpeed = 12f;
    // How fast will the camera zoom in and out
    [SerializeField] private float ZoomSpeed = 4f;
    // Player
    [SerializeField] private Transform DynamicTarget;
    // Another object you might want the camera to lock onto
    [SerializeField] private UnityEngine.Vector3 StaticPoint = new(0f,0f,-10f);
    // Size of camera for normal view
    [SerializeField] private float NormalSize = 12f;
    // Size of camera for vista points
    [SerializeField] private float VistaSize = 25f;
    // Who the camera is currently following
    public enum Follow{
        Dynamic,
        Static
    }
    // Is camera zoomed in on the player or doing a vista point
    public enum CameraMode{
        Normal,
        VistaPoint
    }
    // default camera settings
    [SerializeField] private Follow _FollowTarget = Follow.Dynamic;
    [SerializeField] private CameraMode _CameraMode = CameraMode.Normal;
    [SerializeField] private bool IsShaking = false;
    [SerializeField] private float ShakeStrength = 0.35f;
    [SerializeField] private bool XAxisShakeEnabled = true;
    [SerializeField] private bool YAxisShakeEnabled = true;
    /// <summary>
    /// Sets a point which camera will move towards and changes _FollowTarget to FollowTarget.Static
    /// </summary>
    /// <param name="X"></param>
    /// <param name="Y"></param>
    /// <param name="Z"></param>
    /// <summary>
    /// Changes the size of camera to a specified size
    /// </summary>
    public void ResizeCamera(Camera C, float Size){
        C.orthographicSize = Mathf.SmoothStep(C.orthographicSize, Size, ZoomSpeed * Time.fixedDeltaTime);
    }
    /// <summary>
    /// Quickly changes camera position with pre-definied strength to imitate camera shaking
    ///  <list>+Runs on Update instead of FixedUpdate</list>
    ///  <list>+Uses an "internal" timer</list>
    ///  <list>+Works with all Follow targets and Camera Modes</list>
    /// </summary>    
    public void CameraShake(){
        UnityEngine.Vector3 OgPos = cameraPos;
        transform.position = UnityEngine.Vector3.Slerp(transform.position, 
        new UnityEngine.Vector3
        (
            XAxisShakeEnabled ? cameraPos.x + UnityEngine.Random.Range(-ShakeStrength, ShakeStrength) : cameraPos.x, 
            YAxisShakeEnabled ? cameraPos.y + UnityEngine.Random.Range(-ShakeStrength, ShakeStrength) : cameraPos.y,
            cameraPos.z
        ), 
        CameraSpeed/3 * Time.fixedDeltaTime);
        cameraPos = OgPos;
    }
    /// <summary>
    /// Resets Camera Shake Strength to 0.15f and enables both axies of shake (x and y);
    /// </summary>
    public void ResetCameraShake(){
        ShakeStrength = 0.15f;
        XAxisShakeEnabled = true;
        YAxisShakeEnabled = true;
    }
    public float GetCameraSpeed(){
        return CameraSpeed;
    }
    public void SetCameraSpeed(float val){
        CameraSpeed = val;
    }

    public float GetZoomSpeed(){
        return ZoomSpeed;
    }
    public void SetZoomSpeed(float val){
        ZoomSpeed = val;
    }
    public Transform GetDynamicTarget(){
        return DynamicTarget;
    }
    public void SetDynamicTarget(Transform t){
        DynamicTarget = t;
    }
    public UnityEngine.Vector3 GetStaticPoint(){
        return StaticPoint;
    }
    public void SetStaticPoint(UnityEngine.Vector3 vec){
        StaticPoint = vec;
    }
    public float GetNormalSize(){
        return NormalSize;
    }
    public void SetNormalSize(float f){
        NormalSize = f;
    }
    public float GetVistaSize(){
        return VistaSize;
    }
    public void SetVistaSize(float f){
        VistaSize = f;
    }
    public Follow Get_FollowTarget(){
        return _FollowTarget;
    }
    public void Set_FollowTarget(Follow fol){
        _FollowTarget = fol;
    }
    public CameraMode Get_CameraMode(){
        return _CameraMode;
    }
    public void Set_CameraMode(CameraMode cm){
        _CameraMode = cm;
    }
    public bool GetIsShaking(){
        return IsShaking;
    }
    public void SetIsShaking(bool boolean){
        IsShaking = boolean;
    }
    public float GetShakeStrength(){
        return ShakeStrength;
    }
    public void SetShakeStrength(float f){
        ShakeStrength = f;
    }
    public bool GetXAxisShakeEnabled(){
        return XAxisShakeEnabled;
    }
    public void SetXAxisShakeEnabled(bool boolean){
        XAxisShakeEnabled = boolean;
    }
    public bool GetYAxisShakeEnabled(){
        return YAxisShakeEnabled;
    }
    public void SetYAxisShakeEnabled(bool boolean){
        YAxisShakeEnabled = boolean;
    }    
    void Update(){
        _Time += Time.deltaTime; 
        if (IsShaking && _Time < 1){
            CameraShake();
        }
        if (_Time >= 1){ 
            _Time = 0;
        }    
    }
    void FixedUpdate()
    {
        // fetching main camera object
        Camera Cam = Camera.main;
        // moves the camera acording to the target
        switch(_FollowTarget){
            case Follow.Dynamic:
                cameraPos = new UnityEngine.Vector3(DynamicTarget.position.x, DynamicTarget.position.y, -10f);
                transform.position = UnityEngine.Vector3.Slerp(transform.position, cameraPos, CameraSpeed * Time.fixedDeltaTime);
                break;
            case Follow.Static:
                cameraPos = StaticPoint;
                transform.position = UnityEngine.Vector3.Slerp(transform.position, cameraPos, CameraSpeed/3 * Time.fixedDeltaTime);
                break;
            default:
                break;
        }
        // zoomes in or out if needed
        switch (_CameraMode){
            case CameraMode.Normal:
                ResizeCamera(Cam, NormalSize);
                break;
            case CameraMode.VistaPoint:
                ResizeCamera(Cam, VistaSize);
                break;
            default: 
                break;        
        }
    }
}