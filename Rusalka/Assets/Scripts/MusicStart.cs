using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicStart : MonoBehaviour
{
    [SerializeField] private string musicFile;
    // Start is called before the first frame update
    void Start()
    {
        SoundController.Instance.PlaySound(musicFile);
        Destroy(gameObject, .01f);
    }
}
