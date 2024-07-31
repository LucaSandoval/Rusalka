using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAffect : MonoBehaviour
{
    private enum AffectMode {
        Start,
        Stop,
        FadeIn,
        FadeOut
    }

    [SerializeField] AffectMode Mode;
    [SerializeField] string MusicFile;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MusicAction();
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
                SoundController.Instance.FadeInSound(MusicFile, .5f, 0);
                break;
            case AffectMode.FadeOut:
                SoundController.Instance.FadeOutSound(MusicFile, .5f);
                break;
            default:
                break;
        }
    }
}
