using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
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
        switch (gameObject.name)
        {
            case "StartButton":
                SceneManager.LoadScene(1);
                break;
            case "LanguageButton":
                // Changes Language to the next one in the Language enum class
                GlobalSettings.GlobalLanguage = (Language)(((int)GlobalSettings.GlobalLanguage + 1) % 5);
                break;
            case "SettingsButton":
                FindObjectOfType<MainMenuController>().ChangeActiveButtons();
                break;
            case "BackButton":
                FindObjectOfType<MainMenuController>().ChangeActiveButtons();
                break;
            case "ExitButton":
                Debug.Log("Game should close if its not the editor");
                Application.Quit();
                break;
            default:
                break;
        }
    }

    public override void Deselect()
    {
        bgImage.CrossFadeColor(Color.white, 0f, false, true);
    }

    public override void Select()
    {
        bgImage.CrossFadeColor(Color.red, 0.2f, false, true);
    }
}
