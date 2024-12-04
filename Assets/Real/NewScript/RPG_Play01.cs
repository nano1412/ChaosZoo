using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPG_Play01 : MonoBehaviour
{
    public Rigidbody rb; // Rigidbody ของ RPG
    public float speed = 5f; // ความเร็วในการเคลื่อนที่
    public int damage = 10; // ค่าความเสียหาย
    public Player01Movement player01Movement; // อ้างอิง Player01Movement เพื่อเช็คการหันหน้า
    public Player02Health player02Health;

    void Start()
    {
        // ดึง Component Player01Movement จาก GameObject ที่มี Tag "Player01"
        player01Movement = GameObject.FindGameObjectWithTag("Player01").GetComponent<Player01Movement>();
    }

   private void OnCollisionEnter(Collision other)
    {
        // แสดงข้อมูลวัตถุที่ชน
        Debug.Log("RPG collided with: " + other.gameObject.name + " (Tag: " + other.gameObject.tag + ")");

        // ตรวจสอบว่าชนกับกำแพง (WallLeft หรือ WallRight)
        if (other.gameObject.tag == "WallLeft" || other.gameObject.tag == "WallRight")
        {
            Debug.Log("RPG hit the wall: " + other.gameObject.tag);
            Destroy(gameObject); // ทำลาย RPG
        }

        if (other.gameObject.CompareTag("Player02"))
        {
            // ดึง Component Player02WithRpg
            Player02WithRpg player02WithRpg = other.gameObject.GetComponent<Player02WithRpg>();

            if (player02WithRpg != null)
            {
                // เรียกฟังก์ชัน RPGTIme
                player02WithRpg.RPGTIme();
                Debug.Log("RPGTIme function called successfully.");
            }
            else
            {
                Debug.LogError("Player02WithRpg component not found on " + other.gameObject.name);
            }

            // ทำลาย RPG หลังจากชน
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        // เคลื่อนที่ตามทิศทาง (แกน X)
        if (player01Movement.faceRight)
        {
            // เคลื่อนไปทางขวา
            rb.MovePosition(transform.position + Vector3.right * speed * Time.fixedDeltaTime);
        }
        else
        {
            // เคลื่อนไปทางซ้าย
            rb.MovePosition(transform.position - Vector3.right * speed * Time.fixedDeltaTime);
        }
    }
}
