using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // // Start is called before the first frame update
    
    // void Start()
    // {
    // }
    // Specifies in which state camera is currently
    
    public float CameraSpeed;
    public float ZoomSpeed;
    public Transform Target;
    public Transform FixedTarget;
    public float ZoomInSize = 3f;
    public float ZoomOutSize = 10f;
    public enum FollowTarget{
        player,
        point,
    }
    public enum CameraMode{
        playerFocus,
        vistaPoint
    }
    public void ResizeCamera(Camera C, float Size){
        C.orthographicSize = Mathf.SmoothStep(C.orthographicSize, Size, ZoomSpeed * Time.fixedDeltaTime);
    }
    public FollowTarget _FollowTarget = FollowTarget.player;
    public CameraMode _CameraMode = CameraMode.playerFocus;
    private UnityEngine.Vector3 cameraPos;

    // Update is called once per frame
    void FixedUpdate()
    {
        Camera Cam = Camera.main;
        switch(_FollowTarget){
            case FollowTarget.player:
                cameraPos = new UnityEngine.Vector3(Target.position.x, Target.position.y, -10f);
                transform.position = UnityEngine.Vector3.Slerp(transform.position, cameraPos, CameraSpeed * Time.fixedDeltaTime);
                break;
            case FollowTarget.point:
                cameraPos = new UnityEngine.Vector3(FixedTarget.position.x, FixedTarget.position.y, -10f);
                transform.position = UnityEngine.Vector3.Slerp(transform.position, cameraPos, CameraSpeed * Time.fixedDeltaTime);
                break;
            default:
                break;
        }
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

