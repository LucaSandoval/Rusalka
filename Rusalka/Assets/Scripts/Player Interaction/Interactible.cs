using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class Interactible : MonoBehaviour
{
    private bool isPlayerInRange = false;
    public Image fadeImage;
    public float fadeDuration;
    public float fadeSpeed;
    public static bool inInteraction = false;
    private PlayerController playerController;
    private Collider2D col;
    // Gameobject if you want to cover something up
    public GameObject privacyCurtain;

    // UI prompt
    public GameObject interactionPrompt;
    public Transform teleportPoint;
    private GameObject player;
    public bool LoadDuringFade;
    public bool PlayFadeIn;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        col = playerController.GetComponent<Collider2D>();
        if (PlayFadeIn)
        {
            FadeIntoScene();
        }
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

    /*
     * Start coroutine and make next point non-interactible while in couroutine
     */
    void Interact()
    {
        playerController.enabled = false;
        FadeToBlackAndBack();
        inInteraction = true;
       
    }

    /*
     * Fades screen to black, then back to normal and teleports player while blacked out
     */
    public void FadeToBlackAndBack()
    {
        StartCoroutine(FadeToBlackAndBackCouroutine());
        
        StartCoroutine(RefreshCollider());

    }

    private void FadeIntoScene()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator FadeInFromBlackCouroutine()
    {
        yield return StartCoroutine(Fade(1, 0));
    }
    private IEnumerator FadeToBlackAndBackCouroutine()
    {
        yield return StartCoroutine(Fade(0, 1));
        player.transform.position = teleportPoint.position;
        privacyCurtain.SetActive(false);
        yield return new WaitForSeconds(fadeDuration);
        if (LoadDuringFade) SceneManager.LoadScene(1);
        playerController.enabled = true;
        yield return StartCoroutine(Fade(1, 0));
        
        inInteraction = false;
        
    }

    /*
     * Fades UI texture from specidied alpha values
     */
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

    /*
     * Refreshes the players collider to account for new camera zone
     */
    private IEnumerator RefreshCollider()
    {
        yield return new WaitForSeconds(fadeDuration + fadeSpeed);
        // Disable the collider
        col.enabled = false;

        // Wait for a frame to ensure the physics system updates
        yield return null;

        // Re-enable the collider
        col.enabled = true;
    }
}
