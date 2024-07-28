
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementKeyBoard : MonoBehaviour
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
    private bool facingRight = true;

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

        if(facingRight)
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
            // เช็คการ double-tap สำหรับ A
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (Time.time - lastTapTimeA < doubleTapTime)
                {
                    JumpBackward();
                }
                lastTapTimeA = Time.time;
            }

        }
       else if(!facingRight)
       {
            if (Input.GetKeyDown(KeyCode.A))
                {
                    if (Time.time - lastTapTimeD < doubleTapTime)
                    {
                        isRunning = true;
                    }
                    lastTapTimeD = Time.time;
                }

                if (Input.GetKey(KeyCode.A))
                {
                    if (isRunning)
                    {
                        currentSpeed = runSpeed;
                        Debug.Log("Run");
                    }
                    transform.Translate(Vector3.left * currentSpeed * Time.deltaTime);
                }
                if (Input.GetKeyUp(KeyCode.A))
                {
                    isRunning = false;
                }
                // เช็คการ double-tap สำหรับ A
                if (Input.GetKeyDown(KeyCode.D))
                {
                    if (Time.time - lastTapTimeA < doubleTapTime)
                    {
                        Debug.Log("JumpBackward");
                        JumpBackward();
                    }
                    lastTapTimeA = Time.time;
                }
       }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * walkSpeed * Time.deltaTime);
        }

        // การย่อตัวเมื่อกดปุ่ม S
        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("Crouch");
            // โค้ดย่อตัวตัวละครสามารถเพิ่มตรงนี้
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

    private void LookAtDummy()
    {
        Vector3 directionToDummy = dummy.position - transform.position;
        directionToDummy.y = 0;

        Vector3 currentScale = transform.localScale;

        if (IsGrounded())
        {
            if (directionToDummy.x > 0 && !facingRight)
            {
                facingRight = true;
                Flip();
            }
            else if (directionToDummy.x < 0 && facingRight)
            {
                facingRight = false;
                Flip();
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