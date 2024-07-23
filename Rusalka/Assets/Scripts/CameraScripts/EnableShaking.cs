using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnableShaking : MonoBehaviour
{
    [SerializeField] private float shakeStrength = 0.3f;
    [SerializeField] private bool xShakeOn = true;
    [SerializeField] private bool yShakeOn = true;

    public void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("Player") && CameraOperator.Instance != null)
            CameraOperator.Instance.SetIsShaking(true);
            CameraOperator.Instance.SetXAxisShakeEnabled(xShakeOn);
            CameraOperator.Instance.SetYAxisShakeEnabled(yShakeOn);
            CameraOperator.Instance.SetShakeStrength(shakeStrength);
    }
    public void OnTriggerExit2D(Collider2D collider) {
        if(collider.CompareTag("Player") && CameraOperator.Instance != null){
            CameraOperator.Instance.SetIsShaking(false);
            CameraOperator.Instance.ResetCameraShake();
            }
    }
}
