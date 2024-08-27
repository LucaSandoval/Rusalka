using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManipulator : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private string soundName;
    public void Start()
    {
        switch (name)
        {
            case "MasterVolume":
                slider.value = GlobalSettings.Instance.getMasterVolume();
                break;
            case "SFXVolume":
                slider.value = GlobalSettings.Instance.getSFXVolume();
                break;
            case "MusicVolume":
                slider.value = GlobalSettings.Instance.getMusicVolume();
                break;
        }
    }
    public float getCurrentVolume()
    {
        return slider.value;
    }
    private float getDecibels(float scale)
    {
        if (scale == 0f) return -80f;
        else return 20f * Mathf.Log10(scale);
    }

    public string getSoundName() { return soundName; }
}
