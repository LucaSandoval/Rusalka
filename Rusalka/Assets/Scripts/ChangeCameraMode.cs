using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraMode : MonoBehaviour
{
    // Start is called before the first frame update
    // void Start()
    // {
    // }
    public CameraOperator.CameraMode Mode;
    // Update is called once per frame
    void FixedUpdate()
    {
       if (CameraOperator.Instance._CameraMode != Mode){
            CameraOperator.Instance._CameraMode = Mode;
       }
    }
}
