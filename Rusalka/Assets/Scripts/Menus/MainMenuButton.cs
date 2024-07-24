using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A button on the main menu.
/// </summary>
public class MainMenuButton : NavigatableMenuButton
{
    public Image bgImage;
    public override void Choose()
    {
        Debug.Log("Chose button!");
    }

    public override void Deselect()
    {
        bgImage.color = Color.white;
    }

    public override void Select()
    {
        bgImage.color = Color.red;
    }
}
