using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player01Action : MonoBehaviour
{
    public float Jumpspeed = 0.01f;
    public bool IsCrouch = false;
    public GameObject Player01;
    private Animator anim;
    private AnimatorStateInfo Player01Layer0;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        Player01Layer0 = anim.GetCurrentAnimatorStateInfo(0);

        if(Player01Layer0.IsTag("Motion"))
        {
            if(Input.GetButtonDown("Player01Bt01"))
            {
                PerformAction("Punch");
            }
            if(Input.GetButtonDown("Player01Bt02"))
            {
                PerformAction("Kick");
            }
            if (Input.GetButtonDown("Player01Bt03"))
            {
                PerformAction("Slash");
            }
            if (Input.GetButtonDown("Player01Bt04"))
            {
                //PerformAction("HeavySlash");
            }
        }
    }

    public void JumpUp()
    {
        //Player01.transform.Translate(0, Jumpspeed, 0);
    }

    private void PerformAction(string actionName)
    {
        if(Input.GetAxis("Vertical") < 0)
        {
            anim.SetTrigger("Crouch" + actionName + "Trigger");
        }
        else
        {
            anim.SetTrigger(actionName + "Trigger");
        }
    }
}
