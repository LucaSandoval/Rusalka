using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnableShaking : MonoBehaviour
{
    public float _ShakeStrength = 0.3f;
    public bool XShakeOn = true;
    public bool YShakeOn = true;

    public void OnTriggerEnter2D(Collider2D collider) {
        if(collider.tag == "Player" && CameraOperator.Instance != null)
            CameraOperator.Instance.IsShaking = true;
            CameraOperator.Instance.XAxisShakeEnabled = XShakeOn;
            CameraOperator.Instance.YAxisShakeEnabled = YShakeOn;
            CameraOperator.Instance.ShakeStrength = _ShakeStrength;
    }
    public void OnTriggerExit2D(Collider2D collider) {
        if(collider.tag == "Player" && CameraOperator.Instance != null){
            CameraOperator.Instance.IsShaking = false;
            CameraOperator.Instance.ResetCameraShake();
            }
    }
}
