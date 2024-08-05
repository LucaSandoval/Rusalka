using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton class that controls pausing and unpausing the game.
/// </summary>
public enum pauseState
{
    Menu, Settings
}
public class PauseController : Singleton<PauseController>
{
    private bool GamePaused = false;
    [Header("Pause Menu Parent")]
    public GameObject PauseMenuParent;
    [Header("Pause Menu Children")]
    public GameObject PauseMenuButtons;
    public GameObject PauseMenuSettings;
    [Header("Buttons")]
    public List<NavigatableMenuButton> PauseOptions;
    public List<NavigatableMenuButton> SettingsOptions;

    /// <summary>
    /// Sets the pause state of the game- true for paused, false for unpaused.
    /// </summary>
    public void Start()
    {
        PauseMenuParent.SetActive(false);
    }
    public void SetGamePause(bool paused)
    {
        GamePaused = paused;
        PauseMenuParent.SetActive(paused);
        if (paused)
        {
            ChangeActiveButtons(pauseState.Menu);
        } else
        {
            NavigatableMenuController.Instance?.ClearActiveButtons();
            PauseMenuButtons.SetActive(false);
            PauseMenuSettings.SetActive(false);
        }
    }

    /// <summary>
    /// Returns true if the game is currently paused.
    /// </summary>
    public bool IsGamePaused()
    {
        return GamePaused;
    }

    public void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            SetGamePause(!IsGamePaused());
        }
    }
    public void ChangeActiveButtons(pauseState ps)
    {
        NavigatableMenuController.Instance?.ClearActiveButtons();
        switch(ps)
        {
            case pauseState.Menu:
                PauseMenuButtons.SetActive(true);
                PauseMenuSettings.SetActive(false);
                NavigatableMenuController.Instance?.SetActiveButtons(PauseOptions);
                break;
            case pauseState.Settings:
                PauseMenuButtons.SetActive(false);
                PauseMenuSettings.SetActive(true);
                NavigatableMenuController.Instance?.SetActiveButtons(SettingsOptions);
                break;
            default: 
                break;
        }
    }
}
