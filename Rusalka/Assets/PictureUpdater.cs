using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class updates the gameobject's sprite with a new one, based on the picturemanager's number.
 */
public class PictureUpdater : MonoBehaviour
{
    [SerializeField] private Sprite[] Pictures;
    private SpriteRenderer spriteRend;

    // Start is called before the first frame update
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (NumPictures.Instance != null && NumPictures.Instance.getPieceCount() <= Pictures.Length) {
            spriteRend.sprite = Pictures[NumPictures.Instance.getPieceCount()];
        }
    }
}
