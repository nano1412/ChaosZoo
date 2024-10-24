using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectController : MonoBehaviour
{
    public bool SelectKeyBoard01 = true;
    public bool Selectjoystick01 = false;

    public bool Selectjoystick02 = true;
    public bool SelectKeyBoard02 = false;

    void Start()
    {
        // ตั้งค่าควบคุมเริ่มต้น
        if (SelectKeyBoard01)
        {
            Selectjoystick01 = false;
        }
        else if (Selectjoystick01)
        {
            SelectKeyBoard01 = false;
        }
        else if(SelectKeyBoard02)
        {
            Selectjoystick02 = false;
        }
        else if(Selectjoystick02)
        {
            SelectKeyBoard02 = false;
        }
    }

    void Update()
    {
        // สลับการควบคุมระหว่างคีย์บอร์ดและจอยสติ๊ก
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SelectKeyBoard01 = true;
            Selectjoystick01 = false;
            Debug.Log("Switched to Keyboard control player01");
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            SelectKeyBoard01 = false;
            Selectjoystick01 = true;
            Debug.Log("Switched to Joystick control player01");
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            SelectKeyBoard02 = true;
            Selectjoystick02 = false;
            Debug.Log("Switched to Keyboard control player02");
        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            SelectKeyBoard02 = false;
            Selectjoystick02 = true;
            Debug.Log("Switched to Joystick control player02");
        }
    }
}
