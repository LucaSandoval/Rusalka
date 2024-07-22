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
    public float CameraSpeed = 10f;
    // How fast will the camera zoom in and out
    public float ZoomSpeed = 4.3f;
    // Player
    public Transform Player;
    // Another object you might want the camera to lock onto
    public Transform FixedTarget;
    // Size of camera for normal view
    public float ZoomInSize = 3f;
    // Size of camera for vista points
    public float ZoomOutSize = 10f;
    // Who the camera is currently following
    public enum FollowTarget{
        player,
        point,
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

    // Update is called once per frame
    void FixedUpdate()
    {
        // fetching main camera object
        Camera Cam = Camera.main;
        // moves the camera acording to the target
        switch(_FollowTarget){
            case FollowTarget.player:
                cameraPos = new UnityEngine.Vector3(Player.position.x, Player.position.y, -10f);
                transform.position = UnityEngine.Vector3.Slerp(transform.position, cameraPos, CameraSpeed * Time.fixedDeltaTime);
                break;
            case FollowTarget.point:
                cameraPos = new UnityEngine.Vector3(FixedTarget.position.x, FixedTarget.position.y, -10f);
                transform.position = UnityEngine.Vector3.Slerp(transform.position, cameraPos, CameraSpeed * Time.fixedDeltaTime);
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