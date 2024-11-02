using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player02Health : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }
    public void OnTriggerEnter(Collider col)
    {
        if(col.tag == "5P_AttackBox" || col.tag == "5K_AttackBox " || col.tag == "5S_AttackBox" || col.tag == "5HS_AttackBox" ||
        col.tag == "2P_AttacKBox" || col.tag == "2K_AttackBox" || col.tag == "2S_AttackBox" || col.tag == "2HS_AttackBox" ||
        col.tag == "6P_AttackBox" || col.tag == "6K_AttackBox" || col.tag == "6S_AttackBox" || col.tag == "6HS_AttackBox")
        {
            //anim.SetTrigger("Hurt");
            Debug.Log("Attack");
        }
    }

}
