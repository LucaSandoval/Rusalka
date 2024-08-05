using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAffect : MonoBehaviour
{
    private enum AffectMode
    {
        Start,
        Stop,
        FadeIn,
        FadeOut
    }

    [SerializeField] private AffectMode Mode;
    [SerializeField] private string MusicFile;
    [SerializeField] private float FadeTime;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MusicAction();
            Destroy(gameObject);
        }
    }

    private void MusicAction() {
        switch (Mode) {
            case AffectMode.Start:
                SoundController.Instance.PlaySound(MusicFile);
                break;
            case AffectMode.Stop:
                SoundController.Instance.PauseSound(MusicFile);
                break;
            case AffectMode.FadeIn:
                SoundController.Instance.FadeInSound(MusicFile, FadeTime, 0);
                break;
            case AffectMode.FadeOut:
                SoundController.Instance.FadeOutSound(MusicFile, FadeTime);
                break;
            default:
                break;
        }
    }
}
