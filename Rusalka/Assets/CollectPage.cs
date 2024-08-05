using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        NumPictures.Instance?.AddPiece();
        SoundController.Instance?.PlaySound("PieceCollect");
        Destroy(gameObject);
    }
}
