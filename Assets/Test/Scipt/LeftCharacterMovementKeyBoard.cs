using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftCharacterMovementKeyBoard : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 7f;
    public float jumpBackForce = 7f;
    public float doubleTapTime = 0.3f;
    public bool facingRight = true;

    private Rigidbody rb;
    private float lastTapTimeD = 0;
    private float lastTapTimeA = 0;
    private bool isRunning = false;

    public LeftLookEnemy leftLookEnemy;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        float currentSpeed = walkSpeed;

        if (facingRight)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (Time.time - lastTapTimeD < doubleTapTime)
                {
                    isRunning = true;
                }
                lastTapTimeD = Time.time;
            }

            if (Input.GetKey(KeyCode.D))
            {
                if (isRunning)
                {
                    currentSpeed = runSpeed;
                    Debug.Log("Run");
                }
                transform.Translate(Vector3.right * currentSpeed * Time.deltaTime);
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                isRunning = false;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (Time.time - lastTapTimeA < doubleTapTime)
                {
                    JumpBackward();
                }
                lastTapTimeA = Time.time;
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * walkSpeed * Time.deltaTime);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (Time.time - lastTapTimeA < doubleTapTime)
                {
                    isRunning = true;
                }
                lastTapTimeA = Time.time;
            }

            if (Input.GetKey(KeyCode.A))
            {
                if (isRunning)
                {
                    currentSpeed = runSpeed;
                    Debug.Log("Run");
                }
                transform.Translate(Vector3.right * currentSpeed * Time.deltaTime);
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                isRunning = false;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (Time.time - lastTapTimeD < doubleTapTime)
                {
                    Debug.Log("JumpBackward");
                    JumpBackward();
                }
                lastTapTimeD = Time.time;
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.left * walkSpeed * Time.deltaTime);
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
            Vector3 jumpDirection = facingRight ? Vector3.left : Vector3.right;
            rb.AddForce(jumpDirection * jumpBackForce + Vector3.up * jumpForce / 2, ForceMode.Impulse); // กระโดดถอยหลังเล็กน้อย
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}
