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
    public int currentdamage = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

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

    IEnumerator recovery()
    {
        yield return new WaitForSeconds(1f);
        anim.SetTrigger("recove");
    }
}
