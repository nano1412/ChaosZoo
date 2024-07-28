using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementJoyStick : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public Transform dummy;

    private Rigidbody rb;
    private bool isGrounded;
    private bool jumpInitiated;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal_Joystick");
        float vertical = Input.GetAxisRaw("Vertical_Joystick");

        bool moveLeft = Input.GetButton("MoveLeft");
        bool moveRight = Input.GetButton("MoveRight");
        bool jump = Input.GetButton("Jump");
        bool crouch = Input.GetButton("Crouch");
        if (horizontal != 0)
        {
            transform.Translate(Vector3.right * horizontal * moveSpeed * Time.deltaTime);
        }
        else if (moveLeft)
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        else if (moveRight)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }

        if ((vertical > 0 || jump) && IsGrounded() && !jumpInitiated)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpInitiated = true;
        }
        else if (vertical <= 0 && !jump)
        {
            jumpInitiated = false;
        }


        if (vertical < 0 || crouch)
        {
            Debug.Log("Crouch");
        }

        LookAtDummy();
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

  
        if (directionToDummy.x > 0 && IsGrounded())
        {
            transform.localScale = new Vector3(Mathf.Abs(currentScale.x), currentScale.y, currentScale.z); 
        }
        else if (directionToDummy.x < 0 && IsGrounded())
        {
            transform.localScale = new Vector3(-Mathf.Abs(currentScale.x), currentScale.y, currentScale.z); 
        }
    }
}