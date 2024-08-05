using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Language
{
    English, Polish, German, Turkish, Spanish, Ukrainian
}
public class GlobalSettings : Singleton<GlobalSettings>
{
    public static Language GlobalLanguage = Language.English;
    public static float MasterVolume = 1;
    public static float SFXVolume = 1;
    public static float MusicVolume = 1;
    public void changeMasterVolume(Slider slider) { MasterVolume = slider.value; }
    public void changeSFXVolume(Slider slider) { SFXVolume = slider.value; }
    public void changeMusicVolume(Slider slider) { MusicVolume = slider.value; }
    public float getMasterVolume() { return  MasterVolume; }
    public float getSFXVolume() {  return SFXVolume; }
    public float getMusicVolume() {  return MusicVolume; }
}
