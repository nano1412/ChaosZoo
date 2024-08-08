using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftCharacterMovementKeyBoard : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 7f;
    public float doubleTapTime = 0.3f;
    public float walkanimation;

    private bool canDash = true;
    public bool isDashing;
    public bool IsDashing => isDashing;
    public float dashingPower = 10f;
    public float dashingTime = 0.2f;
    public bool facingRight = true;
    public bool isPerformingAction = false;

    private Rigidbody rb;
    private float lastTapTimeD = 0;
    private float lastTapTimeA = 0;
    private bool isRunning = false;

    private Animator animator;
    private bool animetionCouch = false;
    private bool canJump = true;
    

    public float airtime = 5f; // ระยะเวลาที่ตัวละครอยู่กลางอากาศ
    public float dashStartTime; // เวลาที่ Dash เริ่มต้น

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>(); // เริ่มต้น Animator
    }

    void Update()
    {
        if(isPerformingAction) return;
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        float currentSpeed = 0f; // เริ่มต้นที่ความเร็วเป็น 0
        walkanimation = 0f; // ตั้งค่า walkanimation เป็น 0 ก่อนตรวจสอบการกดปุ่ม

        if (facingRight)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                if(animetionCouch)  return;
                
                if (Time.time - lastTapTimeD < doubleTapTime)
                {
                    if (!IsGrounded() && canDash)
                    {
                        StartCoroutine(Dash(Vector3.right)); // Dash ไปด้านขวาเมื่อหันขวาและอยู่กลางอากาศ
                        Debug.Log("Dash air right");
                    }
                    isRunning = true;
                }
                lastTapTimeD = Time.time;
            }

            if (Input.GetKey(KeyCode.D))
            {
                if(animetionCouch)  return;

                if (isRunning && IsGrounded())
                {
                    currentSpeed = runSpeed;
                    walkanimation = runSpeed;
                    Debug.Log("Run");
                }
                else
                {
                    currentSpeed = walkSpeed;
                    walkanimation = walkSpeed;
                }
                animator.SetBool("canWalk",true);
                transform.Translate(-transform.right * currentSpeed * Time.deltaTime);
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                if(animetionCouch)  return;

                isRunning = false;
                walkanimation = 0f;
                animator.SetBool("canWalk", false);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                if(animetionCouch)  return;

                if (Time.time - lastTapTimeA < doubleTapTime && canDash)
                {
                    StartCoroutine(Dash(Vector3.left)); // Dash ไปด้านซ้ายเมื่อหันขวาและอยู่กลางอากาศ
                    Debug.Log("Dash left");
                }
                lastTapTimeA = Time.time;
            }
            if (Input.GetKey(KeyCode.A))
            {
                if(animetionCouch)  return;

                currentSpeed = walkSpeed;
                walkanimation = -walkSpeed;
                animator.SetBool("canWalk",true);
                transform.Translate(transform.right * walkSpeed * Time.deltaTime);
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                animator.SetBool("canWalk", false);
                walkanimation = 0f; // ตั้งค่า walkanimation เป็น 0 เมื่อปล่อยปุ่ม
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if(animetionCouch)  return;

                if (Time.time - lastTapTimeA < doubleTapTime)
                {
                    if (!IsGrounded() && canDash)
                    {
                        StartCoroutine(Dash(Vector3.left)); // Dash ไปด้านซ้ายเมื่อหันซ้ายและอยู่กลางอากาศ
                        Debug.Log("Dash air left");
                    }
                    isRunning = true;
                }
                lastTapTimeA = Time.time;
            }

            if (Input.GetKey(KeyCode.A))
            {
                if(animetionCouch)  return;

                if (isRunning && IsGrounded())
                {
                    currentSpeed = runSpeed;
                    walkanimation = runSpeed;
                    Debug.Log("Run");
                }
                else
                {
                    currentSpeed = walkSpeed;
                    walkanimation = walkSpeed;
                }
                animator.SetBool("canWalk",true);
                transform.Translate(transform.right * currentSpeed * Time.deltaTime);
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                isRunning = false;
                animator.SetBool("canWalk",false);
                walkanimation = 0f; // ตั้งค่า walkanimation เป็น 0 เมื่อปล่อยปุ่ม
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if(animetionCouch)  return;

                if (Time.time - lastTapTimeD < doubleTapTime && canDash)
                {
                    StartCoroutine(Dash(Vector3.right)); // Dash ไปด้านขวาเมื่อหันซ้ายและอยู่กลางอากาศ
                    Debug.Log("Dash right");
                }
                lastTapTimeD = Time.time;
            }
            if (Input.GetKey(KeyCode.D))
            {
                if(animetionCouch)  return;

                currentSpeed = walkSpeed;
                walkanimation = -walkSpeed;
                animator.SetBool("canWalk",true);
                transform.Translate(-transform.right * walkSpeed * Time.deltaTime);
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                animator.SetBool("canWalk",false);
                walkanimation = 0f; // ตั้งค่า walkanimation เป็น 0 เมื่อปล่อยปุ่ม
            }
        }

        if (Input.GetKey(KeyCode.S) && IsGrounded())
        {
            //Debug.Log("Crouch");
            animetionCouch = true;
            animator.SetBool("Crouch", true);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            animetionCouch = false;
            animator.SetBool("Crouch", false);
        }

        // อัพเดทค่า Speed ใน Animator
        animator.SetFloat("Speed", walkanimation);
    }

    private void HandleJump()
    {
        if (Input.GetKey(KeyCode.W) && IsGrounded() && canJump)
        {
            animator.SetTrigger("Jump");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canJump = false;
        }

        if (IsGrounded())
        {
            canJump = true;
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

        // บันทึกสถานะการใช้งานแรงโน้มถ่วงเดิม
        bool originalUseGravity = rb.useGravity;

        // ปิดการใช้งานแรงโน้มถ่วงชั่วคราว
        rb.useGravity = false;

        // ใช้แรง Dash
        Vector3 dashForce = direction * dashingPower;
        rb.velocity = dashForce;

        // รอระยะเวลา Dash
        yield return new WaitForSeconds(dashingTime);

        // คืนค่าการใช้งานแรงโน้มถ่วงเดิม
        rb.useGravity = originalUseGravity;

        // อาจจะรีเซ็ตความเร็วหากจำเป็น
        // rb.velocity = Vector3.zero;

        isDashing = false;
        canDash = true;
    }
}
