using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that controls main menu behavior.
/// </summary>
public class MainMenuController : MonoBehaviour
{
    [Header("Main Menu Options")]
    public List<NavigatableMenuButton> options;

    void Start()
    {
        NavigatableMenuController.Instance?.SetActiveButtons(options);
    }
}