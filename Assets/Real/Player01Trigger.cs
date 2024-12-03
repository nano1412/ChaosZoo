using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player01Trigger : MonoBehaviour
{
    public Collider Col;
    public int damage = 10;
    public float force;
    public bool Check = false;
    public bool Grab = false;
    public bool TakeAction = false;
    public bool TakeActionMultiButton = false;
    public string animationName;
    public Player02Health player02Health;
    public Player01CameraSpecial player01CameraSpecial;
    void Start()
    {
        Col.enabled = true;
        player02Health = GameObject.FindGameObjectWithTag("Player02Health").GetComponent<Player02Health>();
    }

    void Update()
    {
        if(TakeAction && !TakeActionMultiButton)
        {
            Col.enabled = !Player01TakeAction.Hits;
        }
        else if(!TakeAction && TakeActionMultiButton && animationName != "6LPRPLKRP_Capybara")
        {
            Col.enabled = !Player01TakeActionMultibutton.Hits;
        }
        if(animationName == "6LPRPLKRP_Capybara")
        {
            Col.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player02Health")
        {
            if (Grab)
            {
                if (animationName == "632146S_Shark")
                {
                    GetComponentInParent<Player01TakeAction>().GrapHCBFShark();
                    player02Health.TakeDamage(damage, force, animationName);
                    player01CameraSpecial.CameraAtciveSpecial();
                    Player01TakeAction.Hits = true;
                }
                else if (animationName == "63214P_Shark")
                {
                    GetComponentInParent<Player01TakeAction>().GrapHCBShark();
                    player02Health.TakeDamage(damage, force, animationName);
                    Player01TakeAction.Hits = true;
                }
                else if (animationName == "6LPRPLKRP_Capybara")
                {
                    player02Health.TakeDamage(damage, force, animationName);
                    // ไม่ตั้งค่า Hits เพื่อไม่ให้ปิด Collider
                    return;
                }
                else if (animationName == "4RPLPRKLK_Ken")
                {
                    player02Health.TakeDamage(damage, force, animationName);
                    Player01TakeAction.Hits = true;
                    Player01TakeActionMultibutton.Hits = true;
                }
            }
            else if (!Grab)
            {
                player02Health.TakeDamage(damage, force, "no");
                Player01TakeAction.Hits = true;
                Player01TakeActionMultibutton.Hits = true;
            }
            else if (Check)
            {
                Player01TakeAction.Hits = true;
                Player01TakeActionMultibutton.Hits = true;
                GetComponentInParent<Player01TakeAction>().OnHits();
            }

            StartCoroutine(ResetHits());
        }
    }

    private IEnumerator ResetHits()
    {
        // หาก animationNameGrap เป็น "6LPRPLKRP_Capybara" ไม่ต้องรีเซ็ต Hits
        if (animationName == "6LPRPLKRP_Capybara")
        {
            yield break; // ออกจาก Coroutine ทันที
        }

        yield return new WaitForSeconds(1f);
        Player01TakeAction.Hits = false;
        Player01TakeActionMultibutton.Hits = false;
    }

}
