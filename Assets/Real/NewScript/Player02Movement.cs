using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player02Movement : MonoBehaviour
{
    public float walkspeed = 0.2f;
    public float  jumpForce = 0.2f;
    public float walkThreshold = 0.1f;
    public float verticalThreshold = 0.1f;
    public float walkanimation;
    public GameObject player02;
    public GameObject oppenent;
    public Vector3 oppPosition;
    public Player02Health player02Health;
    public bool isPerformingAction = false;

    private Rigidbody rb;
    private Animator anim;
    private bool FaceingLeft = true;
    private bool FaceingRight = false;
    private bool animationCouch = false;
    private bool IsJumping = false;
    private bool IsDash = false;
    public bool Joystick = false;

    public bool faceLeft => FaceingLeft;
    public bool faceRight => FaceingRight;
    public bool animCrouch => animationCouch;

    public string horizontalInput = "Horizontal";
    public string verticalInput = "Vertical";

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        oppenent = GameObject.FindGameObjectWithTag("PlayerCharacter01Tpose");
        StartCoroutine(FaceLeft());
    }

    void Update()
    {
        if(isPerformingAction) return;

        oppPosition = oppenent.transform.position;
        if (oppPosition.x > player02.transform.position.x && !FaceingRight)
        {
            StartCoroutine(FaceRight());
        }
        else if (oppPosition.x < player02.transform.position.x && !FaceingLeft)
        {
            StartCoroutine(FaceLeft());
        }

        if(FindObjectOfType<SelectController>().SelectKeyBoard02)
        {
            horizontalInput = "Horizontal";
            verticalInput = "Vertical";
            walkThreshold = 0.1f;
            verticalThreshold = 0.1f;
            Joystick = false;
            
        }
        if(FindObjectOfType<SelectController>().Selectjoystick02)
        {
            horizontalInput = "LeftAnalogX2";
            verticalInput = "LeftAnalogY2";
            walkThreshold = 0.4f;
            verticalThreshold = 0.4f;
            Joystick = true;
        }

        HandleCrouch();
        HandleMovement();
        HandleJump();
        HandleDash();
    }

    public void HandleCrouch()
    {
        float verticalAxis = Input.GetAxis(verticalInput);
        float horizontalAxis = Input.GetAxis(horizontalInput);

        if(Joystick)
        {
            if(-verticalAxis < -verticalThreshold && IsGrounded())
            {
                animationCouch = true;
                anim.SetBool("Crouch", true);
                if(horizontalAxis < -walkThreshold && FaceingRight)
                {
                    player02Health.block = true;
                }
                else if(horizontalAxis > walkThreshold && FaceingLeft)
                {
                    player02Health.block = true;
                }
                else if(horizontalAxis == 0)
                {
                    player02Health.block = false;
                }
            }
            else
            {
                animationCouch = false;
                anim.SetBool("Crouch", false);
                player02Health.block = false;
            }
        }
        else
        {
             if(verticalAxis < -verticalThreshold && IsGrounded())
            {
                animationCouch = true;
                anim.SetBool("Crouch", true);
                if(horizontalAxis < -walkThreshold && FaceingRight)
                {
                    player02Health.block = true;
                }
                else if(horizontalAxis > walkThreshold && FaceingLeft)
                {
                    player02Health.block = true;
                }
                else if(horizontalAxis == 0)
                {
                    player02Health.block = false;

                }
            }
            else
            {
                animationCouch = false;
                anim.SetBool("Crouch", false);
                player02Health.block = false;
            }
        }
    }

    public void HandleMovement()
    {
        if(animationCouch) return;
        
        float horizontalAxis = Input.GetAxis(horizontalInput);
        if(horizontalAxis > walkThreshold)
        {
            walkanimation = walkspeed;
            anim.SetBool("canWalk", true);
            if(FaceingLeft)
            {
                player02Health.block = true;
            }
            transform.Translate(transform.right * walkspeed * Time.deltaTime);
        }
        else if(horizontalAxis < -walkThreshold)
        {
            walkanimation = -walkspeed;
            anim.SetBool("canWalk", true);
            if(FaceingRight)
            {
                player02Health.block = true;
            }
            transform.Translate(-transform.right * walkspeed * Time.deltaTime);
        }
        else
        {
            walkanimation = 0f;
            anim.SetBool("canWalk", false);
            player02Health.block = false;
        }
        anim.SetFloat("Speed", walkanimation);
    }

    private void HandleJump()
    {
        float verticalAxis = Input.GetAxis(verticalInput);
        if(Joystick)
        {
            if(-verticalAxis > verticalThreshold && !IsJumping && !animationCouch)
            {
                IsJumping = true;
                anim.SetTrigger("Jump");
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                walkspeed = 0.6f;
                StartCoroutine(JumpPause());
            }
        }
        else
        {
            if(verticalAxis > verticalThreshold && !IsJumping && !animationCouch)
            {
                IsJumping = true;
                anim.SetTrigger("Jump");
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                walkspeed = 0.6f;
                StartCoroutine(JumpPause());
            }
        }
    }
    private void HandleDash()
    {
        float horizontalAxis = Input.GetAxis(horizontalInput);
        
        if(FaceingRight)
        {
            if(Joystick)
            {
                if(horizontalAxis > walkThreshold && Input.GetButtonDown("Player02Joystick05") && !IsDash)
                {
                    anim.SetTrigger("DashForward");
                    IsDash = true;
                    StartCoroutine(DashPause());
                }
                else if(horizontalAxis < walkThreshold && Input.GetButtonDown("Player02Joystick05") && !IsDash)
                {
                    anim.SetTrigger("DashBackward");
                    IsDash = true;
                    StartCoroutine(DashPause());
                }
            }
            else
            {
                if(horizontalAxis > walkThreshold && Input.GetButtonDown("Player02Bt05") && !IsDash)
                {
                    anim.SetTrigger("DashForward");
                    IsDash = true;
                    StartCoroutine(DashPause());
                }
                else if(horizontalAxis < walkThreshold && Input.GetButtonDown("Player02Bt05") && !IsDash)
                {
                    anim.SetTrigger("DashBackward");
                    IsDash = true;
                    StartCoroutine(DashPause());
                }
            }
             }
        else
        {
            if(Joystick)
            {
                if(horizontalAxis < walkThreshold && Input.GetButtonDown("Player02Joystick05") && !IsDash)
                {
                    anim.SetTrigger("DashForward");
                    StartCoroutine(DashPause());
                }
                else if(horizontalAxis > walkThreshold && Input.GetButtonDown("Player02Joystick05") && !IsDash)
                {
                    anim.SetTrigger("DashBackward");
                    StartCoroutine(DashPause());
                }
            }
            else
            {
                if(horizontalAxis < walkThreshold && Input.GetButtonDown("Player02Bt05") && !IsDash)
                {
                    anim.SetTrigger("DashForward");
                    StartCoroutine(DashPause());
                }
                else if(horizontalAxis > walkThreshold && Input.GetButtonDown("Player02Bt05") && !IsDash)
                {
                    anim.SetTrigger("DashBackward");
                    StartCoroutine(DashPause());
                }
            }
        }
    } 


    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }


    IEnumerator JumpPause()
    {
        yield return new WaitForSeconds(1.0f);
        IsJumping = false;
        walkspeed = 0.2f;
    }
    IEnumerator DashPause()
    {
        yield return new WaitForSeconds(1.0f);
        IsDash = false;
    }

   IEnumerator FaceLeft()
    {
        if (!FaceingLeft)
        {
            FaceingLeft = true;
            FaceingRight = false;
            yield return new WaitForSeconds(0.15f);

            // Flip the character by inverting the scale on the X axis
            Vector3 newScale = transform.localScale;
            newScale.x = Mathf.Abs(newScale.x) * -1;  // Invert the X scale to face left
            transform.localScale = newScale;

            // Set weights: RightLayer = 0, LeftLayer = 1
            //anim.SetLayerWeight(1, 0);  // RightLayer
            //anim.SetLayerWeight(2, 1);  // LeftLayer
            anim.SetBool("FaceLeft", true);
            anim.SetBool("FaceRight", false);
            
        }
    }

    IEnumerator FaceRight()
    {
        if (!FaceingRight)
        {
            FaceingRight = true;
            FaceingLeft = false;
            yield return new WaitForSeconds(0.15f);

            // Reset the character scale to face right
            Vector3 newScale = transform.localScale;
            newScale.x = Mathf.Abs(newScale.x);  // Ensure the X scale is positive
            transform.localScale = newScale;

            // Set weights: RightLayer = 1, LeftLayer = 0
            //anim.SetLayerWeight(1, 1);  // RightLayer
            //anim.SetLayerWeight(2, 0);  // LeftLayer
            anim.SetBool("FaceLeft", false);
            anim.SetBool("FaceRight", true);
            
        }
    }
}
