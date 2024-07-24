using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton class allowing NivigatableMenuButtons to be navigated using a keyboard
/// or controller. 
/// </summary>
public class NavigatableMenuController : Singleton<NavigatableMenuController>
{
    private List<NavigatableMenuButton> activeButtons;
    private const float inputDelayMaxTime = 0.15f;
    private float inputDelayTimer;
    private int selectionId;

    protected override void Awake()
    {
        base.Awake();
        activeButtons = new List<NavigatableMenuButton>();
    }

    /// <summary>
    /// Sets the currently navigating buttons to the given list of buttons
    /// and automatically selects the first item.
    /// </summary>
    public void SetActiveButtons(List<NavigatableMenuButton> buttons)
    {
        activeButtons = new List<NavigatableMenuButton>(buttons);
        selectionId = 0;
        SelectCurrentButton();
    }

    /// <summary>
    /// Clears the currently navigating buttons.
    /// </summary>
    public void ClearActiveButtons()
    {
        foreach(NavigatableMenuButton button in activeButtons)
        {
            button.Deselect();
        }
        selectionId = 0;
        activeButtons.Clear();
    }

    // Selects the currently selected button.
    private void SelectCurrentButton()
    {
        if (activeButtons[selectionId] != null && selectionId < activeButtons.Count)
        {
            activeButtons[selectionId].Select();
        }
    }

    // Deselects the currently selected button.
    private void DeselectCurrentButton()
    {
        if (activeButtons[selectionId] != null && selectionId < activeButtons.Count)
        {
            activeButtons[selectionId].Deselect();
        }
    }

    // Chooses the currently selected button.
    private void ChooseCurrentButton()
    {
        if (activeButtons[selectionId] != null && selectionId < activeButtons.Count)
        {
            activeButtons[selectionId].Choose();
        }
    }

    // Changes the selection index by the given value, properly wrapping it around
    // the currently navigating buttons.
    private void ChangeSelectionId(int value)
    {
        selectionId += value;
        if (selectionId < 0) selectionId = activeButtons.Count - 1;
        if (selectionId >= activeButtons.Count) selectionId = 0;
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

    private void Update()
    {
        // Button selection
        if (activeButtons.Count > 0)
        {
            if (Input.GetAxisRaw("Vertical") > 0
                && !IsInputDelayed())
            {
                DeselectCurrentButton();
                ChangeSelectionId(-1);
                DelayInput();
            }
            if (Input.GetAxisRaw("Vertical") < 0
                && !IsInputDelayed())
            {
                DeselectCurrentButton();
                ChangeSelectionId(1);
                DelayInput();
            }
            SelectCurrentButton();

            if (Input.GetButtonDown("Jump"))
            {
                ChooseCurrentButton();
            }
        }

        if (IsInputDelayed())
        {
            inputDelayTimer -= Time.deltaTime;
        }
    }
}
