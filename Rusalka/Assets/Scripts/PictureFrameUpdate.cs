using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PictureFrameUpdate : MonoBehaviour
{
    [SerializeField] private Sprite[] PictureStates;
    private Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        NumPictures tem = NumPictures.Instance;
        if (tem != null && tem.getPieceCount() <= PictureStates.Length) {
            image.sprite = PictureStates[tem.getPieceCount()];
        }
    }
}
