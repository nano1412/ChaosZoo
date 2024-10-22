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

