using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScroll : MonoBehaviour
{
    public GameObject SkipIndicator;
    private bool CanScroll;
    private float scrollSpeed;

    private const float scrollSpeedMin = 120f;
    private const float scrollSpeedMax = 850f;

    public string creditsSong;

    void Start()
    {
        CanScroll = false;
        Invoke("StartScroll", 4.5f);
    }

    private void StartScroll()
    {
        CanScroll = true;
        SoundController.Instance?.PlaySound(creditsSong);
    }

    private void Update()
    {
        if (CanScroll)
        {
            transform.position += new Vector3(0, scrollSpeed * Time.deltaTime, 0);

            if (GetComponent<RectTransform>().anchoredPosition.y >= 10900)
            {
                CanScroll = false;
                Debug.Log("Credits Finished!");
                SoundController.Instance?.FadeOutSound(creditsSong, 3f);
                Invoke("ReturnToMenu", 3f);
            }
        }

        if (Input.GetButton("Jump"))
        {
            scrollSpeed = scrollSpeedMax;
        } else
        {
            scrollSpeed = scrollSpeedMin;
        }

        SkipIndicator.SetActive(CanScroll);
    }

    private void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
