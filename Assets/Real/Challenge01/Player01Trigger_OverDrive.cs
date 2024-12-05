using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player01Trigger_OverDrive : MonoBehaviour
{
    public Collider Col;
    public int damage = 10;
    public float force;
    public bool Check = false;
    public bool Grab = false;
    public bool TakeAction = false;
    public bool TakeActionMultiButton = false;
    public bool ActionInChallenge = false;
    public string animationName;
    public Player01CameraInChallange player01CameraSpecial;
    public Player02Movement_Overdrive player02Movement_Overdrive;

    public Player01TakeActionInChallenge player01TakeAction;
    public Player01TakeActionMultiButtonInChallange player01TakeActionMultibutton;

    void Start()
    {
        // คุณอาจต้องการรีเซ็ตค่าเริ่มต้นของ Collider ที่นี่
        Col.enabled = true;
        player02Movement_Overdrive = GameObject.FindGameObjectWithTag("Player02").GetComponent<Player02Movement_Overdrive>();
    }

    void Update()
    {
        if(TakeAction && !TakeActionMultiButton)
        {
            Col.enabled = !Player01TakeActionInChallenge.Hits;
        }
        else if(!TakeAction && TakeActionMultiButton && animationName != "6LPRPLKRP_Capybara")
        {
            Col.enabled = !Player01TakeActionMultiButtonInChallange.Hits;
        }
        if(animationName == "6LPRPLKRP_Capybara")
        {
            Col.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player02")
        {
            if (Grab)
            {
                if (animationName == "632146S_Shark")
                {
                    GetComponentInParent<Player01TakeActionInChallenge>().GrapHCBFShark();
                    player02Movement_Overdrive.TakeDamage(damage, force, animationName);
                    player01CameraSpecial.CameraAtciveSpecial();
                    Player01TakeActionInChallenge.Hits = true;
                }
                else if (animationName == "63214P_Shark")
                {
                    GetComponentInParent<Player01TakeAction>().GrapHCBShark();
                    player02Movement_Overdrive.TakeDamage(damage, force, animationName);
                    Player01TakeActionInChallenge.Hits = true;
                }
                else if (animationName == "6LPRPLKRP_Capybara")
                {
                    player02Movement_Overdrive.TakeDamage(damage, force, animationName);
                    return;
                }
                else if (animationName == "4RPLPRKLK_Ken")
                {
                    player02Movement_Overdrive.TakeDamage(damage, force, animationName);
                    Player01TakeActionInChallenge.Hits = true;
                    Player01TakeActionMultibutton.Hits = true;
                }
            }
            else if (!Grab && !Check && !ActionInChallenge)
            {
                player02Movement_Overdrive.TakeDamage(damage, force, "no");
                Player01TakeActionInChallenge.Hits = true;
                Player01TakeActionMultibutton.Hits = true;
            }
            else if (Check && !Grab)
            {
                Player01TakeAction.Hits = true;
                Player01TakeActionMultibutton.Hits = true;
                player02Movement_Overdrive.TakeDamage(damage, force, "no");
                if(TakeAction && !TakeActionMultiButton) player01TakeAction.OnHits();
                else if(!TakeAction && TakeActionMultiButton) player01TakeActionMultibutton.OnHits();
            }
            else if(ActionInChallenge)
            {
                Player01TakeAction.Hits = true;
                Player01TakeActionMultibutton.Hits = true;
                player02Movement_Overdrive.TakeDamage(damage, force, animationName);
                if(Check)
                {
                    if(TakeAction && !TakeActionMultiButton) player01TakeAction.OnHits();
                    else if(!TakeAction && TakeActionMultiButton) player01TakeActionMultibutton.OnHits();
                }
            }

            Debug.Log("Player");
            StartCoroutine(ResetHits());
        }
    }

    private IEnumerator ResetHits()
    {
        yield return new WaitForSeconds(1f);
        Player01TakeActionInChallenge.Hits = false;
    }
}
