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
        if 
        (
            Input.GetKeyDown(KeyCode.JoystickButton0) ||
            Input.GetKeyDown(KeyCode.JoystickButton1) ||
            Input.GetKeyDown(KeyCode.JoystickButton2) ||
            Input.GetKeyDown(KeyCode.JoystickButton3) ||
            Input.GetKeyDown(KeyCode.JoystickButton4) ||
            Input.GetKeyDown(KeyCode.JoystickButton5) ||
            Input.GetKeyDown(KeyCode.JoystickButton6) ||
            Input.GetKeyDown(KeyCode.JoystickButton7) ||
            Input.GetKeyDown(KeyCode.JoystickButton8) ||
            Input.GetKeyDown(KeyCode.JoystickButton9) ||
            Input.GetKeyDown(KeyCode.JoystickButton10) ||
            Input.GetKeyDown(KeyCode.JoystickButton11) ||
            Input.GetKeyDown(KeyCode.JoystickButton12) ||
            Input.GetKeyDown(KeyCode.JoystickButton13) ||
            Input.GetKeyDown(KeyCode.JoystickButton14) ||
            Input.GetKeyDown(KeyCode.JoystickButton15) ||
            Input.GetKeyDown(KeyCode.JoystickButton16) ||
            Input.GetKeyDown(KeyCode.JoystickButton17) ||
            Input.GetKeyDown(KeyCode.JoystickButton18) ||
            Input.GetKeyDown(KeyCode.JoystickButton19)
        )
        { 
            isUsingKeyboard = false; 
        }
        else if (Input.anyKeyDown)
        {
            isUsingKeyboard = true;
        }
    }

    public bool IsUsingKeyboard()
    {
        return isUsingKeyboard;
    }
}