using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float StopTime = 2.5f;
    [SerializeField] private GameObject parent;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(CountdownToDestroy(StopTime));
    }
    // Start is called before the first frame update
    public IEnumerator CountdownToDestroy(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(parent);
    }
}
