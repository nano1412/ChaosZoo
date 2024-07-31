using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightLookEnemy : MonoBehaviour
{
    public Transform dummy;
    public RightCharacterMovementKeyBoard rightCharacterMovementKeyBoard;
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
            if (directionToDummy.x < 0 && !rightCharacterMovementKeyBoard.facingLeft)
            {
                rightCharacterMovementKeyBoard.facingLeft = true;
                Flip();
            }
            else if (directionToDummy.x > 0 && rightCharacterMovementKeyBoard.facingLeft)
            {
                rightCharacterMovementKeyBoard.facingLeft = false;
                Flip();
            }
        }
    }
    private void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        rotation.y = rightCharacterMovementKeyBoard.facingLeft ? 180 : 0;
        transform.eulerAngles = rotation;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}

