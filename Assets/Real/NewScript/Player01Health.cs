using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player01Health : MonoBehaviour
{

    private Rigidbody rb;
    private Animator anim;
    public ScriptableHealth scriptableHealth;
    public Player01Movement player01Movement;
    public Player01TakeAction player01TakeAction;
    public Slider HPSlider;
    public bool block = false;
    public bool knockout = false;
    public int currentdamage = 0;
    public float time;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        scriptableHealth.currentHealth = scriptableHealth.maxHealth;
    }

    void Update()
    {
        if(currentdamage > 1)
        {
            time += Time.deltaTime;
            if(time > 4)
            {
                currentdamage = 0;
                time = 0;
            }
        }

        HPSliderLink();
    }
    public void TakeDamage(int damage)
    {
        if(scriptableHealth.currentHealth > 5 && !knockout)
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
                    currentdamage += damage;
                    if(currentdamage >= 30)
                    {
                        anim.SetTrigger("KnockCrouch");
                        currentdamage = 0;
                        StartCoroutine(recovery());
                        knockout = true;
                        time = 0;
                    }
                    else
                    {
                        anim.SetTrigger("HurtCrouch");
                    }
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
                        knockout = true;
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

    public void HPSliderLink()
    {
        if(HPSlider.value != scriptableHealth.currentHealth)
        {
            HPSlider.value = scriptableHealth.currentHealth;
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
        knockout = false;
    }
}   
