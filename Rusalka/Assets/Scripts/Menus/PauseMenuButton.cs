using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuButton : NavigatableMenuButton
{
    public Image bgImage;
    public override void Choose()
    {
        // add functionality later
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
