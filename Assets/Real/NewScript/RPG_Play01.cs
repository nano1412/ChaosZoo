using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPG_Play01 : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 0;
    public int damage = 0;

    public Player01Movement player01Movement;
    public Player02Health player02Health;

    void Start()
    {
        player01Movement = GameObject.FindGameObjectWithTag("Player01").GetComponent<Player01Movement>();
    }

    void FixedUpdate()
    {
        if (player01Movement.faceRight)
        {
            rb.MovePosition(transform.position + transform.right * speed * Time.fixedDeltaTime);
        }
        else
        {
            rb.MovePosition(transform.position - transform.right * speed * Time.fixedDeltaTime);
        }
    }



}


