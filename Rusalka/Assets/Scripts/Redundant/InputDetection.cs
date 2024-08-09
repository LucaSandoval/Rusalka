using UnityEngine;
using UnityEngine.UI;

public class InputDetection : MonoBehaviour
{
    private bool isUsingKeyboard = false;

    void Update()
    {
        DetectInputDevice();
    }

    void DetectInputDevice()
    {
        if (Input.GetKeyDown(KeyCode.A) || 
            Input.GetKeyDown(KeyCode.D) ||
            Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.RightArrow) ||
            Input.GetKeyDown(KeyCode.LeftArrow) ||
            Input.GetKeyDown(KeyCode.UpArrow) ||
            Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.Space) ||
            Input.GetKeyDown(KeyCode.Z)||
            Input.GetKeyDown(KeyCode.KeypadEnter)||
            Input.GetKeyDown(KeyCode.Mouse0))
        {
            isUsingKeyboard = true;
        }
        else if (Input.anyKeyDown)
        {
            isUsingKeyboard = false;
        }
    }

    public bool IsUsingKeyboard()
    {
        return isUsingKeyboard;
    }
}