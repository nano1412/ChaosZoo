using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightLookEnemy : MonoBehaviour
{
    public Transform dummy;
    public RightCharacterMovementKeyBoard rightCharacterMovementKeyBoard;
    public RightCharacterActionKeyBoard rightCharacterActionKeyBoard;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(IsGrounded() || rightCharacterMovementKeyBoard.IsDashing)
        {
            LookAtDummy();
        }
    }

    private void LookAtDummy()
    {
        Vector3 directionToDummy = dummy.position - transform.position;
        directionToDummy.y = 0;

        Vector3 directionToUse = directionToDummy;
        bool shouldFlip = false;

        if(rightCharacterMovementKeyBoard.IsDashing)
        {
            float airtime = rightCharacterMovementKeyBoard.airtime;
            float dashElapsedTime = Time.time - rightCharacterMovementKeyBoard.dashStartTime;

            float additionalDistance = 1f;
            directionToUse = directionToDummy.normalized * (directionToDummy.magnitude + additionalDistance);
            
            if(dashElapsedTime < airtime)
            {
                shouldFlip = true;
            }
        }
        else if(IsGrounded())
        {
            shouldFlip = true;
        }


        if (shouldFlip)
        {
            if (directionToDummy.x < 0 && !rightCharacterMovementKeyBoard.facingLeft)
            {
                rightCharacterMovementKeyBoard.facingLeft = true;
                rightCharacterActionKeyBoard.facingLeft = true;
                Flip();
            }
            else if (directionToDummy.x > 0 && rightCharacterMovementKeyBoard.facingLeft)
            {
                rightCharacterMovementKeyBoard.facingLeft = false;
                rightCharacterActionKeyBoard.facingLeft = false;
                Flip();
            }
        }
    }
    private void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        rotation.y = rightCharacterMovementKeyBoard.facingLeft ? 270 : 90;
        transform.eulerAngles = rotation;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}

