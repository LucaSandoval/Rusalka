using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlsTextChanger : MonoBehaviour
{
    private Language language = Language.English;
    /* Action names */
    [SerializeField] private string[] englishAction;
    [SerializeField] private string[] polishAction;
    [SerializeField] private string[] germanAction;
    [SerializeField] private string[] turkishAction;
    [SerializeField] private string[] spanishAction;
    [SerializeField] private string[] ukrainianAction;
    /* Keyboard Input */
    [SerializeField] private string[] enKeyboard;
    [SerializeField] private string[] plKeyboard;
    [SerializeField] private string[] deKeyboard;
    [SerializeField] private string[] tuKeyboard;
    [SerializeField] private string[] esKeyboard;
    [SerializeField] private string[] ukKeyboard;
    /* Xbox Controller Input */
    [SerializeField] private string[] enController;
    [SerializeField] private string[] plController;
    [SerializeField] private string[] deController;
    [SerializeField] private string[] tuController;
    [SerializeField] private string[] esController;
    [SerializeField] private string[] ukController;

    [SerializeField] private TextMeshProUGUI textField;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (language != GlobalSettings.GlobalLanguage){
            string text = "";
            switch (GlobalSettings.GlobalLanguage)
            {
            case Language.English:
                for(int i = 0; i < englishAction.Length; i++)
                {
                    text += englishAction[i];
                    text += " - ";
                    text += enKeyboard[i];
                    text += " | ";
                    text += enController[i];
                    text += "\n";
                }
                break;
            case Language.Polish:
                for (int i = 0; i < polishAction.Length; i++)
                {
                    text += polishAction[i];
                    text += " - ";
                    text += plKeyboard[i];
                    text += " | ";
                    text += plController[i];
                    text += "\n";
                }
                break;
            case Language.German:
                for (int i = 0; i < germanAction.Length; i++)
                 {
                    text += germanAction[i];
                    text += " - ";
                    text += deKeyboard[i];
                    text += " | ";
                    text += deController[i];
                    text += "\n";
                }
                break;
            case Language.Turkish:
                for (int i = 0; i < turkishAction.Length; i++)
                {
                    text += turkishAction[i];
                    text += " - ";
                    text += tuKeyboard[i];
                    text += " | ";
                    text += tuController[i];
                    text += "\n";
                }
                break;
            case Language.Spanish:
                for (int i = 0; i  < spanishAction.Length; i++)
                {
                    text += spanishAction[i];
                    text += " - ";
                    text += esKeyboard[i];
                    text += " | ";
                    text += esController[i];
                    text += "\n";
                }
                break;
            case Language.Ukrainian:
                for (int i = 0; i  < ukrainianAction.Length; i++)
                {
                    text += ukrainianAction[i];
                    text += " - ";
                    text += ukKeyboard[i];
                    text += " | ";
                    text += ukController[i];
                    text += "\n";
                }
                break;
            default:
                break;
            }
            textField.text = text;
            language = GlobalSettings.GlobalLanguage;
        }
    }
}
