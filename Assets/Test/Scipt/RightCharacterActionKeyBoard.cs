using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightCharacterActionKeyBoard : MonoBehaviour
{
    public RightLookEnemy rightLookEnemy;
    public bool facingLeft = true;

    private bool isPerformingAction = false;
    private float actionCooldown = 0.1f;

    private enum InputState { None, Down, Forward }
    private InputState inputState = InputState.None;


    void Update()
    {
        if(isPerformingAction)
        {
            return;
        }
        HandleQCF();

        if(Input.GetKeyDown(KeyCode.U))
        {
            PerformAction("Punch",KeyCode.U);
        }
        else if(Input.GetKeyDown(KeyCode.I))
        {
            PerformAction("Kick",KeyCode.I);
        }
        else if(Input.GetKeyDown(KeyCode.O))
        {
            PerformAction("Slash",KeyCode.O);
        }
        else if(Input.GetKeyDown(KeyCode.P))
        {
            PerformAction("Heavily Slash",KeyCode.P);
        }
    }
    private void HandleQCF()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            inputState = InputState.Down;
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKeyDown(facingLeft ? KeyCode.A : KeyCode.D))
        {
            inputState = InputState.Forward;
        }
        else if (Input.GetKeyUp(facingLeft ? KeyCode.A : KeyCode.D) && inputState == InputState.Forward)
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                Debug.Log("QCF " + (facingLeft ? "Left" : "Right") + " Special Action");
                // ทำการกระทำพิเศษที่ต้องการ
            }
            inputState = InputState.None; // รีเซ็ตสถานะ
        }
        else
        {
            inputState = InputState.None; // รีเซ็ตสถานะหากไม่ตรงตามลำดับ
        }
    }

    private void PerformAction(string actionName, KeyCode actionKey)
    {
        isPerformingAction = true;

        if(Input.GetKey(KeyCode.S))
        {
            Debug.Log("Crounch and" + actionName);
        }
        else if(!IsGrounded())
        {
            Debug.Log("Airbone" + actionName);
        }
        else if(facingLeft)
        {
            if(Input.GetKey(KeyCode.A))
            {
                Debug.Log("Special" + actionName);
            }
            else
            {
                Debug.Log(actionName);
            }
        }
        else if(!facingLeft)
        {
            if(Input.GetKey(KeyCode.D))
            {
                Debug.Log("Special" + actionName);
            }
            else
            {
                Debug.Log(actionName);
            }
        }

        StartCoroutine(ResetActionFlag());
    }
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    private IEnumerator ResetActionFlag()
    {
        yield return new WaitForSeconds(actionCooldown); // ปรับเวลาตามที่ต้องการ
        isPerformingAction = false; // รีเซ็ตตัวแปรหลังจากเวลาที่กำหนด
    }
}
