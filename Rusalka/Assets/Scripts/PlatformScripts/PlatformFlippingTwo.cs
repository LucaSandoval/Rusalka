using System.Collections;
using UnityEngine;

public class PlatformFlippingTwo : MonoBehaviour
{
    public float FlipInterval = 2f; // Time before flipping
    public float FadeDuration = 1f; // Duration of the fade effect
    public bool Flipped = false;

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
            if (Flipped)
            {
                // Start fading out
                yield return StartCoroutine(FadePlatform(true, FadeDuration));
                Collider.enabled = false;
            }
            else
            {
                // Enable collider and start fading in
                Collider.enabled = true;
                yield return StartCoroutine(FadePlatform(false, FadeDuration));
            }

            // Wait for the remaining interval minus the fade duration
            yield return new WaitForSeconds(FlipInterval - FadeDuration);

            // Flip the state
            Flipped = !Flipped;
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
