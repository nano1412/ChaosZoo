using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player02Health : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;
    public ScriptableHealth scriptableHealth;
    public Player02Movement player02Movement;
    public Player02TakeAction player02TakeAction;
    public Slider hpMainSlider;
    //public Slider hpEaseSlider;
    //private float lerpSpeed = 0.05f;
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
            if(player02Movement.animCrouch)
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
            player02Movement.isPerformingAction = true;
            player02TakeAction.isPerformingAction = true;
            StartCoroutine(resetHurt());
        }    
        if(scriptableHealth.currentHealth <= 0)
        {
            anim.SetTrigger("Dead");
        }
    }

    public void HPSliderLink()
    {
        if(hpMainSlider.value != scriptableHealth.currentHealth)
        {
            hpMainSlider.value = scriptableHealth.currentHealth;
        }

        /*if(hpMainSlider.value != hpEaseSlider.value)
        {
            hpEaseSlider.value = Mathf.Lerp(hpEaseSlider.value, scriptableHealth.currentHealth, lerpSpeed);
        }*/
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
        knockout = false;
    }
}
