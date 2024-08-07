using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawnOnFall : MonoBehaviour
{
    public GameObject BirdPrefab;
    private Vector3 Spawn;

    // Start is called before the first frame update
    void Start()
    {
        Spawn = new Vector3(155, -422, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(BirdPrefab, Spawn, Quaternion.identity);
    }
}
