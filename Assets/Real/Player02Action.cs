using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player02Action : MonoBehaviour
{
    /*public float Jumpspeed = 0.01f;
    public float actionCooldown = 1f;
    public GameObject Player02;
    public Player02Move player02Move;

    public SelectController selectController;
    public bool isPerformingAction = false;
    public static bool Hits = false;

    private Animator anim;
    private AnimatorStateInfo Player02Layer0;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(selectController.Selectjoystick)
        {
            if(Input.GetButtonDown("Player02Joystick01"))
            {
                PerformAction("Punch");
                Hits = false;
            }
            if(Input.GetButtonDown("Player02Joystick02"))
            {
                PerformAction("Kick");
                Hits = false;
            }
            if(Input.GetButtonDown("Player02Joystick03"))
            {
                PerformAction("Slash");
                Hits = false;
            }
            if(Input.GetButtonDown("Player02Joystick04"))
            {
                PerformAction("HeavySlash");
                Hits = false;
            }
        }
        else
        {
            if(Input.GetButtonDown("Player02Bt01"));
            {
                PerformAction("Punch");
                Hits = false;
            }
            if(Input.GetButtonDown("Player02Bt02"))
            {
                PerformAction("Kick");
                Hits = false;
            }
            if(Input.GetButtonDown("Player02Bt03"));
            {
                PerformAction("Slash");
                Hits = false;
            }
            if(Input.GetButtonDown("Player02Bt04"));
            {
                PerformAction("HeavySlash");
                Hits = false;
            }
        }
    }

    private void PerformAction(string actionName)
    {
        isPerformingAction = true;
        player02Move.isPerformingAction = true;

        string verticalInput = selectController.Selectjoystick ? "VerticalJoystick" : "Vertical";
        
        if(Input.GetAxis(verticalInput) < -0.4f)
        {
            Debug.Log(verticalInput);
            anim.SetTrigger("Crouch" + actionName + "Trigger");
        }
        else
        {
            anim.SetTrigger(actionName + "Trigger");
        }
    }*/
}
