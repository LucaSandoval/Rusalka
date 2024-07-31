using System.Collections;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    public GameObject objectToFade;
    public float fadeDuration = 1.0f;

    private Renderer objectRenderer;
    private Color originalColor;

    void Start()
    {
        objectRenderer = objectToFade.GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalColor = objectRenderer.material.color;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // Assuming the player has the tag "Player"
        {
            StartCoroutine(FadeOutCoroutine());
        }
    }

    IEnumerator FadeOutCoroutine()
    {
        float startAlpha = originalColor.a;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, 0, elapsedTime / fadeDuration);
            SetAlpha(newAlpha);
            yield return null;  // Wait for the next frame
        }
        SetAlpha(0);  // Ensure the object is completely transparent at the end
    }

    void SetAlpha(float alpha)
    {
        Color newColor = originalColor;
        newColor.a = alpha;
        objectRenderer.material.color = newColor;
    }
}
