using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private bool hasTriggered = false;
    public float moveXBy;
    public float moveYBy;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player") && !hasTriggered){
            transform.position += new Vector3(moveXBy, moveYBy, 0);
            hasTriggered = true;
        }
    }

}
