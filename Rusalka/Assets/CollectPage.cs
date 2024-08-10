using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPage : MonoBehaviour
{
    public bool level2;
    private GameObject player;
    private bool flyToPlayer;
    private float flySpeed = 3f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        NumPictures.Instance?.AddPiece();
        SoundController.Instance?.PlaySound(level2 ? "PieceCollect2" : "PieceCollect");
        Destroy(gameObject);
    }

    public void FlyToPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        flyToPlayer = true;
    }

    private void Update()
    {
        if (flyToPlayer)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime * flySpeed);
        }
    }
}
