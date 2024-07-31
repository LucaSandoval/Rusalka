using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float StopTime = 2.5f;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(ShakeAndDestroy(StopTime));
    }
    // Start is called before the first frame update
    public IEnumerator ShakeAndDestroy(float seconds)
    {
        CameraOperator.Instance.SetIsShaking(true);
        yield return new WaitForSeconds(seconds);
        CameraOperator.Instance.SetIsShaking(false);
        Destroy(gameObject);
    }
}
