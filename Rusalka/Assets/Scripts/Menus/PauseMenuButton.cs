using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuButton : NavigatableMenuButton
{
    public Image bgImage;
    public override void Choose()
    {
        Debug.Log("Chose Button!");
        switch (gameObject.name)
        {
            case "Resume":
                PauseController.Instance.SetGamePause(!PauseController.Instance.IsGamePaused());
                break;
            case "Settings":
                PauseController.Instance.ChangeActiveButtons(pauseState.Settings);
                break;
            case "Language":
                GlobalSettings.GlobalLanguage = (Language)(((int)GlobalSettings.GlobalLanguage + 1) % 6);
                break;
            case "Back":
                PauseController.Instance.ChangeActiveButtons(pauseState.Menu);
                break;
            case "MainMenu":
                PauseController.Instance.SetGamePause(!PauseController.Instance.IsGamePaused());
                SceneManager.LoadScene(0);
                break;
        }
    }

    public override void Deselect()
    {
        bgImage.CrossFadeColor(Color.white, 0f, false, true);
    }

    public override void Select()
    {
        bgImage.CrossFadeColor(new Color(1, 0.77f, 0.1f, 1f), 0.2f, false, true);
    }
}
