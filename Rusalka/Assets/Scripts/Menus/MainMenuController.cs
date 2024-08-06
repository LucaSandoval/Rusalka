using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that controls main menu behavior.
/// </summary>
public enum Submenu
{
    Menu,
    Settings,
    Controls,
    Stop
}
public class MainMenuController : MonoBehaviour
{
    [Header("Main Menu Options")]
    public List<NavigatableMenuButton> MenuOptions;
    public List<NavigatableMenuButton> SoundOptions;
    public List<NavigatableMenuButton> Controls;
    [SerializeField] private GameObject menuButtons;
    [SerializeField] private GameObject soundButtons;
    [SerializeField] private GameObject controls;
    void Start()
    {
        ChangeActiveButtons(Submenu.Menu);
    }
    public void ChangeActiveButtons(Submenu submenu)
    {
        NavigatableMenuController.Instance?.ClearActiveButtons();
        switch (submenu)
        {
            case Submenu.Menu:
                menuButtons.SetActive(true);
                soundButtons.SetActive(false);
                controls.SetActive(false);
                NavigatableMenuController.Instance?.SetActiveButtons(MenuOptions);
                break;
            case Submenu.Settings:
                menuButtons.SetActive(false);
                soundButtons.SetActive(true);
                controls.SetActive(false);
                NavigatableMenuController.Instance?.SetActiveButtons(SoundOptions);
                break;
            case Submenu.Controls:
                menuButtons.SetActive(false);
                soundButtons.SetActive(false);
                controls.SetActive(true);
                NavigatableMenuController.Instance?.SetActiveButtons(Controls);
                break;
            default:
                menuButtons.SetActive(false);
                soundButtons.SetActive(false);
                controls.SetActive(false);
                break;
        }
    }
    /*public Submenu GetCurrentMenu()
    {
        return currentMenu;
    }*/
}