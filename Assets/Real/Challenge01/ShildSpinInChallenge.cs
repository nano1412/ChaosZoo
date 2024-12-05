using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShildSpinInChallenge : MonoBehaviour
{
    public Rigidbody rb; // Rigidbody ของ RPG
    public float speed = 5f; // ความเร็วในการเคลื่อนที่
    public float spinSpeed = 1f; // ความเร็วในการหมุน (องศาต่อวินาที)
    public Player01MovementChallenge player01Movement;
    public Player01TakeActionInChallenge player01TakeAction;
    void Start()
    {
        transform.rotation = Quaternion.Euler(-90, 0, 0);
        player01Movement = GameObject.FindGameObjectWithTag("Player01").GetComponent<Player01MovementChallenge>();
        player01TakeAction = GameObject.FindGameObjectWithTag("PlayerCharacter01Tpose").GetComponent<Player01TakeActionInChallenge>();
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("RPG collided with: " + other.gameObject.name + " (Tag: " + other.gameObject.tag + ")");

        if (other.gameObject.tag == "WallLeft" || other.gameObject.tag == "WallRight")
        {
            Debug.Log("RPG hit the wall: " + other.gameObject.tag);
            Destroy(gameObject); // ทำลาย RPG
        }

        if (other.gameObject.CompareTag("Player02"))
        {
            Player02WithRPG_Challenge player02WithRPG_Challenge = other.gameObject.GetComponent<Player02WithRPG_Challenge>();

            player02WithRPG_Challenge.ShildAttack();

            Destroy(gameObject);
        }
    }
    void FixedUpdate()
    {
        if (player01Movement.faceRight)
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
