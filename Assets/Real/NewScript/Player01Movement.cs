using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player01Movement : MonoBehaviour
{
    public float walkspeed = 0.5f;
    public float  jumpForce = 0.2f;
    public float walkThreshold = 0.1f;
    public float walkanimation;
    public GameObject player01;
    public GameObject oppenent;
    public Vector3 oppPosition;
    public bool isPerformingAction = false;

    private Rigidbody rb;
    private Animator anim;
    private bool canWalkleft = true;
    private bool canWalkright = true;
    private bool FaceingLeft = false;
    private bool FaceingRight = true;
    private bool animationCouch = false;
    private bool IsJumping = false;

    public bool faceLeft => FaceingLeft;
    public bool faceRight => FaceingRight;

    public string horizontalInput = "Horizontal";
    public string verticalInput = "Vertical";

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        StartCoroutine(FaceRight());

    }

    void Update()
    {
        if(isPerformingAction) return;

        oppPosition = oppenent.transform.position;
        if (oppPosition.x > player01.transform.position.x && !FaceingRight)
        {
            StartCoroutine(FaceRight());
        }
        else if (oppPosition.x < player01.transform.position.x && !FaceingLeft)
        {
            StartCoroutine(FaceLeft());
        }

        horizontalInput = "Horizontal";
        verticalInput = "Vertical";
        walkThreshold = 0.1f;
        if(FindObjectOfType<SelectController>().Selectjoystick)
        {
            horizontalInput = "HorizontalJoyStick";
            verticalInput = "VerticalJoystick";
            walkThreshold = 0.99f;
        }

        HandleCrouch();
        HandleMovement();
        HandleJump();
    }

    public void HandleCrouch()
    {
        float verticalAxis = Input.GetAxis(verticalInput);
        if(verticalAxis < -0.5f && IsGrounded())
        {
            animationCouch = true;
            anim.SetBool("Crouch", true);
        }
        else
        {
            animationCouch = false;
            anim.SetBool("Crouch", false);
        }
    }

    public void HandleMovement()
    {
        if(animationCouch) return;
        
        float horizontalAxis = Input.GetAxis(horizontalInput);
        if(horizontalAxis > walkThreshold && canWalkright)
        {
            walkanimation = walkspeed;
            anim.SetBool("canWalk", true);
            transform.Translate(transform.right * walkspeed * Time.deltaTime);
        }
        else if(horizontalAxis < -walkThreshold && canWalkleft)
        {
            walkanimation = -walkspeed;
            anim.SetBool("canWalk", true);
            transform.Translate(-transform.right * walkspeed * Time.deltaTime);
        }
        else
        {
            walkanimation = 0f;
            anim.SetBool("canWalk", false);
        }
        anim.SetFloat("Speed", walkanimation);
    }  
    private void HandleJump()
    {
        float verticalAxis = Input.GetAxis(verticalInput);

        if(verticalAxis > 0.5f && !IsJumping && !animationCouch)
        {
            IsJumping = true;
            anim.SetTrigger("Jump");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            StartCoroutine(JumpPause());
        }
    } 

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "WallLeft")
        {
            canWalkleft = false;
        }
        if (collision.gameObject.tag == "WallRight")
        {
            canWalkright = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "WallLeft")
        {
            canWalkleft = true;
        }
        if (collision.gameObject.tag == "WallRight")
        {
            canWalkright = true;
        }
    }

    IEnumerator JumpPause()
    {
        yield return new WaitForSeconds(1.0f);
        IsJumping = false;
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
            anim.SetLayerWeight(1, 0);  // RightLayer
            anim.SetLayerWeight(2, 1);  // LeftLayer
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
            anim.SetLayerWeight(1, 1);  // RightLayer
            anim.SetLayerWeight(2, 0);  // LeftLayer
        }
    }
}
