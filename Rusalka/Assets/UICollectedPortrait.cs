using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICollectedPortrait : MonoBehaviour
{
    public Image portaitImage;
    public Image bgImage;

    private bool fadingIn;
    private float fadeInTimer;

    private float alpha;

    protected void Start()
    {
        if (NumPictures.Instance != null)
        {
            NumPictures.Instance.OnPieceCollected += ShowFrame;
        }
    }

    private void Awake()
    {
        alpha = 0;
        SetPortraitOpacity();
    }

    private void ShowFrame()
    {
        fadingIn = true;
        fadeInTimer = 7.5f;
    }


    protected  void Update()
    {
        if (fadingIn)
        {
            fadeInTimer -= Time.deltaTime;
            alpha += Time.deltaTime * 0.5f;
            if (alpha > 1) alpha = 1;

            if (fadeInTimer <= 0)
            {
                fadingIn = false;
            }
        } else
        {
            alpha -= Time.deltaTime * 0.8f;
            if (alpha <= 0) alpha = 0;
        }

        SetPortraitOpacity();
    }

    private void SetPortraitOpacity()
    {
        portaitImage.color = new Color(portaitImage.color.r, portaitImage.color.g, portaitImage.color.b, alpha);
        bgImage.color = new Color(bgImage.color.r, bgImage.color.g, bgImage.color.b, Mathf.Clamp(alpha, 0, 0.38f));
    }
}
