using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player02EventAnimation : MonoBehaviour
{
    public Rigidbody rb;
    public Player02Movement player02Movement;
    public float jumpForce = 0;
    public float forceAmount = 0;
    public float QCFHS_force = 0;
    public float dashforce = 0;
    public float dashBackforce = 0;
    //public float HurtForce = 0;

    public void JumpEvent()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public void SpecialkickEvent()
    {
        if(player02Movement.faceRight)
        {
            rb.AddForce(Vector3.right * forceAmount, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(Vector3.left * forceAmount, ForceMode.Impulse);
        }
    }

    public void SpecialQCFHSEvent()
    {
        if(player02Movement.faceRight)
        {
            rb.AddForce(Vector3.right * QCFHS_force, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(Vector3.left * QCFHS_force, ForceMode.Impulse);
        }
    }
    public void DashForwardEvent()
    {
        if(player02Movement.faceRight)
        {
            rb.AddForce(Vector3.right * dashforce, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(Vector3.left * dashforce, ForceMode.Impulse);
        }
    }

    public void DashBackwardEvent()
    {
        if(player02Movement.faceRight)
        {
            rb.AddForce(Vector3.left * dashBackforce, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(Vector3.right * dashBackforce, ForceMode.Impulse);
        }
    }

    /*public void HurtEvent()
    {
        if(player02Movement.faceRight)
        {
            rb.AddForce(Vector3.left * HurtForce, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(Vector3.right * HurtForce, ForceMode.Impulse);
        }
    }*/
    public void Hurt(float Hurtforce)
    {
         if(player02Movement.faceRight)
        {
            rb.AddForce(Vector3.left * Hurtforce, ForceMode.Impulse);
            Debug.Log(Hurtforce);
        }
        else
        {
            rb.AddForce(Vector3.right * Hurtforce, ForceMode.Impulse);
        }
    }
    
}
