using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a button that can be navigated with keyboard or controller.
/// </summary>
public abstract class NavigatableMenuButton : MonoBehaviour
{
    /// <summary>
    /// Defines what happens when this button is selected or 'highlighted.'
    /// </summary>
    public abstract void Select();
    /// <summary>
    /// Defines what happens when this button is deselected or 'dehighlighted.'
    /// </summary>
    public abstract void Deselect();
    /// <summary>
    /// Defines what happens when this button is chosen or 'pressed.'
    /// </summary>
    public abstract void Choose();
}
