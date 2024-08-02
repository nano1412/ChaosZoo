using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftCharacterActionKeyBoard : MonoBehaviour
{
    public LeftLookEnemy leftLookEnemy;
    public bool facingRight = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (Input.GetKey(KeyCode.S))
            {
                Debug.Log("Crouch and Punch");
            }
            else if (!IsGrounded())
            {
                Debug.Log("Airborne Punch");
            }
            else if(facingRight)
            {
                if (Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.U))
                {
                    Debug.Log("Special Punch");
                }
                else
                {
                    Debug.Log("Punch");
                }
            }
            else if(!facingRight)
            {
                if (Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.U))
                {
                    Debug.Log("Special Punch");
                }
                else
                {
                    Debug.Log("Punch");
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (Input.GetKey(KeyCode.S))
            {
                Debug.Log("Crouch and Kick");
            }
            else if (!IsGrounded())
            {
                Debug.Log("Airborne Kick");
            }
            else if(facingRight)
            {
                if (Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.I))
                {
                    Debug.Log("Special Kick");
                }
                else
                {
                    Debug.Log("Kick");
                }
            }
            else if(!facingRight)
            {
                if (Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.I))
                {
                    Debug.Log("Special Kick");
                }
                else
                {
                    Debug.Log("Kick");
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            if (Input.GetKey(KeyCode.S))
            {
                Debug.Log("Crouch and slash");
            }
            else if (!IsGrounded())
            {
                Debug.Log("Airborne slash");
            }
            else if(facingRight)
            {
                if (Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.O))
                {
                    Debug.Log("Special slash");
                }
                else
                {
                    Debug.Log("slash");
                }
            }
            else if(!facingRight)
            {
                if (Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.O))
                {
                    Debug.Log("Special slash");
                }
                else
                {
                    Debug.Log("slash");
                }
            }
        }


        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Input.GetKey(KeyCode.S))
            {
                Debug.Log("Crouch and heavly slash");
            }
            else if (!IsGrounded())
            {
                Debug.Log("Airborne heavly slash");
            }
            else if(facingRight)
            {
                if (Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.P))
                {
                    Debug.Log("Special heavly slash");
                }
                else
                {
                    Debug.Log("heavly slash");
                }
            }
            else if(!facingRight)
            {
                if (Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.P))
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
