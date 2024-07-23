using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class CameraOperator : Singleton<CameraOperator>
{
    // private variable storing camera position
    private UnityEngine.Vector3 cameraPos;    
    // How fast will the camera follow player
    public float CameraSpeed = 12f;
    // How fast will the camera zoom in and out
    public float ZoomSpeed = 4f;
    // Player
    public Transform DynamicTarget;
    // Another object you might want the camera to lock onto
    public UnityEngine.Vector3 StaticPoint = new(0f,0f,-10f);
    // Size of camera for normal view
    public float NormalSize = 12f;
    // Size of camera for vista points
    public float VistaSize = 25f;
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
    public Follow _FollowTarget = Follow.Dynamic;
    public CameraMode _CameraMode = CameraMode.Normal;
    public bool IsShaking = false;
    public float ShakeStrenght = 3.7f;

    /// <summary>
    /// Sets a point which camera will move towards and changes _FollowTarget to FollowTarget.Static
    /// </summary>
    /// <param name="X"></param>
    /// <param name="Y"></param>
    /// <param name="Z"></param>
    public void followStaticPoint(float X, float Y, float Z){
        StaticPoint = new(X,Y,Z);
        _FollowTarget = Follow.Static;
    }
    public void followStaticPoint(float X, float Y){
        followStaticPoint(X,Y,-10f);
    }
    /// <summary>
    /// Changes the size of camera to a specified size
    /// </summary>
    public void ResizeCamera(Camera C, float Size){
        C.orthographicSize = Mathf.SmoothStep(C.orthographicSize, Size, ZoomSpeed * Time.fixedDeltaTime);
    }
    public void CameraShake(){
        UnityEngine.Vector3 OgPos = cameraPos;
        transform.position = UnityEngine.Vector3.Slerp(transform.position, 
        new UnityEngine.Vector3(cameraPos.x + UnityEngine.Random.Range(-ShakeStrenght, ShakeStrenght), 
        cameraPos.y + UnityEngine.Random.Range(-ShakeStrenght, ShakeStrenght),
        cameraPos.z), CameraSpeed/3 * Time.fixedDeltaTime);
        cameraPos = OgPos;
    }
    // Update is called once per frame
    void Update(){
        if (IsShaking){
            CameraShake();
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