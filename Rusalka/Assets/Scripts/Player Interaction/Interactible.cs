using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    private bool isPlayerInRange = false;
    public Image fadeImage;
    public float fadeDuration;
    public float fadeSpeed;
    public static bool inInteraction = false;
    private PlayerController playerController;
    // Gameobject if you want to cover something up
    public GameObject privacyCurtain;

    // UI prompt
    public GameObject interactionPrompt;
    public Transform teleportPoint;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }
    void Update()
    {
        if (isPlayerInRange && !inInteraction && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            player = other.gameObject;
            if (interactionPrompt != null)
            {
                interactionPrompt.SetActive(true);
            }
        }
        privacyCurtain.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            player = null;
            if (interactionPrompt != null)
            {
                interactionPrompt.SetActive(false);
            }
        }
    }

    void Interact()
    {
        playerController.enabled = false;
        FadeToBlackAndBack();
        inInteraction = true;
       
    }

    public void FadeToBlackAndBack()
    {
        StartCoroutine(FadeToBlackAndBackCouroutine());
        
    }

    private IEnumerator FadeToBlackAndBackCouroutine()
    {
        yield return StartCoroutine(Fade(0, 1));
        player.transform.position = teleportPoint.position;
        privacyCurtain.SetActive(false);
        yield return new WaitForSeconds(fadeDuration);
        playerController.enabled = true;
        yield return StartCoroutine(Fade(1, 0));
       
        inInteraction = false;
        
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeSpeed)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeSpeed);
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }
        fadeImage.color = new Color(color.r, color.g, color.b, endAlpha);
    }
}
