using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureFallbackZone : MonoBehaviour
{
    [SerializeField] private CollectPage PortraitPiece;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (PortraitPiece != null)
            {
                PortraitPiece.FlyToPlayer();
                Destroy(gameObject);
            }
        }
    }
}
