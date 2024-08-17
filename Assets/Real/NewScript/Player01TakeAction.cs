using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player01TakeAction : MonoBehaviour
{
    public float actionCooldown = 1f;
    public GameObject player01;
    public Player01Movement player01Movement;
    public SelectController selectController;
    public bool isPerformingAction = false;
    public static bool Hits = false;

    private Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(isPerformingAction) return;

        if(selectController.Selectjoystick)
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

        string verticalInput = selectController.Selectjoystick ? "VerticalJoystick" : "Vertical";
        string horizontalInput = selectController.Selectjoystick ? "HorizontalJoyStick" : "Horizontal";
        if (Input.GetAxis(verticalInput) < 0)
        {
            anim.SetTrigger("Crouch" + actionName + "Trigger");
        }
        else if(player01Movement.faceRight)
        {
            if(Input.GetAxis(horizontalInput) > 0)
            {
                anim.SetTrigger("Special" + actionName + "Trigger");
            }
            else
            {
                anim.SetTrigger(actionName + "Trigger");
            }
        }
        else if(player01Movement.faceLeft)
        {
            if(Input.GetAxis(horizontalInput) < 0)
            {
                anim.SetTrigger("Special" + actionName + "Trigger");
            }
            else
            {
                anim.SetTrigger(actionName + "Trigger");
            }
        }

        StartCoroutine(ResetIsPerformingAction());
    }

     IEnumerator ResetIsPerformingAction()
    {
        yield return new WaitForSeconds(actionCooldown);
        isPerformingAction = false;
        player01Movement.isPerformingAction = false;
    }

    public void OnHits()
    {
        StopCoroutine(ResetIsPerformingAction());
        isPerformingAction = false;
    }
}
