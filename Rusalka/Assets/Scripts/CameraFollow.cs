using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // How fast will the camera follow player
    public float CameraSpeed = 3.3f;
    // How fast will the camera zoom in and out
    public float ZoomSpeed = 4.3f;
    // Player
    public Transform DynamicTarget;
    // Another object you might want the camera to lock onto
    public UnityEngine.Vector3 StaticPoint = new(0f,0f,-10f);
    // Size of camera for normal view
    public float NormalSize = 3f;
    // Size of camera for vista points
    public float VistaSize = 10f;
    // Who the camera is currently following
    public enum FollowTarget{
        Dynamic,
        Static
    }
    // Is camera zoomed in on the player or doing a vista point
    public enum CameraMode{
        Normal,
        VistaPoint
    }
    /// <summary>
    /// Changes the size of camera to a specified size
    /// </summary>
    public void ResizeCamera(Camera C, float Size){
        C.orthographicSize = Mathf.SmoothStep(C.orthographicSize, Size, ZoomSpeed * Time.fixedDeltaTime);
    }
    // default camera settings
    public FollowTarget _FollowTarget = FollowTarget.Dynamic;
    public CameraMode _CameraMode = CameraMode.Normal;
    // private variable storing camera position
    private UnityEngine.Vector3 cameraPos;
    /// <summary>
    /// Sets a point which camera will move towards and changes _FollowTarget to FollowTarget.Static
    /// </summary>
    /// <param name="X"></param>
    /// <param name="Y"></param>
    /// <param name="Z"></param>
    public void followStaticPoint(float X, float Y, float Z){
        StaticPoint = new(X,Y,Z);
        _FollowTarget = FollowTarget.Static;
    }
    public void followStaticPoint(float X, float Y){
        followStaticPoint(X,Y,-10f);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        // fetching main camera object
        Camera Cam = Camera.main;
        // moves the camera acording to the target
        switch(_FollowTarget){
            case FollowTarget.Dynamic:
                cameraPos = new UnityEngine.Vector3(DynamicTarget.position.x, DynamicTarget.position.y, -10f);
                transform.position = UnityEngine.Vector3.Slerp(transform.position, cameraPos, CameraSpeed * Time.fixedDeltaTime);
                break;
            case FollowTarget.Static:
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