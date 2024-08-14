using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player01Move : MonoBehaviour
{

    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        //Horizontal walking left and right
        if(Input.GetAxis("Horizontal") > 0)
        {
            anim.SetBool("Forward", true);
        }
        if(Input.GetAxis("Horizontal") < 0)
        {
            anim.SetBool("Backward", true);
        }
        if(Input.GetAxis("Horizontal") == 0)
        {
            anim.SetBool("Forward", false);
            anim.SetBool("Backward", false);
        }

        //Vertical Jump and Crouch
        if(Input.GetAxis("Vertical") > 0)
        {
            anim.SetTrigger("Jump");
        }
        if(Input.GetAxis("Vertical") < 0)
        {
            anim.SetBool("Crouch", true);
        }
        if(Input.GetAxis("Vertical") == 0)
        {
            anim.SetBool("Crouch", false);
        }

    }
}
