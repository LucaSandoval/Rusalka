using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScroll : MonoBehaviour
{
    public GameObject SkipIndicator;
    private bool CanScroll;
    private float scrollSpeed;

    private const float scrollSpeedMin = 90f;
    private const float scrollSpeedMax = 750f;

    void Start()
    {
        CanScroll = false;
        Invoke("StartScroll", 1.5f);
    }

    private void StartScroll()
    {
        CanScroll = true;
        SoundController.Instance?.PlaySound("Lvl2MusicWhiteVoid");
    }

    private void Update()
    {
        if (CanScroll)
        {
            transform.position += new Vector3(0, scrollSpeed * Time.deltaTime, 0);

            if (GetComponent<RectTransform>().anchoredPosition.y >= 3500)
            {
                CanScroll = false;
                Debug.Log("Credits Finished!");
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
}
