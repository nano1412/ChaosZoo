using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShildSpin_Player02 : MonoBehaviour
{
    public Rigidbody rb; // Rigidbody ของ RPG
    public float speed = 5f; // ความเร็วในการเคลื่อนที่
    public int damage = 10; // ค่าความเสียหาย
    public float spinSpeed = 1f; // ความเร็วในการหมุน (องศาต่อวินาที)
    public Player02Movement player02Movement;
    public Player02TakeAction player02TakeAction;
    void Start()
    {
        transform.rotation = Quaternion.Euler(-90, 0, 0);
        player02Movement = GameObject.FindGameObjectWithTag("Player02").GetComponent<Player02Movement>();
        player02TakeAction = GameObject.FindGameObjectWithTag("PlayerCharacter02Tpose").GetComponent<Player02TakeAction>();
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("RPG collided with: " + other.gameObject.name + " (Tag: " + other.gameObject.tag + ")");

        if (other.gameObject.tag == "WallLeft" || other.gameObject.tag == "WallRight")
        {
            Debug.Log("RPG hit the wall: " + other.gameObject.tag);
            Destroy(gameObject); // ทำลาย RPG
        }

        if (other.gameObject.CompareTag("Player01"))
        {
            Player01WithRpg player01WithRpg = other.gameObject.GetComponent<Player01WithRpg>();

            player01WithRpg.ShildAttack();

            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (player02Movement.faceRight)
        {
            rb.MovePosition(transform.position + Vector3.right * speed * Time.fixedDeltaTime);
        }
        else
        {
            rb.MovePosition(transform.position - Vector3.right * speed * Time.fixedDeltaTime);
        }
        RotateShield();
    }

    void RotateShield()
    {
        // หมุนรอบแกน Z แบบช้าๆ โดยใช้ค่าจาก spinSpeed
        transform.Rotate(0, 0, spinSpeed * Time.fixedDeltaTime, Space.Self);
    }
}
