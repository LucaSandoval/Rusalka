using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPage : MonoBehaviour
{
    public bool level2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        NumPictures.Instance?.AddPiece();
        SoundController.Instance?.PlaySound(level2 ? "PieceCollect2" : "PieceCollect");
        Destroy(gameObject);
    }
}
