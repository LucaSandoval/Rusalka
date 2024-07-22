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
    void Start()
    {
        cameraPos = new UnityEngine.Vector3(0,0,0);
    }
    // Specifies in which state camera is currently
    
    public float CameraSpeed;
    public float ZoomSpeed;
    public Transform target;
    public enum FollowTarget{
        player,
        point,
    }
    public enum CameraMode{
        playerFocus,
        vistaPoint,
        lockedOn
    }
    public void ResizeCamera(Camera C, float Size){
        if (C.orthographicSize < Size) ZoomOut(C, Size);
        else if (C.orthographicSize > Size) ZoomIn(C, Size);
    }
    public void ZoomOut(Camera C, float Size){
        C.orthographicSize = Mathf.SmoothStep(C.orthographicSize, Size, ZoomSpeed * Time.fixedDeltaTime);
    }
    public void ZoomIn(Camera C, float Size){
        C.orthographicSize = Mathf.SmoothStep(Size, C.orthographicSize, ZoomSpeed * Time.fixedDeltaTime);
    }
    public FollowTarget _FollowTarget = FollowTarget.player;
    public CameraMode _CameraMode = CameraMode.playerFocus;
    private UnityEngine.Vector3 cameraPos;
    // void setCameraState(CameraState cs){
    //     _CameraState = cs;
    // }

    // CameraState getCameraState(){
    //     return _CameraState;
    // }

    // Update is called once per frame
    void Update()
    {
        Camera Cam = Camera.main;
        switch(_FollowTarget){
            case FollowTarget.player:
                cameraPos = new UnityEngine.Vector3(target.position.x, target.position.y, -10f);
                transform.position = UnityEngine.Vector3.Slerp(transform.position, cameraPos, CameraSpeed * Time.deltaTime);
                break;
            case FollowTarget.point:
                break;           
            default:
                break;
        }
        switch (_CameraMode){
            case CameraMode.playerFocus:
                ResizeCamera(Cam, 3f);
                break;
            case CameraMode.vistaPoint:
                ResizeCamera(Cam, 10f);
                break;
            default: 
                break;        
        }
    }
}
