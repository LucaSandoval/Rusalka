using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeText : MonoBehaviour
{
    private Language language;

    private TextMeshProUGUI textField;
    [SerializeField] private string english;
    [SerializeField] private string polish;
    [SerializeField] private string german;
    [SerializeField] private string french;
    [SerializeField] private string spanish;
    // Start is called before the first frame update
    void Start()
    {
        textField = GetComponentInChildren<TextMeshProUGUI>();
    }
    // Update is called once per frame
    void Update()
    {
        language = GlobalSettings.GlobalLanguage;
        switch(language)
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
            case Language.French:
                textField.text = french;
                break;
            case Language.Spanish:
                textField.text = spanish;
                break;
            default:
                break;
        }
    }
}
