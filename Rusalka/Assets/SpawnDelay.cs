using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDelay : MonoBehaviour
{
    private SpriteRenderer Sprite;
    public float delay;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowAndHide(delay));
        Sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ShowAndHide(float delay)
    {
        Sprite.enabled = true;
        yield return new WaitForSeconds(delay);
        
    }

}
