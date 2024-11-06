using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player01Health : MonoBehaviour
{

    private Rigidbody rb;
    private Animator anim;
    public ScriptableHealth scriptableHealth;
    public Player01Movement player01Movement;
    public Player01TakeAction player01TakeAction;
    public bool block = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }
    // public void OnTriggerEnter(Collider col)
    // {
    //     if(col.tag == "5P_AttackBox" || col.tag == "5K_AttackBox" || col.tag == "5S_AttackBox" || col.tag == "5HS_AttackBox" ||
    //        col.tag == "2P_AttackBox" || col.tag == "2K_AttackBox" || col.tag == "2S_AttackBox" || col.tag == "2HS_AttackBox" ||
    //        col.tag == "6P_AttackBox" || col.tag == "6K_AttackBox" || col.tag == "6S_AttackBox" || col.tag == "6HS_AttackBox")
    //     {
    //         if(scriptableHealth.currentHealth > 0)
    //         {
    //             if(player01Movement.animCrouch)
    //             {
    //                 anim.SetTrigger("HurtCrouch");
    //             }
    //             else
    //             {
    //                 anim.SetTrigger("Hurt");
    //             }
    //         }
    //     }
    // }

    public void TakeDamage(int damage)
    {
        if(scriptableHealth.currentHealth > 5)
        {
            if(player01Movement.animCrouch)
            {
                if(block)
                {
                    anim.SetTrigger("BlockCrouch");
                }
                else
                {
                    scriptableHealth.currentHealth -= damage;
                    anim.SetTrigger("HurtCrouch");
                }
            }
            else
            {
                if(block)
                {
                    anim.SetTrigger("Block");
                }
                else
                {
                    scriptableHealth.currentHealth -= damage;
                    anim.SetTrigger("Hurt");
                }
            }
            player01Movement.isPerformingAction = true;
            player01TakeAction.isPerformingAction = true;
            StartCoroutine(resetHurt());
        }    
        if(scriptableHealth.currentHealth <= 0)
        {
            anim.SetTrigger("Dead");
        }
    }

    IEnumerator resetHurt()
    {
        yield return new WaitForSeconds(0.5f);
        player01Movement.isPerformingAction = false;
        player01TakeAction.isPerformingAction = false;
    }
}   
