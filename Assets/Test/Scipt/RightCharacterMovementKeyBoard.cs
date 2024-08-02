using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightCharacterMovementKeyBoard : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 7f;
    public float jumpBackForce = 7f;
    public float doubleTapTime = 0.3f;
    public bool facingLeft = true;

    private bool canDash = true;
    public bool isDashing;
    public bool IsDashing => isDashing;
    public float dashingPower = 10f;
    public float dashingTime = 0.2f;
    public bool facingRight = true;

    private Rigidbody rb;
    private float lastTapTimeD = 0;
    private float lastTapTimeA = 0;
    private bool isRunning = false;

    public RightLookEnemy  rightLookEnemy;

    public float airtime = 5f; // ระยะเวลาที่ตัวละครอยู่กลางอากาศ
    public float dashStartTime; // เวลาที่ Dash เริ่มต้น

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

        if (!facingLeft)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (Time.time - lastTapTimeD < doubleTapTime)
                {
                    if(!IsGrounded() && canDash == true)
                    {
                        StartCoroutine(Dash(Vector3.right));
                        Debug.Log("Dash air right"); 
                    }
                    isRunning = true;
                }
                lastTapTimeD = Time.time;
            }

            if (Input.GetKey(KeyCode.D))
            {
                if (isRunning && IsGrounded())
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
                if (Time.time - lastTapTimeA < doubleTapTime && canDash == true)
                {
                    StartCoroutine(Dash(Vector3.left));
                    Debug.Log("Dash left");
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
                    if(!IsGrounded() && canDash == true)
                    {
                        StartCoroutine(Dash(Vector3.left));
                        Debug.Log("Dash air left"); 
                    }
                    isRunning = true;
                }
                lastTapTimeA = Time.time;
            }

            if (Input.GetKey(KeyCode.A))
            {
                if (isRunning && IsGrounded())
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
                if (Time.time - lastTapTimeD < doubleTapTime && canDash == true)
                {
                    StartCoroutine(Dash(Vector3.right));
                    Debug.Log("Dash right");
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
            //Debug.Log("Crouch");
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

    private IEnumerator Dash(Vector3 direction)
    {
        canDash = false;
        isDashing = true;
        dashStartTime = Time.time; // บันทึกเวลาที่ Dash เริ่มต้น

        // Save the original gravity status
        bool originalUseGravity = rb.useGravity;

        // Temporarily disable gravity
        rb.useGravity = false;

        // Apply the dash force
        Vector3 dashForce = direction * dashingPower;
        rb.velocity = dashForce;

        // Wait for the dash duration
        yield return new WaitForSeconds(dashingTime);

        // Restore the original gravity status
        rb.useGravity = originalUseGravity;

        // Optionally reset the velocity if needed
        // rb.velocity = Vector3.zero;

        isDashing = false;
        canDash = true;
    }
}
