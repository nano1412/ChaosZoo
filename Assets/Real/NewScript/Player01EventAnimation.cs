using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player01EventAnimation : MonoBehaviour
{   
    public Rigidbody rb;
    public Player01Movement player01Movement;
    public float jumpForce = 0;
    public float forceAmount = 0;
    public float QCFHS_force = 0;
    public float dashforce = 0;
    public float dashBackforce = 0;
    public float HurtForce = 0;
    public float forcehurt;
    public GameObject Rpg;
    public GameObject spawnPoint;
    public void JumpEvent()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public void SpecialkickEvent()
    {
        if(player01Movement.faceRight)
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
        if(player01Movement.faceRight)
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
        if(player01Movement.faceRight)
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
        if(player01Movement.faceRight)
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
        if(player01Movement.faceRight)
        {
            rb.AddForce(Vector3.left * HurtForce, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(Vector3.right * HurtForce, ForceMode.Impulse);
        }
    }*/

    public void Hurt()
    {
        if(player01Movement.faceRight)
        {
            rb.AddForce(Vector3.left * forcehurt, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(Vector3.right * forcehurt, ForceMode.Impulse);
        }
    }

    public void SlideFront()
    {
        if(player01Movement.faceRight)
        {
            rb.AddForce(Vector3.right * dashforce, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(Vector3.left * dashforce, ForceMode.Impulse);
        }
    }
    
    public void SlideBack()
    {
        if(player01Movement.faceRight)
        {
            rb.AddForce(Vector3.left * dashBackforce, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(Vector3.right * dashBackforce, ForceMode.Impulse);
        }
    }

    public void RPGTime()
    {
        Instantiate(Rpg, spawnPoint.transform.position, spawnPoint.transform.rotation);
    }



}
