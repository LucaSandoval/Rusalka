using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPage : MonoBehaviour
{
    [SerializeField] NumPictures pictureCount;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        pictureCount.AddPiece();
        Destroy(gameObject);
    }
}
