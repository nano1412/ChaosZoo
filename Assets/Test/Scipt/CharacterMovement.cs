using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public Transform dummy; // Drag the dummy GameObject here in the Inspector

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Keyboard input for movement
        float horizontalInput = Input.GetAxis("Horizontal"); // Get horizontal axis input (-1 to 1)
        float verticalInput = Input.GetAxis("Vertical"); // Get vertical axis input (-1 to 1)

        // Joystick input for movement
        float joystickHorizontal = Input.GetAxis("Horizontal_Joystick"); // Example: replace with your actual joystick axis name
        float joystickVertical = Input.GetAxis("Vertical_Joystick"); // Example: replace with your actual joystick axis name

        // Combine keyboard and joystick inputs
        float moveHorizontal = Mathf.Abs(horizontalInput) > Mathf.Abs(joystickHorizontal) ? horizontalInput : joystickHorizontal;
        float moveVertical = Mathf.Abs(verticalInput) > Mathf.Abs(joystickVertical) ? verticalInput : joystickVertical;

        // Movement
        Vector3 moveDirection = new Vector3(moveHorizontal, 0, moveVertical).normalized;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // Crouching
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Crouch");
        }

        // Jumping
        if ((Input.GetKeyDown(KeyCode.W) && IsGrounded()))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Look at Dummy
        LookAtDummy();
    }

    private bool IsGrounded()
    {
        // Raycast downwards to check if the character is grounded
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    private void LookAtDummy()
    {
        // Calculate direction to the dummy in the X-axis
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