using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectController : MonoBehaviour
{
    public LeftCharacterMovementKeyBoard leftCharacterMovementKeyBoard; 
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

        if (leftCharacterMovementKeyBoard != null)
        {
            leftCharacterMovementKeyBoard.enabled = true;
        }
    }

    public void SelectJoystick()
    {
        Debug.Log("SelectJoystick called");
        panel.SetActive(false);

        if (leftCharacterMovementKeyBoard != null)
        {
            leftCharacterMovementKeyBoard.enabled = false;
        }
        if (characterMovementJoystick != null)
        {
            leftCharacterMovementKeyBoard.enabled = true;
        }
    }
}