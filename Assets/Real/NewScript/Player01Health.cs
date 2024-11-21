using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player01Health : MonoBehaviour
{
    public Rigidbody rb;
    private Animator anim;
    public ScriptableHealth scriptableHealth;
    public Player01Movement player01Movement;
    public Player01TakeAction player01TakeAction;
    public Player01EventAnimation player01EventAnimation;
    //public Slider hpMainSlider;
    //public Slider hpEaseSlider;
    //private float lerpSpeed = 0.05f;
    public bool block = false;
    public bool knockout = false;
    public int currentdamage = 0;
    public float time;

    void Start()
    {
        anim = transform.parent.GetComponent<Animator>();
        scriptableHealth.currentHealth = scriptableHealth.maxHealth;
        knockout = false;
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

        //HPSliderLink();
    }
    public void TakeDamage(int damage, float force, string actionGrapName)
    {
        if(scriptableHealth.currentHealth > 5 && !knockout)
        {
            if(actionGrapName == "no")
            {
                if(player01Movement.animCrouch)
                {
                    if(block)
                    {
                        anim.SetTrigger("BlockCrouch");
                        player01EventAnimation.forcehurt = force;
                    }
                    else
                    {
                        scriptableHealth.currentHealth -= damage;
                        currentdamage += damage;
                        if(currentdamage >= 30)
                        {
                            if(scriptableHealth.currentHealth > 0)
                            {
                                anim.SetTrigger("KnockCrouch");
                                currentdamage = 0;
                                StartCoroutine(recovery());
                                knockout = true;
                                time = 0;
                            }
                            else
                            {
                                anim.SetTrigger("Dead");
                                knockout = true;
                            }
                        }
                        else if(scriptableHealth.currentHealth > 0 && !knockout)
                        {
                            anim.SetTrigger("HurtCrouch");
                            player01EventAnimation.forcehurt = force;
                        }
                    }
                }
                else
                {
                    if(block)
                    {
                        anim.SetTrigger("Block");
                        player01EventAnimation.forcehurt = force;
                    }
                    else
                    {
                        scriptableHealth.currentHealth -= damage;
                        currentdamage += damage;
                        if(currentdamage >= 30)
                        {
                            if(scriptableHealth.currentHealth > 0)
                            {
                                anim.SetTrigger("Knock");
                                currentdamage = 0;
                                StartCoroutine(recovery());
                                knockout = true;
                            }
                            else
                            {
                                anim.SetTrigger("Dead");
                                knockout = true;
                            }
                        }
                        else if(scriptableHealth.currentHealth > 0 && !knockout)
                        {
                            anim.SetTrigger("Hurt");
                            player01EventAnimation.forcehurt = force;
                        }
                    }
                }
                player01Movement.isPerformingAction = true;
                player01TakeAction.isPerformingAction = true;
                StartCoroutine(resetHurt());
            }
            if(actionGrapName == "63214P_Shark")
            {
                if(scriptableHealth.currentHealth > 0)
                {
                    player01EventAnimation.forcehurt = force;
                    anim.SetTrigger("Shark_grab_HCB");
                    StartCoroutine(resetGrapHCB(damage));
                    player01Movement.isPerformingAction = true;
                    player01TakeAction.isPerformingAction = true;
                    return;
                }
            }   
        }    
        if(scriptableHealth.currentHealth <= 0 && !knockout)
        {
            anim.SetTrigger("Dead");
            knockout = true;
        }
    }

    /*public void HPSliderLink()
    {
        if(hpMainSlider.value != scriptableHealth.currentHealth)
        {
            hpMainSlider.value = scriptableHealth.currentHealth;
        }

        /*if(hpMainSlider.value != hpEaseSlider.value)
        {
            hpEaseSlider.value = Mathf.Lerp(hpEaseSlider.value, scriptableHealth.currentHealth, lerpSpeed);
        }
    }*/

    IEnumerator resetHurt()
    {
        yield return new WaitForSeconds(0.5f);
        player01Movement.isPerformingAction = false;
        player01TakeAction.isPerformingAction = false;
        player01EventAnimation.forcehurt = 0;
    }

    IEnumerator recovery()
    {
        yield return new WaitForSeconds(1f);
        anim.SetTrigger("recove");
        knockout = false;
    }
    IEnumerator resetGrapHCB(int damage)
    {
        yield return new WaitForSeconds(2f);
        scriptableHealth.currentHealth -= damage;
        player01EventAnimation.forcehurt = 0;
        if(scriptableHealth.currentHealth > 0)
        {
            StartCoroutine(recovery());
        }
        else
        {
            knockout = true;
        }
    }
}   
