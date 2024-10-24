using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player02Move : MonoBehaviour
{   
    public float walkSpeed = 0.0015f;
    public float Jumpspeed = 0f;
    public float walkThreshold = 0.1f;
    //public Player02Action player02Action;
    public GameObject player02;
    public GameObject opponent;
    public Vector3 oppPosition;
    //public bool isPerformingAction = false;

    private bool IsJumping = false;
    private Rigidbody rb;
    private Animator anim;
    private AnimatorStateInfo Player02Layer0;
    private bool canWalkleft = true;
    private bool canWalkright = true;
    private bool FaceingLeft = true;
    private bool FaceingRight = false;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        StartCoroutine(FaceLeft());
    }

    void Update()
    {
        Player02Layer0 = anim.GetCurrentAnimatorStateInfo(0);

        oppPosition = opponent.transform.position;

        if (oppPosition.x < player02.transform.position.x && !FaceingRight)
        {
            StartCoroutine(FaceRight());
        }
        else if (oppPosition.x > player02.transform.position.x && !FaceingLeft)
        {
            StartCoroutine(FaceLeft());
        }

        string horizontalInput = "Horizontal";
        string verticalInput = "Vertical";

        if(FindObjectOfType<SelectController>().Selectjoystick02)
        {
            horizontalInput = "HorizontalJoystick";
            verticalInput = "VecticalJoystick";
            walkThreshold = 0.99f;
        }

        float horizontalAxis = Input.GetAxis(horizontalInput);
        
        if(horizontalAxis > walkThreshold && canWalkright)
        {
            //anim.SetBool("Forward", true);
            transform.Translate(walkSpeed, 0, 0);
        }
        else if (horizontalAxis < -walkThreshold && canWalkleft)
        {
            //anim.SetBool("Backward", true);
            transform.Translate(-walkSpeed, 0, 0);
        }
        else
        {
            //anim.SetBool("Forward", false);
            //anim.SetBool("Backward", false);
        }
        

        float verticalAxis = Input.GetAxis(verticalInput);
        if(verticalAxis > 0.5f && !IsJumping)
        {
            IsJumping = true;
            //anim.SetTrigger("Jump");
            rb.AddForce(Vector3.up * Jumpspeed, ForceMode.Impulse);
            StartCoroutine(JumpPause());
        }
        else if (verticalAxis < -0.5f)
        {
            //anim.SetBool("Crouch", true);
        }
        else
        {
            //anim.SetBool("Crouch", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "WalkLeft")
        {
            canWalkleft = false;
        }
        if(collision.gameObject.tag == "WalkRight")
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

            
            Vector3 newScale = transform.localScale;
            newScale.x = Mathf.Abs(newScale.x) * -1;  
            transform.localScale = newScale;

            
            anim.SetLayerWeight(1, 0);  
            anim.SetLayerWeight(2, 1);  
        }
    }

    IEnumerator FaceRight()
    {
        if (!FaceingRight)
        {
            FaceingRight = true;
            FaceingLeft = false;
            yield return new WaitForSeconds(0.15f);

            
            Vector3 newScale = transform.localScale;
            newScale.x = Mathf.Abs(newScale.x);
            transform.localScale = newScale;

            
            anim.SetLayerWeight(1, 1);
            anim.SetLayerWeight(2, 0);
        }
    }
}

