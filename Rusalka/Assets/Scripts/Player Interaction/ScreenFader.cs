using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.TerrainUtils;

public class ScreenFader : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration;
    // Start is called before the first frame update
    void Start()
    {
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b);
    }

    public void FadeToBlackAndBack()
    {
        StartCoroutine(FadeToBlackAndBackCouroutine());
    }

    private IEnumerator FadeToBlackAndBackCouroutine()
    {
        yield return StartCoroutine(Fade(0, 1));
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(Fade(1, 0));
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }
        fadeImage.color = new Color(color.r, color.g, color.b, endAlpha);
    }
}
