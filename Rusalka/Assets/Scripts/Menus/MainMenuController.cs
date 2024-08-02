using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that controls main menu behavior.
/// </summary>
public class MainMenuController : MonoBehaviour
{
    [Header("Main Menu Options")]
    public List<NavigatableMenuButton> MenuOptions;
    public List<NavigatableMenuButton> SoundOptions;
    private bool menuActive = true;
    [SerializeField] GameObject menuButtons;
    [SerializeField] GameObject soundButtons;

    void Start()
    {
        NavigatableMenuController.Instance?.ClearActiveButtons();
        NavigatableMenuController.Instance?.SetActiveButtons(MenuOptions);
    }
    public void ChangeActiveButtons()
    {
        NavigatableMenuController.Instance?.ClearActiveButtons();
        if (menuActive)
        {
            menuButtons.SetActive(false);
            soundButtons.SetActive(true);
            NavigatableMenuController.Instance?.SetActiveButtons(SoundOptions);
        }
        else
        {
            menuButtons.SetActive(true);
            soundButtons.SetActive(false);
            NavigatableMenuController.Instance?.SetActiveButtons(MenuOptions);
        }
        menuActive = !menuActive;
    }
}