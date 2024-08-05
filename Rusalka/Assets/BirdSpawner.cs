using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    public BoxCollider2D SpawnTriggerArea;
    public GameObject BirdPrefab;
    public float BirdSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnBird());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            0
        );
    }

    IEnumerator SpawnBird()
    {
        while (true) 
        {
            Vector3 SpawnPosition = RandomPointInBounds(SpawnTriggerArea.bounds);
            Instantiate(BirdPrefab, SpawnPosition, Quaternion.identity);

            Debug.Log("Bird Spawn");
            Debug.Log(SpawnPosition);

            yield return new WaitForSeconds(BirdSpawnTime);
        }
    }
}
