using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
     public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public Transform dummy; // Drag the dummy GameObject here in the Inspector
    private bool isGrounded;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // การเคลื่อนที่ทางด้านข้าง
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }

        // การย่อตัว
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Crouch");
        }

        // การกระโดด
        if (Input.GetKeyDown(KeyCode.W) && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // หันหน้าตัวละครไปหาตัว Dummy ในแนวแกน X เท่านั้น
        LookAtDummy();
    }

    private bool IsGrounded()
    {
        // Raycast ลงล่างเพื่อตรวจสอบว่าตัวละครสัมผัสกับพื้นหรือไม่
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    private void LookAtDummy()
    {
        // คำนวณทิศทางไปยัง Dummy ในแนวแกน X
        Vector3 directionToDummy = dummy.position - transform.position;
        directionToDummy.y = 0; // ลบองค์ประกอบแนวตั้ง

        // หาค่า Scale ที่เก่า
        Vector3 currentScale = transform.localScale;

        // กำหนด Scale ตามทิศทางที่หันไป
        if (directionToDummy.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(currentScale.x), currentScale.y, currentScale.z); // หันหน้าทางขวา
        }
        else if (directionToDummy.x < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(currentScale.x), currentScale.y, currentScale.z); // หันหน้าทางซ้าย
        }
    }

}