using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureFrameUpdate : MonoBehaviour
{
    [SerializeField] private Sprite[] PictureStates;
    private SpriteRenderer spRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        NumPictures tem = NumPictures.Instance;
        if (tem != null && tem.getPieceCount() <= PictureStates.Length) {
            spRenderer.sprite = PictureStates[tem.getPieceCount()];
        }
    }
}
