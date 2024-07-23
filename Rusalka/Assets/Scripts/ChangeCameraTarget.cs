using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraTarget : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public CameraOperator.Follow Target;
    // Update is called once per frame
    void Update()
    {
        if (CameraOperator.Instance._FollowTarget != Target){
            CameraOperator.Instance._FollowTarget = Target;
       }
    }
}
