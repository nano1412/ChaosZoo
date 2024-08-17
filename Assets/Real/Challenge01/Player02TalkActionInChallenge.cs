using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player02TalkActionInChallenge : MonoBehaviour
{
    public GameObject player02;
    private Player02MoveInChallenge player02MoveInChallenge; // อ้างอิงไปยัง Player02Move script
    private Rigidbody rb; // อ้างอิงไปยัง Rigidbody ของ player02

    public float forceAmount = 0.5f; // ปรับค่านี้เพื่อเพิ่มหรือลดแรง    
    void Start()
    {
        player02MoveInChallenge = player02.GetComponent<Player02MoveInChallenge>();
        rb = player02.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public void MoveBack()
    {
        if (player02MoveInChallenge.faceLeft)
        {
            rb.AddForce(Vector3.right * forceAmount, ForceMode.Impulse); // ผลักไปทางขวา
        }
        else
        {
            rb.AddForce(Vector3.left * forceAmount, ForceMode.Impulse); // ผลักไปทางซ้าย
        }
    }
}
