using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeForTime : MonoBehaviour
{
    private bool hasStarted = false;
    private bool toBeDestroyed = false;
    [SerializeField] private float shakeTime;
    void OnTriggerEnter2D(Collider2D collider){
        if (!hasStarted)
        {
            StartCoroutine(Shake(shakeTime));
        }
    }
    private IEnumerator Shake(float time)
    {
        CameraOperator.Instance.SetIsShaking(true);
        yield return new WaitForSeconds(time);
        CameraOperator.Instance.SetIsShaking(false);
        toBeDestroyed = true;

    }
    // Update is called once per frame
    private void Update()
    {
        if (toBeDestroyed)
        {
            Destroy(gameObject);
        }
    }

}
