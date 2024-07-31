using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftLookEnemy : MonoBehaviour
{
    public Transform dummy;
    public LeftCharacterMovementKeyBoard movementScript; // อ้างอิงถึงสคริปต์ LeftCharacterMovementKeyBoard

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        LookAtDummy();
    }

    private void LookAtDummy()
    {
        Vector3 directionToDummy = dummy.position - transform.position;
        directionToDummy.y = 0;

        if (IsGrounded())
        {
            if (directionToDummy.x > 0 && !movementScript.facingRight)
            {
                movementScript.facingRight = true;
                Flip();
            }
            else if (directionToDummy.x < 0 && movementScript.facingRight)
            {
                movementScript.facingRight = false;
                Flip();
            }
        }
    }

    private void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        rotation.y = movementScript.facingRight ? 0 : 180;
        transform.eulerAngles = rotation;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}