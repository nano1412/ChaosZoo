using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player01Move : MonoBehaviour
{
    public float walkSpeed = 0.0015f;
    public float JumpSpeed = 0.02f;
    public GameObject player01;
    public GameObject opponent;
    public Vector3 oppPosition;

    private bool IsJumping = false;
    private Rigidbody rb;
    private Animator anim; 
    private AnimatorStateInfo Player01Layer0;
    private bool canWalkleft = true;
    private bool canWalkright = true;
    private bool FaceingLeft = false;
    private bool FaceingRight = true;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        StartCoroutine(FaceRight());
    }

    void Update()
    {
        Player01Layer0 = anim.GetCurrentAnimatorStateInfo(0);

        //Vector3 ScreenBounds = Camera.main.WorldToScreenPoint(this.transform.position);

        /*if(ScreenBounds.x > Screen.width)
        {
            canWalkright = false;
        }
        if(ScreenBounds.x < 0)
        {
            canWalkleft = false;
        }
        else
        {
            canWalkleft = true;
            canWalkright = true;
        }*/ 

        //Get the oppenet position
        oppPosition = opponent.transform.position;
        //Facing left or right
         if (oppPosition.x > player01.transform.position.x && !FaceingRight)
        {
            StartCoroutine(FaceRight());
        }
        else if (oppPosition.x < player01.transform.position.x && !FaceingLeft)
        {
            StartCoroutine(FaceLeft());
        }

        // Horizontal walking left and right
        if (Player01Layer0.IsTag("Motion"))
        {
            if (Input.GetAxis("Horizontal") > 0 && canWalkright)
            {
                anim.SetBool("Forward", true);
                transform.Translate(walkSpeed, 0, 0);
            }
            else if (Input.GetAxis("Horizontal") < 0 && canWalkleft)
            {
                anim.SetBool("Backward", true);
                transform.Translate(-walkSpeed, 0, 0);
            }
            else
            {
                anim.SetBool("Forward", false);
                anim.SetBool("Backward", false);
            }
        }

        // Vertical Jump and Crouch
        if (Input.GetAxis("Vertical") > 0 && !IsJumping)
        {
            IsJumping = true;
            anim.SetTrigger("Jump");
            rb.AddForce(Vector3.up * JumpSpeed, ForceMode.Impulse);
            StartCoroutine(JumpPause());
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            anim.SetBool("Crouch", true);
        }
        else
        {
            anim.SetBool("Crouch", false);
        }
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