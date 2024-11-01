using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player01TakeAction : MonoBehaviour
{
    public float defaultActionCooldown = 0.5f; // เวลา default
    public GameObject player01;
    public Player01Movement player01Movement;
    public SelectController selectController;
    public bool isPerformingAction = false;
    public static bool Hits = false;
    public bool hits => Hits;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isPerformingAction) return;

        if (selectController.Selectjoystick01)
        {
            if (Input.GetButtonDown("Player01Joystick01"))
            {
                PerformAction("Punch");
                Hits = false;
            }
            if (Input.GetButtonDown("Player01Joystick02"))
            {
                PerformAction("Kick");
                Hits = false;
            }
            if (Input.GetButtonDown("Player01Joystick03"))
            {
                PerformAction("Slash");
                Hits = false;
            }
            if (Input.GetButtonDown("Player01Joystick04"))
            {
                PerformAction("HeavySlash");
                Hits = false;
            }
        }
        else
        {
            if (Input.GetButtonDown("Player01Bt01"))
            {
                PerformAction("Punch");
                Hits = false;
            }
            if (Input.GetButtonDown("Player01Bt02"))
            {
                PerformAction("Kick");
                Hits = false;
            }
            if (Input.GetButtonDown("Player01Bt03"))
            {
                PerformAction("Slash");
                Hits = false;
            }
            if (Input.GetButtonDown("Player01Bt04"))
            {
                PerformAction("HeavySlash");
                Hits = false;
            }
        }
    }

    private void PerformAction(string actionName)
    {   
        isPerformingAction = true;
        player01Movement.isPerformingAction = true;

        string verticalInput = selectController.Selectjoystick01 ? "LeftAnalogY1" : "Vertical";
        string horizontalInput = selectController.Selectjoystick01 ? "LeftAnalogX1" : "Horizontal";
        bool Joystick = selectController.Selectjoystick01 ? true : false;
        
        if(Joystick)
        {
            if (-Input.GetAxis(verticalInput) < -0.4f)
            {
                anim.SetTrigger("Crouch" + actionName + "Trigger");
            }
            else if (player01Movement.faceRight)
            {
                if (Input.GetAxis(horizontalInput) > 0.4f)
                {
                    anim.SetTrigger("Special" + actionName + "Trigger");
                }
                else
                {
                    anim.SetTrigger(actionName + "Trigger");
                }
            }
            else if (player01Movement.faceLeft)
            {
                if (Input.GetAxis(horizontalInput) < 0.4f)
                {
                    anim.SetTrigger("Special" + actionName + "Trigger");
                }
                else
                {
                    anim.SetTrigger(actionName + "Trigger");
                }
            }
        }

        else
        {
            if (Input.GetAxis(verticalInput) < -0.4f)
            {
                anim.SetTrigger("Crouch" + actionName + "Trigger");
                Debug.Log($"Crouch {actionName}");
            }
            else if (player01Movement.faceRight)
            {
                if (Input.GetAxis(horizontalInput) > 0.4f)
                {
                    anim.SetTrigger("Special" + actionName + "Trigger");
                    Debug.Log($"Special {actionName}");
                }
                else
                {
                    anim.SetTrigger(actionName + "Trigger");
                }
            }
            else if (player01Movement.faceLeft)
            {
                if (Input.GetAxis(horizontalInput) < 0f)
                {
                    anim.SetTrigger("Special" + actionName + "Trigger");
                    Debug.Log($"Special {actionName}");
                    
                }
                else
                {
                    anim.SetTrigger(actionName + "Trigger");
                }
            }
        }

        StartCoroutine(ResetIsPerformingAction(1f));
    }
    IEnumerator ResetIsPerformingAction(float delay)
    {
        yield return new WaitForSeconds(delay);
        isPerformingAction = false;
        player01Movement.isPerformingAction = false;
    }

    public void OnHits()
    {
        StopCoroutine(ResetIsPerformingAction(0));
        isPerformingAction = false;
        player01Movement.isPerformingAction = true;
    }
}
