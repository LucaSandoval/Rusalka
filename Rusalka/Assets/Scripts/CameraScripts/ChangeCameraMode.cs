using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraMode : MonoBehaviour
{
    [SerializeField] private CameraOperator.CameraMode mode;
    [SerializeField] private float newSize;
    private float originalValue;
    public void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("Player") && CameraOperator.Instance != null){
            CameraOperator.Instance.SetCameraMode(mode);
            switch(mode){
                case CameraOperator.CameraMode.Normal:
                    originalValue = CameraOperator.Instance.GetNormalSize();
                    CameraOperator.Instance.SetNormalSize(newSize);
                    break;
                case CameraOperator.CameraMode.VistaPoint:
                    originalValue = CameraOperator.Instance.GetVistaSize();
                    CameraOperator.Instance.SetVistaSize(newSize);
                    break;
                default:
                    break;
            }
        }
    }
    public void OnTriggerExit2D(Collider2D collider) {
        if(collider.CompareTag("Player") && CameraOperator.Instance != null){
            switch(mode){
                case CameraOperator.CameraMode.Normal:
                    CameraOperator.Instance.SetNormalSize(originalValue);
                    break;
                case CameraOperator.CameraMode.VistaPoint:
                    CameraOperator.Instance.SetVistaSize(originalValue);
                    break;
                default:
                    break;
            }
            CameraOperator.Instance.SetCameraMode(CameraOperator.CameraMode.Normal);
        }

    }    
}
