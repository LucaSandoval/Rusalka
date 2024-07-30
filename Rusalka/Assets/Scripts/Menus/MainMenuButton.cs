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
        Debug.Log("Chose Button!");
        if (gameObject.name == "LanguageButton")
        {
            switch (GlobalSettings.GlobalLanguage)
            {
                case Language.English: GlobalSettings.GlobalLanguage = Language.Polish; break;
                case Language.Polish: GlobalSettings.GlobalLanguage = Language.German; break;
                case Language.German: GlobalSettings.GlobalLanguage = Language.French; break;
                case Language.French: GlobalSettings.GlobalLanguage = Language.Spanish; break;
                case Language.Spanish: GlobalSettings.GlobalLanguage = Language.English; break;
                default: break;
            }
        }
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
