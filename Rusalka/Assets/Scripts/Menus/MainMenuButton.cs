using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/// <summary>
/// A button on the main menu.
/// </summary>
public class MainMenuButton : NavigatableMenuButton
{
    public Image bgImage;
    private bool isSelected;
    private Slider slider;
    private const float inputDelayMaxTime = 0.15f;
    private float inputDelayTimer;
    public void Start()
    {
        slider = GetComponentInChildren<Slider>();
    }
    public override void Choose()
    {
        Debug.Log("Chose Button!");
        switch (gameObject.name)
        {
            case "StartButton":
                SceneManager.LoadScene(1);
                break;
            case "LanguageButton":
                // Changes Language to the next one in the Language enum class
                GlobalSettings.GlobalLanguage = (Language)(((int)GlobalSettings.GlobalLanguage + 1) % 6);
                break;
            case "SettingsButton":
                FindObjectOfType<MainMenuController>().ChangeActiveButtons(Submenu.Settings);
                break;
            case "MasterVolume":
                slider.value = (GlobalSettings.Instance.getMasterVolume() == 0 ? 1f : 0f); 
                break;
            case "SFXVolume":
                slider.value = (GlobalSettings.Instance.getSFXVolume() == 0 ? 1f : 0f);
                break;
            case "MusicVolume":
                slider.value = (GlobalSettings.Instance.getMusicVolume() == 0 ? 1f : 0f);
                break;
            case "BackButton":
                FindObjectOfType<MainMenuController>().ChangeActiveButtons(Submenu.Menu);
                break;
            case "ControlsButton":
                FindObjectOfType<MainMenuController>().ChangeActiveButtons(Submenu.Controls);
                break;
            case "ExitButton":
                Debug.Log("Game should close if its not the editor");
                Application.Quit();
                break;
            default:
                break;
        }
    }

    public override void Deselect()
    {
        bgImage.CrossFadeColor(Color.white, 0f, false, true);
        isSelected = false;
    }

    public override void Select()
    {
        bgImage.CrossFadeColor(new Color(1, 0.77f, 0.1f, 1f), 0.2f, false, true);
        isSelected = true;
    }
    // Delays the player input. 
    private void DelayInput()
    {
        inputDelayTimer = inputDelayMaxTime;
    }

    // Checks if the input is currently being delayed.
    private bool IsInputDelayed()
    {
        return inputDelayTimer > 0;
    }
    public void Update()
    {
        if ((name != "MasterVolume" && name != "SFXVolume" && name != "MusicVolume") || !isSelected) return;
        if (Input.GetAxisRaw("Horizontal") != 0 && !IsInputDelayed())
            {
            switch (name)
            {
                case "MasterVolume":
                    slider.value = GlobalSettings.Instance.getMasterVolume() + 0.1f * (Input.GetAxisRaw("Horizontal") > 0 ? 1 : -1);
                    GlobalSettings.Instance.changeMasterVolume(slider);
                    DelayInput();
                    break;
                case "SFXVolume":
                    slider.value = GlobalSettings.Instance.getSFXVolume() + 0.1f * (Input.GetAxisRaw("Horizontal") > 0 ? 1 : -1);
                    GlobalSettings.Instance.changeSFXVolume(slider);
                    DelayInput();
                    break;
                case "MusicVolume":
                    slider.value = GlobalSettings.Instance.getMusicVolume() + 0.1f * (Input.GetAxisRaw("Horizontal") > 0 ? 1 : -1);
                    GlobalSettings.Instance.changeMusicVolume(slider);
                    DelayInput();
                    break;
            }
            }
        else if (IsInputDelayed()) inputDelayTimer -= Time.deltaTime;
    }
}
