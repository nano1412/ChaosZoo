using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player02TakeAction : MonoBehaviour
{
    public GameObject player02;
    private Player02Move player02Move; // อ้างอิงไปยัง Player02Move script
    private Rigidbody rb; // อ้างอิงไปยัง Rigidbody ของ player02

    public float forceAmount = 50f; // ปรับค่านี้เพื่อเพิ่มหรือลดแรง

    void Start()
    {
        player02Move = player02.GetComponent<Player02Move>(); // รับการอ้างอิงไปยัง Player02Move
        rb = player02.GetComponent<Rigidbody>(); // รับการอ้างอิงไปยัง Rigidbody
    }

    /*public void MoveBack()
    {
        if (player02Move.faceLeft)
        {
            rb.AddForce(Vector3.right * forceAmount, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(Vector3.left * forceAmount, ForceMode.Impulse); 
        }
    }*/
}

