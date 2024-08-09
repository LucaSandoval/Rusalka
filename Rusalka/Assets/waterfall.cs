using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterfall : MonoBehaviour
{
    public GameObject[] fallingSegments;

    private float fallSpeed = 20f;

    [SerializeField] private float topSpawnPos;
    [SerializeField] private float bottomMin;

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject obj in fallingSegments)
        {
            obj.transform.position += new Vector3(0, -(Time.deltaTime * fallSpeed), 0);
            if (obj.transform.localPosition.y < bottomMin)
            {
                obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, topSpawnPos, 0);
            }
        }
    }
}
