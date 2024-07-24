using System.Collections;
using UnityEngine;

public class PlatformFlippingTwo : MonoBehaviour
{
    public float FlipInterval;
    public float FadeDuration;
    public bool Flipped;

    private SpriteRenderer Sprite;
    private BoxCollider2D Collider;

    // Start is called before the first frame update
    void Start()
    {
        Sprite = GetComponent<SpriteRenderer>();
        Collider = GetComponent<BoxCollider2D>();

        Sprite.enabled = Flipped;
        Collider.enabled = Flipped;

        StartCoroutine(FlipAndFadePlatformRoutine());
    }

    IEnumerator FlipAndFadePlatformRoutine()
    {
        while (true)
        {
            // Start fading out
            yield return StartCoroutine(FadePlatform(true, FadeDuration));
            yield return new WaitForSeconds(FlipInterval - FadeDuration);

            // Flip the state
            Flipped = !Flipped;

            // Apply the flipped state to the BoxCollider
            Collider.enabled = Flipped;

            if (Flipped)
            {
                // Start fading in
                yield return StartCoroutine(FadePlatform(false, FadeDuration));
            }
        }
    }

    IEnumerator FadePlatform(bool fadeOut, float duration)
    {
        float startAlpha = fadeOut ? 1f : 0f;
        float endAlpha = fadeOut ? 0f : 1f;
        float elapsedTime = 0f;

        Color spriteColor = Sprite.color;

        // Enable the sprite renderer at the start of the fade-in process
        if (!fadeOut)
        {
            Sprite.enabled = true;
        }

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            Sprite.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            yield return null;
        }

        Sprite.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, endAlpha);

        // Disable the sprite renderer at the end of the fade-out process
        if (fadeOut)
        {
            Sprite.enabled = false;
        }
    }
}
