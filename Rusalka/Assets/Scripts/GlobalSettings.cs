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
    private static float MasterVolume = 1;
    private static float SFXVolume = 1;
    private static float MusicVolume = 1;

    public void changeMasterVolume(Slider slider) { changeMasterVolume(slider.value); }
    public void changeMasterVolume(float f) { MasterVolume = (f < 0 ? 0 : (f > 1 ? 1 : f)); }
    public void changeSFXVolume(Slider slider) { changeSFXVolume(slider.value); }
    public void changeSFXVolume(float f) { SFXVolume = (f < 0 ? 0 : (f > 1 ? 1 : f)); }
    public void changeMusicVolume(Slider slider) { changeMusicVolume(slider.value); }
    public void changeMusicVolume(float f) { MusicVolume = (f < 0 ? 0 : (f > 1 ? 1 : f)); }
    public float getMasterVolume() { return  MasterVolume; }
    public float getSFXVolume() {  return SFXVolume; }
    public float getMusicVolume() {  return MusicVolume; }
}
