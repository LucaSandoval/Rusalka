using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableShaking : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public CameraOperator Operator;
    public bool WantedResult;
    // Update is called once per frame
    void Update(){
        if (Operator.IsShaking != WantedResult){
            Operator.IsShaking = WantedResult;
        }
    }
}
