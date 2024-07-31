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
            case "SoundButton":
                Debug.Log("Mute / Unmute");
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
        //bgImage.color = Color.white;
    }

    public override void Select()
    {
        bgImage.CrossFadeColor(Color.red, 0.2f, false, true);
        if (gameObject.name.Equals("SoundButton")){
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log("Increase Volume");
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("Decrease Volume");
            }
        }

    }
}
