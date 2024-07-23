using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraMode : MonoBehaviour
{
    // Start is called before the first frame update
    // void Start()
    // {
    // }
    public CameraFollow.CameraMode Mode;
    // Update is called once per frame
    public bool Change = false;
    void FixedUpdate()
    {
       CameraFollow _CameraFollow = FindObjectOfType<CameraFollow>(); 
       if (Change && _CameraFollow._CameraMode != Mode){
            _CameraFollow._CameraMode = Mode;
            Change = false;
       }
    }
}
