using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnableShaking : MonoBehaviour
{
    [SerializeField] private float _ShakeStrength = 0.3f;
    [SerializeField] private bool XShakeOn = true;
    [SerializeField] private bool YShakeOn = true;

    public void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("Player") && CameraOperator.Instance != null)
            CameraOperator.Instance.SetIsShaking(true);
            CameraOperator.Instance.SetXAxisShakeEnabled(XShakeOn);
            CameraOperator.Instance.SetYAxisShakeEnabled(YShakeOn);
            CameraOperator.Instance.SetShakeStrength(_ShakeStrength);
    }
    public void OnTriggerExit2D(Collider2D collider) {
        if(collider.CompareTag("Player") && CameraOperator.Instance != null){
            CameraOperator.Instance.SetIsShaking(false);
            CameraOperator.Instance.ResetCameraShake();
            }
    }
}
