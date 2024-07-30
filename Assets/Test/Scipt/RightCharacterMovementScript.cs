using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightCharacterMovementScript : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 7f;
    public float jumpBackForce = 7f;
    public Transform dummy;
    public float doubleTapTime = 0.3f; // เวลาในการจับ double tap

    private Rigidbody rb;
    private bool isGrounded;
    private float lastTapTimeD = 0;
    private float lastTapTimeA = 0;
    private bool isRunning = false;
    public bool facingLeft = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        LookAtDummy();
    }

    private void HandleMovement()
    {
        float currentSpeed = walkSpeed;

        if(facingLeft == true)
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                if(Time.time - lastTapTimeA < doubleTapTime)
                {
                    isRunning = true;
                }
                lastTapTimeA = Time.time;
            }
            if(Input.GetKey(KeyCode.A))
            {
                if(isRunning)
                {
                    currentSpeed = runSpeed;
                    Debug.Log("Run");
                }
                transform.Translate(Vector3.right * currentSpeed * Time.deltaTime);
            }
            if(Input.GetKeyUp(KeyCode.A))
            {
                isRunning = false;
            }
            if(Input.GetKeyDown(KeyCode.D))
            {
                if(Time.time - lastTapTimeD < doubleTapTime)
                {
                    JumpBackward();
                }
                lastTapTimeD = Time.time;
            }
            if(Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.left * walkSpeed * Time.deltaTime);
            }
        }
        
        else if(facingLeft == false)
        {
            if(Input.GetKeyDown(KeyCode.D))
            {
                if(Time.time - lastTapTimeD < doubleTapTime)
                {
                    isRunning = true;
                }
                lastTapTimeD = Time.time;
            }
            if(Input.GetKey(KeyCode.D))
            {
                if(isRunning)
                {
                    currentSpeed = runSpeed;
                    Debug.Log("Run");
                }
                transform.Translate(Vector3.left * currentSpeed * Time.deltaTime);
            }
            if(Input.GetKeyUp(KeyCode.D))
            {
                isRunning = false;
            }
            if(Input.GetKeyDown(KeyCode.A))
            {
                if(Time.time - lastTapTimeA < doubleTapTime)
                {
                    JumpBackward();
                }
                lastTapTimeA = Time.time;
            }
            if(Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.right * walkSpeed * Time.deltaTime);
            }
        }
        if (Input.GetKey(KeyCode.S) && IsGrounded())
        {
            Debug.Log("Crouch");
        }
    }

    private void HandleJump()
    {
        if (Input.GetKey(KeyCode.W) && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void JumpBackward()
    {
        if (IsGrounded())
        {
            Vector3 jumpDirection = facingLeft ? Vector3.right : Vector3.left;
            rb.AddForce(jumpDirection * jumpBackForce + Vector3.up * jumpForce / 2, ForceMode.Impulse); // กระโดดถอยหลังเล็กน้อย
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    private void LookAtDummy()
    {
        Vector3 directionToDummy = dummy.position - transform.position;
        directionToDummy.y = 0;

        Vector3 currentScale = transform.localScale;

        if (IsGrounded())
        {
            if (directionToDummy.x < 0 && !facingLeft)
            {
                facingLeft = true;
                Flip();
                Debug.Log("Right");
            }
            else if (directionToDummy.x > 0 && facingLeft)
            {
                facingLeft = false;
                Flip();
                Debug.Log("Left");
            }
        }
    }

    private void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
    }
}
