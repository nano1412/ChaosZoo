using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player02TakeAction : MonoBehaviour
{
    public float defaultActionCooldown = 0.5f;
    public GameObject player02;
    public Player02Movement player02Movement;
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

        if(selectController.Selectjoystick)
        {
            if (Input.GetButtonDown("Player02Joystick01"))
            {
                PerformAction("Punch");
                Hits = false;
            }
            if (Input.GetButtonDown("Player02Joystick02"))
            {
                PerformAction("Kick");
                Hits = false;
            }
            if (Input.GetButtonDown("Player02Joystick03"))
            {
                PerformAction("Slash");
                Hits = false;
            }
            if (Input.GetButtonDown("Player02Joystick04"))
            {
                PerformAction("HeavySlash");
                Hits = false;
            }
        }
        else
        {
            if (Input.GetButtonDown("Player02Bt01"))
            {
                PerformAction("Punch");
                Hits = false;
            }
            if (Input.GetButtonDown("Player02Bt02"))
            {
                PerformAction("Kick");
                Hits = false;
            }
            if (Input.GetButtonDown("Player02Bt03"))
            {
                PerformAction("Slash");
                Hits = false;
            }
            if (Input.GetButtonDown("Player02Bt04"))
            {
                PerformAction("HeavySlash");
                Hits = false;
            }
        }
    }

    private void PerformAction(string actionName)
    {   
        isPerformingAction = true;
        player02Movement.isPerformingAction = true;

        string verticalInput = selectController.Selectjoystick ? "VerticalJoystick" : "Vertical";
        string horizontalInput = selectController.Selectjoystick ? "HorizontalJoyStick" : "Horizontal";

        if (Input.GetAxis(verticalInput) < -0.4f)
        {
            anim.SetTrigger("Crouch" + actionName + "Trigger");
        }
        else if (player02Movement.faceRight)
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
        else if (player02Movement.faceLeft)
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

        StartCoroutine(ResetIsPerformingAction(0.5f));
    }

    IEnumerator ResetIsPerformingAction(float delay)
    {
        yield return new WaitForSeconds(delay);
        isPerformingAction = false;
        player02Movement.isPerformingAction = false;
    }

    public void OnHits()
    {
        StopCoroutine(ResetIsPerformingAction(0));
        isPerformingAction = false;
        player02Movement.isPerformingAction = true;
    }
}
