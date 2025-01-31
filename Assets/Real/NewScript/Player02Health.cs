using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player02Health : MonoBehaviour
{
    public Rigidbody rb;
    private Animator anim;
    public ScriptableHealth scriptableHealth;
    public Player02Movement player02Movement;
    public Player02TakeAction player02TakeAction;
    public Player02TakeActionMultibutton player02TakeActionMultibutton;
    public Player02EventAnimation player02EventAnimation;
    //public Slider hpMainSlider;
    //public Slider hpEaseSlider;
    //private float lerpSpeed = 0.05f;
    public bool block = false;
    public bool knockout = false;
    public bool SharkDrive = false;
    public bool KenInAir = false;
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
        if(scriptableHealth.currentHealth <= 0)
        {
            player02Movement.isPerformingAction = true;
            player02TakeAction.isPerformingAction = true;
            player02TakeActionMultibutton.isPerformingAction = true;
            //anim.SetTrigger("Dead");
            knockout = true;
        }

        //HPSliderLink();
    }

    public void TakeDamage(int damage, float force, string actionGrabName)
    {
        if(scriptableHealth.currentHealth > 5 && !knockout && !SharkDrive && !KenInAir)
        {
            if(actionGrabName == "no")
            {
                if(player02Movement.animCrouch)
                {
                    if(block)
                    {
                        anim.SetTrigger("BlockCrouch");
                        player02EventAnimation.forcehurt = force;
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
                            player02EventAnimation.forcehurt = force;
                        }
                    }
                }
                else
                {
                    if(block)
                    {
                        anim.SetTrigger("Block");
                        player02EventAnimation.forcehurt = force;
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
                            player02EventAnimation.forcehurt = force;
                        }
                    }
                }
                player02Movement.isPerformingAction = true;
                player02TakeAction.isPerformingAction = true;
                player02TakeActionMultibutton.isPerformingAction = true;
                StartCoroutine(resetHurt());
            }
            else if(actionGrabName == "63214P_Shark")
            {
                if(scriptableHealth.currentHealth > 0  && !knockout && !SharkDrive && !KenInAir)
                {
                    player02EventAnimation.forcehurt = force;
                    anim.SetTrigger("Shark_grab_HCB");
                    StartCoroutine(resetGrapHCB(damage));
                    player02Movement.isPerformingAction = true;
                    player02TakeAction.isPerformingAction = true;
                    player02TakeActionMultibutton.isPerformingAction = true;
                }
            }
            else if(actionGrabName == "632146S_Shark")
            {
                if(scriptableHealth.currentHealth > 0  && !knockout && !SharkDrive && !KenInAir)
                {
                    player02EventAnimation.forcehurt = force;
                    anim.SetTrigger("Shark_grab_HCBF");
                    StartCoroutine(resetGrapHCBF(damage));
                    player02Movement.isPerformingAction = true;
                    player02TakeAction.isPerformingAction = true;
                    player02TakeActionMultibutton.isPerformingAction = true;
                }
            }
            else if(actionGrabName == "6LPRPLKRP_Capybara")
            {
                if(scriptableHealth.currentHealth > 0  && !knockout && !SharkDrive && !KenInAir)
                {
                    scriptableHealth.currentHealth -= damage;
                    if(scriptableHealth.currentHealth > 0)
                    {
                        anim.SetTrigger("Hurt");
                    }
                    else if(scriptableHealth.currentHealth <= 0)
                    {
                        anim.SetTrigger("Dead");
                        knockout = true;
                    }
                    player02Movement.isPerformingAction = true;
                    player02TakeAction.isPerformingAction = true;
                    player02TakeActionMultibutton.isPerformingAction = true;
                }
            }
            else if(actionGrabName == "4RPLPRKLK_Ken")
            {
                if(scriptableHealth.currentHealth > 0  && !knockout && !SharkDrive && !KenInAir)
                {
                    player02EventAnimation.forcehurt = force;
                    anim.SetTrigger("Ken_4RPLPRKLK");
                    StartCoroutine(resetKenSpecial(damage));
                    player02Movement.isPerformingAction = true;
                    player02TakeAction.isPerformingAction = true;
                    player02TakeActionMultibutton.isPerformingAction = true;
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
        player02Movement.isPerformingAction = false;
        player02TakeAction.isPerformingAction = false;
        player02TakeActionMultibutton.isPerformingAction = false;
        player02EventAnimation.forcehurt = 0;
    }

    IEnumerator recovery()
    {
        yield return new WaitForSeconds(1f);
        anim.SetTrigger("recove");
        player02Movement.isPerformingAction = false;
        player02TakeAction.isPerformingAction = false;
        player02TakeActionMultibutton.isPerformingAction = false;
        knockout = false;
    }
    IEnumerator resetGrapHCB(int damage)
    {
        yield return new WaitForSeconds(2f);
        scriptableHealth.currentHealth -= damage;
        player02EventAnimation.forcehurt = 0;
        if(scriptableHealth.currentHealth > 0)
        {
            StartCoroutine(recovery());
        }
        else
        {
            knockout = true;
            player02Movement.isPerformingAction = true;
            player02TakeAction.isPerformingAction = true;
            player02TakeActionMultibutton.isPerformingAction = true;
        }

    }

    IEnumerator resetGrapHCBF(int damage)
    {
        yield return new WaitForSeconds(4f);
        scriptableHealth.currentHealth -= damage;
        player02EventAnimation.forcehurt = 0;
        if(scriptableHealth.currentHealth > 0)
        {
            StartCoroutine(recovery());
        }
        else
        {
            knockout = true;
            player02Movement.isPerformingAction = true;
            player02TakeAction.isPerformingAction = true;
            player02TakeActionMultibutton.isPerformingAction = true;
        }
    }

    IEnumerator resetKenSpecial(int damage)
    {
        yield return new WaitForSeconds(0.5f);
        scriptableHealth.currentHealth -= damage;
        player02EventAnimation.forcehurt = 0;
        if(scriptableHealth.currentHealth <= 0)
        {
            anim.SetTrigger("DeadKen");
            knockout = true;
            player02Movement.isPerformingAction = true;
            player02TakeAction.isPerformingAction = true;
            player02TakeActionMultibutton.isPerformingAction = true;
        }
        else
        {
            player02Movement.isPerformingAction = false;
            player02TakeAction.isPerformingAction = false;
            player02TakeActionMultibutton.isPerformingAction = false;
            knockout = false;
        }
    }
}
