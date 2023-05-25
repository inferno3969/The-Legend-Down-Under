using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public enum InputType
{
    Keyboard,
    Xbox,
    PlayStation,
    Other
}

public class SaveControllerInput : MonoBehaviour
{
    [Header("Input")]
    public InputDevice inputDevice;
    public InputType inputType;

    void FixedUpdate()
    {
        inputDevice = InputManager.ActiveDevice;
        GetControllerType();
    }

    void OnDetached()
    {
        inputType = InputType.Keyboard;
    }

    private InputType GetControllerType()
    {
        if (inputDevice.Name.Contains("Sony") && inputDevice.IsAttached)
        {
            inputType = InputType.PlayStation;
            //Debug.Log("PlayStation");
        }
        if (inputDevice.Name.Contains("Microsoft") && inputDevice.IsAttached)
        {
            inputType = InputType.Xbox;
            //Debug.Log("Xbox");
        }
        if (!inputDevice.IsAttached || Input.anyKey && !inputDevice.IsActive)
        {
            inputType = InputType.Keyboard;
            //Debug.Log("Keyboard");
        }
        //Debug.Log("InputType: " + inputType);
        return inputType;
    }
}
