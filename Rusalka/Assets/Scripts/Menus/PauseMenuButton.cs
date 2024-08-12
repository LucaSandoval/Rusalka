using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuButton : NavigatableMenuButton
{
    public Image bgImage;
    private bool isSelected;
    private Slider slider;
    private const float inputDelayMaxTime = 0.15f;
    private float inputDelayTimer;
    [SerializeField] private float selectTime = 0.15f;
    [SerializeField] private TextMeshProUGUI tmp;
    public void Start()
    {
        slider = GetComponentInChildren<Slider>();
    }
    public override void Choose()
    {
        Debug.Log("Chose Button!");
        switch (gameObject.name)
        {
            case "Resume":
                PauseController.Instance.SetGamePause(!PauseController.Instance.IsGamePaused());
                break;
            case "Settings":
                PauseController.Instance.ChangeActiveButtons(pauseState.Settings);
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
            case "Language":
                GlobalSettings.GlobalLanguage = (Language)(((int)GlobalSettings.GlobalLanguage + 1) % 6);
                break;
            case "Back":
                PauseController.Instance.ChangeActiveButtons(pauseState.Menu);
                break;
            case "MainMenu":
                PauseController.Instance.SetGamePause(!PauseController.Instance.IsGamePaused());
                SceneManager.LoadScene("PrologueGreybox");
                break;
        }
    }

    public override void Deselect()
    {
        bgImage.CrossFadeAlpha(1f, selectTime, false);
        isSelected = false;
        tmp.color = Color.black;
    }

    public override void Select()
    {
        bgImage.CrossFadeAlpha(0f, selectTime, false);
        isSelected = true;
        tmp.color = Color.white;
    }
    public override void InstantDeselect()
    {
        bgImage.CrossFadeAlpha(1f, 0f, false);
        isSelected = false;
        tmp.color = Color.black;
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
