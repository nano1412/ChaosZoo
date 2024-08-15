using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player01Action : MonoBehaviour
{
    public float Jumpspeed = 0.01f;
    public float actionCooldown = 1f;
    public GameObject Player01;
    public Player01Move player01Move;
    public SelectController selectController; // อ้างอิงไปยัง SelectController script
    public bool isPerformingAction = false;

    private Animator anim;
    private AnimatorStateInfo Player01Layer0;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }


    
    void Update()
    {
        Player01Layer0 = anim.GetCurrentAnimatorStateInfo(0);

        if (isPerformingAction) return;

        // ตรวจสอบว่าเป็น Joystick หรือ Keyboard
        if (Player01Layer0.IsTag("Motion"))
        {
            if (selectController.Selectjoystick)
            {
                // สำหรับ Joystick
                if (Input.GetButtonDown("Player01Joystick01"))
                {
                    PerformAction("Punch");
                }
                if (Input.GetButtonDown("Player01Joystick02"))
                {
                    PerformAction("Kick");
                }
                if (Input.GetButtonDown("Player01Joystick03"))
                {
                    PerformAction("Slash");
                }
                if (Input.GetButtonDown("Player01Joystick04"))
                {
                    // PerformAction("HeavySlash");
                }
            }
            else
            {
                // สำหรับ Keyboard
                if (Input.GetButtonDown("Player01Bt01"))
                {
                    PerformAction("Punch");
                }
                if (Input.GetButtonDown("Player01Bt02"))
                {
                    PerformAction("Kick");
                }
                if (Input.GetButtonDown("Player01Bt03"))
                {
                    PerformAction("Slash");
                }
                if (Input.GetButtonDown("Player01Bt04"))
                {
                    // PerformAction("HeavySlash");
                }
            }
        }
    }

    public void JumpUp()
    {
        //Player01.transform.Translate(0, Jumpspeed, 0);
    }

    private void PerformAction(string actionName)
    {   
        isPerformingAction = true;
        player01Move.isPerformingAction = true;

        // ตรวจสอบการควบคุม Vertical ขึ้นอยู่กับการใช้ Joystick หรือ Keyboard
        string verticalInput = selectController.Selectjoystick ? "VerticalJoystick" : "Vertical";

        if (Input.GetAxis(verticalInput) < 0)
        {
            anim.SetTrigger("Crouch" + actionName + "Trigger");
        }
        else
        {
            anim.SetTrigger(actionName + "Trigger");
        }

        StartCoroutine(ResetIsPerformingAction());
    }

    IEnumerator ResetIsPerformingAction()
    {
        yield return new WaitForSeconds(actionCooldown);
        isPerformingAction = false;
        player01Move.isPerformingAction = false;
    }
}

