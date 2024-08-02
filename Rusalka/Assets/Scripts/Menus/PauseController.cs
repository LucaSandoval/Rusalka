using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton class that controls pausing and unpausing the game.
/// </summary>
public class PauseController : Singleton<PauseController>
{
    private bool GamePaused = false;
    private bool notInSettings = true;
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
    public void SetGamePause(bool paused)
    {
        GamePaused = paused;
        PauseMenuParent.SetActive(paused);
        if (paused)
        {
            ChangeActiveButtons();
        } else
        {
            NavigatableMenuController.Instance?.ClearActiveButtons();
            PauseMenuButtons.SetActive(false);
            PauseMenuSettings.SetActive(false);
            notInSettings = true;
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
    public void ChangeActiveButtons()
    {
        NavigatableMenuController.Instance?.ClearActiveButtons();
        if (notInSettings)
        {
            PauseMenuButtons.SetActive(true);
            PauseMenuSettings.SetActive(false);
            NavigatableMenuController.Instance?.SetActiveButtons(PauseOptions);
        }
        else
        {
            PauseMenuButtons.SetActive(false);
            PauseMenuSettings.SetActive(true);
            NavigatableMenuController.Instance?.SetActiveButtons(SettingsOptions);
        }
        notInSettings = !notInSettings;
    }
}
