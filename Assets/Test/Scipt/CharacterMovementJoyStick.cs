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
        // Get joystick axis input
        float horizontal = Input.GetAxis("Horizontal_Joystick");
        float vertical = Input.GetAxisRaw("Vertical_Joystick");

        // Get button input
        bool moveLeft = Input.GetButton("MoveLeft");
        bool moveRight = Input.GetButton("MoveRight");
        bool jump = Input.GetButton("Jump");
        bool crouch = Input.GetButton("Crouch");

        // Move left or right based on joystick or button input
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

        // Jump if vertical joystick input is up or jump button is pressed, and character is grounded
        if ((vertical > 0 || jump) && IsGrounded() && !jumpInitiated)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpInitiated = true;
        }
        else if (vertical <= 0 && !jump)
        {
            jumpInitiated = false;
        }

        // Crouch if vertical joystick input is down or crouch button is pressed
        if (vertical < 0 || crouch)
        {
            Debug.Log("Crouch");
        }

        LookAtDummy();
    }

    private bool IsGrounded()
    {
        // Use Raycast to check if the character is grounded
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    private void LookAtDummy()
    {
        // Calculate direction to the dummy on the X-axis
        Vector3 directionToDummy = dummy.position - transform.position;
        directionToDummy.y = 0; // Ignore vertical component

        // Store current scale
        Vector3 currentScale = transform.localScale;

        // Set scale based on direction to dummy
        if (directionToDummy.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(currentScale.x), currentScale.y, currentScale.z); // Face right
        }
        else if (directionToDummy.x < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(currentScale.x), currentScale.y, currentScale.z); // Face left
        }
    }
}