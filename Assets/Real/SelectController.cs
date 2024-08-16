using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectController : MonoBehaviour
{
    public GameObject player01;
    public bool SelectKeyBoard = true; // ค่าเริ่มต้นเป็นการใช้คีย์บอร์ด
    public bool Selectjoystick = false;

    void Start()
    {
        // ตั้งค่าควบคุมเริ่มต้น
        if (SelectKeyBoard)
        {
            Selectjoystick = false;
        }
        else if (Selectjoystick)
        {
            SelectKeyBoard = false;
        }
    }

    void Update()
    {
        // สลับการควบคุมระหว่างคีย์บอร์ดและจอยสติ๊ก
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SelectKeyBoard = true;
            Selectjoystick = false;
            Debug.Log("Switched to Keyboard control");
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            SelectKeyBoard = false;
            Selectjoystick = true;
            Debug.Log("Switched to Joystick control");
        }
    }
}
