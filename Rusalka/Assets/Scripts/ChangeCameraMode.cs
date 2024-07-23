using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraMode : MonoBehaviour
{
    public CameraOperator.CameraMode Mode;
    public void OnTriggerEnter2D(Collider2D collider) {
        if(collider.tag == "Player" && CameraOperator.Instance != null){
            CameraOperator.Instance._CameraMode = Mode;
        }
    }
    public void OnTriggerExit2D(Collider2D collider) {
        if(collider.tag == "Player" && CameraOperator.Instance != null){
            CameraOperator.Instance._CameraMode = CameraOperator.CameraMode.Normal;
        }
    }    
}
