using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementKeyBoard : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public Transform dummy;

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Crouch");
        }
        if ((Input.GetKeyDown(KeyCode.W) && IsGrounded()))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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

        if (directionToDummy.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(currentScale.x), currentScale.y, currentScale.z); 
        }
        else if (directionToDummy.x < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(currentScale.x), currentScale.y, currentScale.z); 
        }
    }
}
