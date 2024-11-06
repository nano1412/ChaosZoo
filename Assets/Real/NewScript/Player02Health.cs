using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player02Health : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;
    public ScriptableHealth scriptableHealth;
    public Player02Movement player02Movement;
    public Player02TakeAction player02TakeAction;
    public bool block = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }
    // public void OnTriggerEnter(Collider col)
    // {
    //     if(col.tag == "5P_AttackBox" || col.tag == "5K_AttackBox " || col.tag == "5S_AttackBox" || col.tag == "5HS_AttackBox" ||
    //     col.tag == "2P_AttacKBox" || col.tag == "2K_AttackBox" || col.tag == "2S_AttackBox" || col.tag == "2HS_AttackBox" ||
    //     col.tag == "6P_AttackBox" || col.tag == "6K_AttackBox" || col.tag == "6S_AttackBox" || col.tag == "6HS_AttackBox")
    //     {
    //         if(scriptableHealth.currentHealth > 0)
    //         {
    //             if(player02Movement.animCrouch)
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
        scriptableHealth.currentHealth -= damage;
        if(scriptableHealth.currentHealth > 5)
        {
            if(player02Movement.animCrouch)
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
            player02Movement.isPerformingAction = true;
            player02TakeAction.isPerformingAction = true;
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
        player02Movement.isPerformingAction = false;
        player02TakeAction.isPerformingAction = false;
    }

}
