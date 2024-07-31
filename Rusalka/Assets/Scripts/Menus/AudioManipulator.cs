using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManipulator : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider slider;
    [SerializeField] private string soundName;
    public void Start()
    {
        slider.value = 1;
    }
    public void SetMusicVolume(){
        if (mixer != null) mixer.SetFloat(soundName, getDecibels(slider.value));
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
    public AudioMixer getMixer () { return mixer; }
    public string getSoundName() { return soundName; }
}
