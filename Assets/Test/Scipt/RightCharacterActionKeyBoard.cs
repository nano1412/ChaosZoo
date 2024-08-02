using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightCharacterActionKeyBoard : MonoBehaviour
{
    public RightLookEnemy rightLookEnemy;
    public bool facingLeft = true;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            if(Input.GetKey(KeyCode.S))
            {
                Debug.Log("Crouch and Punch");
            }
            else if(!IsGrounded())
            {
                Debug.Log("Airbone Punch");
            }
            else if(facingLeft)
            {
                if(Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.U))
                {
                    Debug.Log("Special Punch");
                }
                else
                {
                    Debug.Log("Punch");
                }
            }
            else if(!facingLeft)
            {
                if(Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.U))
                {
                    Debug.Log("Speacial Punch");
                }
                else
                {
                    Debug.Log("Punch");
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.I))
        {
            if(Input.GetKey(KeyCode.S))
            {
                Debug.Log("Crouch and Kick");
            }
            else if(!IsGrounded())
            {
                Debug.Log("Airbone Kick");
            }
            else if(facingLeft)
            {
                if(Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.I))
                {
                    Debug.Log("Speacial Kick");
                }
                else
                {
                    Debug.Log("Kick");
                }
            }
            else if(!facingLeft)
            {
                if(Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.I))
                {
                    Debug.Log("Special Kick");
                }
                else
                {
                    Debug.Log("Kick");
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.O))
        {
            if(Input.GetKey(KeyCode.S))
            {
                Debug.Log("Crouch and slash");
            }
            else if(!IsGrounded())
            {
                Debug.Log("Airbone slash");
            }
            else if(facingLeft)
            {
                if(Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.O))
                {
                    Debug.Log("Speacial slash");
                }
                else
                {
                    Debug.Log("slash");
                }
            }
            else if(!facingLeft)
            {
                if(Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.O))
                {
                    Debug.Log("Special slash");
                }
                else
                {
                    Debug.Log("slash");
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(Input.GetKey(KeyCode.S))
            {
                Debug.Log("Crouch and heavly slash");
            }
            else if(!IsGrounded())
            {
                Debug.Log("Airbone heavly slash");
            }
            else if(facingLeft)
            {
                if(Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.P))
                {
                    Debug.Log("Speacial heavly slash");
                }
                else
                {
                    Debug.Log("heavly slash");
                }
            }
            else if(!facingLeft)
            {
                if(Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.P))
                {
                    Debug.Log("Special heavly slash");
                }
                else
                {
                    Debug.Log("heavly slash");
                }
            }
        }
            
    }
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}
