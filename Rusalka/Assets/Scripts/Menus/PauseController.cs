using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton class that controls pausing and unpausing the game.
/// </summary>
public class PauseController : Singleton<PauseController>
{
    private bool GamePaused;

    [Header("Pause Menu Parent")]
    public GameObject PauseMenuParent;
    [Header("Menu Options")]
    public List<NavigatableMenuButton> Options;

    /// <summary>
    /// Sets the pause state of the game- true for paused, false for unpaused.
    /// </summary>
    public void SetGamePause(bool paused)
    {
        GamePaused = paused;
        PauseMenuParent.SetActive(paused);
        if (paused)
        {
            NavigatableMenuController.Instance?.SetActiveButtons(Options);
        } else
        {
            NavigatableMenuController.Instance?.ClearActiveButtons();
        }
    }

    /// <summary>
    /// Returns true if the game is currently paused.
    /// </summary>
    public bool IsGamePaused()
    {
        return GamePaused;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            SetGamePause(!IsGamePaused());
        }
    }
}
