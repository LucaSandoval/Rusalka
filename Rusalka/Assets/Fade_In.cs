using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade_In : MonoBehaviour
{
    private bool fadeIn;
    private SpriteRenderer sprite;
    [SerializeField] private float FadeSpeed;
    // Start is called before the first frame update
    void Start()
    {
        fadeIn = false;
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIn) {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a + FadeSpeed * Time.deltaTime);
        }
    }

    public void SetFadeIn(bool doFade) {
        fadeIn = doFade;
    }
}
