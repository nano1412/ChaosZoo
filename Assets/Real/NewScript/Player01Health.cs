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
    public int currentdamage = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }
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
                    currentdamage += damage;
                    if(currentdamage >= 30)
                    {
                        anim.SetTrigger("Knock");
                        currentdamage = 0;
                        StartCoroutine(recovery());
                    }
                    else
                    {
                        anim.SetTrigger("Hurt");
                    }
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

    IEnumerator recovery()
    {
        yield return new WaitForSeconds(1f);
        anim.SetTrigger("recove");
    }
}   
