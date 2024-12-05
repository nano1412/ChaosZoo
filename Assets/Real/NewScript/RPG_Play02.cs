using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPG_Play02 : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 5f;
    public int damage = 10;
    public Player02Movement player02Movement;
    public Player02TakeAction player02TakeAction;
    public Player01Health player01Health;

    void Start()
    {
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

            if(player02TakeAction.NumberRPG)
            {
                player01WithRpg.RPGOverDrive();
            }
            else
            {
                player01WithRpg.RPGTIme();
            }

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
    }
}
