using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProximityText : MonoBehaviour
{
    private TextMeshPro text;
    private float alpha;

    private float fadeSpeed = 1f;

    private bool playerInRange;

    [SerializeField] private string[] textForKeyboard;
    [SerializeField] private string[] textForGamepad;
    [SerializeField] private InputDetection inputDetection;

    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
        alpha = 0;
    }

    private void Update()
    {
        if (playerInRange)
        {
            if (alpha < 1)
            {
                alpha += Time.deltaTime * fadeSpeed;
            }
        } else
        {
            if (alpha > 0)
            {
                alpha -= Time.deltaTime * fadeSpeed;
            }
        }
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
        text.text = inputDetection.IsUsingKeyboard() ? textForKeyboard[(int)GlobalSettings.GlobalLanguage] : textForGamepad[(int)GlobalSettings.GlobalLanguage];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
