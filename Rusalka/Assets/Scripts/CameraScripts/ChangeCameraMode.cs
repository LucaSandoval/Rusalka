using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraMode : MonoBehaviour
{
    [SerializeField] private CameraOperator.CameraMode Mode;
    public void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("Player") && CameraOperator.Instance != null){
            CameraOperator.Instance.Set_CameraMode(Mode);
        }
    }
    public void OnTriggerExit2D(Collider2D collider) {
        if(collider.CompareTag("Player") && CameraOperator.Instance != null){
            CameraOperator.Instance.Set_CameraMode(CameraOperator.CameraMode.Normal);
        }
    }    
}
