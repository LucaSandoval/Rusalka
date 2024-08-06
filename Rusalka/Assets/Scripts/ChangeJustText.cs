using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeJustText : MonoBehaviour
{
    private TextMeshProUGUI textField;
    [SerializeField] private string english;
    [SerializeField] private string polish;
    [SerializeField] private string german;
    [SerializeField] private string turkish;
    [SerializeField] private string spanish;
    [SerializeField] private string ukrainian;
    void Start()
    {
        textField = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (GlobalSettings.GlobalLanguage)
        {
            case Language.English:
                textField.text = english;
                break;
            case Language.Polish:
                textField.text = polish;
                break;
            case Language.German:
                textField.text = german;
                break;
            case Language.Turkish:
                textField.text = turkish;
                break;
            case Language.Spanish:
                textField.text = spanish;
                break;
            case Language.Ukrainian:
                textField.text = ukrainian;
                break;
            default:
                break;
        }
    }
}
