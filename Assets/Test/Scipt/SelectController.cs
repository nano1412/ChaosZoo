using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectController : MonoBehaviour
{
    public CharacterMovementKeyBoard characterMovementKeyboard; // สคริปต์ควบคุมคีย์บอร์ด
    public CharacterMovementJoyStick characterMovementJoystick; // สคริปต์ควบคุมจอยสติ๊ก
    public GameObject panel; // UI Panel

    public void SelectKeyboard()
    {
        Debug.Log("SelectKeyboard called");
        panel.SetActive(false);

        // ปิดการทำงานของสคริปต์ Joystick
        if (characterMovementJoystick != null)
        {
            characterMovementJoystick.enabled = false;
        }

        // เปิดการทำงานของสคริปต์ Keyboard
        if (characterMovementKeyboard != null)
        {
            characterMovementKeyboard.enabled = true;
        }
    }

    public void SelectJoystick()
    {
        Debug.Log("SelectJoystick called");
        panel.SetActive(false);

        // ปิดการทำงานของสคริปต์ Keyboard
        if (characterMovementKeyboard != null)
        {
            characterMovementKeyboard.enabled = false;
        }

        // เปิดการทำงานของสคริปต์ Joystick
        if (characterMovementJoystick != null)
        {
            characterMovementJoystick.enabled = true;
        }
    }
}