using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectController : MonoBehaviour
{
    public CharacterMovementKeyBoard characterMovementKeyboard; 
    public CharacterMovementJoyStick characterMovementJoystick; 
    public GameObject panel; 

    public void SelectKeyboard()
    {
        Debug.Log("SelectKeyboard called");
        panel.SetActive(false);

        if (characterMovementJoystick != null)
        {
            characterMovementJoystick.enabled = false;
        }

        if (characterMovementKeyboard != null)
        {
            characterMovementKeyboard.enabled = true;
        }
    }

    public void SelectJoystick()
    {
        Debug.Log("SelectJoystick called");
        panel.SetActive(false);

        if (characterMovementKeyboard != null)
        {
            characterMovementKeyboard.enabled = false;
        }
        if (characterMovementJoystick != null)
        {
            characterMovementJoystick.enabled = true;
        }
    }
}