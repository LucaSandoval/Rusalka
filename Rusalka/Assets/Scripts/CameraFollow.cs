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
    public Transform Target;
    // Another object you might want the camera to lock onto
    public Transform FixedTarget;
    public UnityEngine.Vector3 Point = new(0f,0f,-10f);
    // Size of camera for normal view
    public float ZoomInSize = 3f;
    // Size of camera for vista points
    public float ZoomOutSize = 10f;
    // Who the camera is currently following
    public enum FollowTarget{
        player,
        nonPlayer,
        point
    }
    // Is camera zoomed in on the player or doing a vista point
    public enum CameraMode{
        playerFocus,
        vistaPoint
    }
    /// <summary>
    /// Changes the size of camera to a specified size
    /// </summary>
    public void ResizeCamera(Camera C, float Size){
        C.orthographicSize = Mathf.SmoothStep(C.orthographicSize, Size, ZoomSpeed * Time.fixedDeltaTime);
    }
    // default camera values
    public FollowTarget _FollowTarget = FollowTarget.player;
    public CameraMode _CameraMode = CameraMode.playerFocus;
    // private variable storing camera position
    private UnityEngine.Vector3 cameraPos;
    /// <summary>
    /// Changes the point where camera statically looks at
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public void setCameraStaticPoint(float x, float y, float z){
        Point = new(x,y,z);
        _FollowTarget = FollowTarget.point;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        // fetching main camera object
        Camera Cam = Camera.main;
        // moves the camera acording to the target
        switch(_FollowTarget){
            case FollowTarget.player:
                cameraPos = new UnityEngine.Vector3(Target.position.x, Target.position.y, -10f);
                transform.position = UnityEngine.Vector3.Slerp(transform.position, cameraPos, CameraSpeed * Time.fixedDeltaTime);
                break;
            case FollowTarget.nonPlayer:
                cameraPos = new UnityEngine.Vector3(FixedTarget.position.x, FixedTarget.position.y, -10f);
                transform.position = UnityEngine.Vector3.Slerp(transform.position, cameraPos, CameraSpeed * Time.fixedDeltaTime);
                break;
            case FollowTarget.point:
                cameraPos = Point;
                transform.position = UnityEngine.Vector3.Slerp(transform.position, cameraPos, CameraSpeed/3 * Time.fixedDeltaTime);
                break;
            default:
                break;
        }
        // zoomes in or out if needed
        switch (_CameraMode){
            case CameraMode.playerFocus:
                ResizeCamera(Cam, ZoomInSize);
                break;
            case CameraMode.vistaPoint:
                ResizeCamera(Cam, ZoomOutSize);
                break;
            default: 
                break;        
        }
    }
}