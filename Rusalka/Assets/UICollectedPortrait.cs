using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICollectedPortrait : MonoBehaviour
{
    public Image[] portaitImages;

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
        for(int i = 0; i <= 4; i++)
        {
            if (NumPictures.Instance != null)
            {
                Color img_color = (NumPictures.Instance.getPieceCount() > i) ? Color.white : Color.black;
                float max_op = (NumPictures.Instance.getPieceCount() > i) ? 1 : 0.5f;

                portaitImages[i].color = new Color(img_color.r, img_color.g, img_color.b, Mathf.Clamp(alpha, 0, max_op));
            }
        }        
    }
}
