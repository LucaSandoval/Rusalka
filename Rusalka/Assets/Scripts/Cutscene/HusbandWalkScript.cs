using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class HusbandWalkScript : MonoBehaviour
{
    public float Walktime;
    public Vector3 endPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Increase the interpolation time
        float t = Time.deltaTime * Walktime;

        transform.position = Vector2.MoveTowards(transform.position, endPosition, t);

        if (transform.position == endPosition)
        {
            Destroy(gameObject);
        }
    }

}
