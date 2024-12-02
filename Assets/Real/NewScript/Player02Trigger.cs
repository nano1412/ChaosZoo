using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player02Trigger : MonoBehaviour
{
    public Collider Col;
    public int damage = 10;
    public float force;
    public bool Check = false;
    public bool Grap = false;
    public bool TakeAction = false;
    public bool TakeActionMultiButton = false;
    public string animationNameGrap;
    public Player01Health player01Health;
    public Player02CameraSpecial player02CameraSpecial;

    void Start()
    {
        Col.enabled = true;
        player01Health = GameObject.FindGameObjectWithTag("Player01Health").GetComponent<Player01Health>();
    }

    void Update()
    {
        if(TakeAction && !TakeActionMultiButton)
        {
            Col.enabled = !Player02TakeAction.Hits;
        }
        else if(!TakeAction && TakeActionMultiButton && animationNameGrap != "6LPRPLKRP_Capybara")
        {
            Col.enabled = !Player02TakeActionMultibutton.Hits;
        }
        if(animationNameGrap == "6LPRPLKRP_Capybara")
        {
            Col.enabled = true;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player01Health")
        {
            if (Grap)
            {
                if (animationNameGrap == "632146S_Shark")
                {
                    GetComponentInParent<Player02TakeAction>().GrapHCBFShark();
                    player01Health.TakeDamage(damage, force, animationNameGrap);
                    player02CameraSpecial.CameraAtciveSpecial();
                    Player02TakeAction.Hits = true;
                }
                else if (animationNameGrap == "63214P_Shark")
                {
                    GetComponentInParent<Player02TakeAction>().GrapHCBShark();
                    player01Health.TakeDamage(damage, force, animationNameGrap);
                    Player02TakeAction.Hits = true;
                }
                else if (animationNameGrap == "6LPRPLKRP_Capybara")
                {
                    player01Health.TakeDamage(damage, force, animationNameGrap);
                    // ไม่ตั้งค่า Hits เพื่อไม่ให้ปิด Collider
                    return;
                }
                else if (animationNameGrap == "4RPLPRKLK_Ken")
                {
                    player01Health.TakeDamage(damage, force, animationNameGrap);
                    Player02TakeAction.Hits = true;
                    Player02TakeActionMultibutton.Hits = true;
                }
            }
            else if (!Grap)
            {
                player01Health.TakeDamage(damage, force, "no");
                Player02TakeAction.Hits = true;
                Player02TakeActionMultibutton.Hits = true;
            }
            else if (Check)
            {
                GetComponentInParent<Player02TakeAction>().OnHits();
                Player02TakeAction.Hits = true;
                Player02TakeActionMultibutton.Hits = true;
            }

            StartCoroutine(ResetHits());
        }
    }

    private IEnumerator ResetHits()
    {
        // หาก animationNameGrap เป็น "6LPRPLKRP_Capybara" ไม่ต้องรีเซ็ต Hits
        if (animationNameGrap == "6LPRPLKRP_Capybara")
        {
            yield break; // ออกจาก Coroutine ทันที
        }

        yield return new WaitForSeconds(1f);
        Player02TakeAction.Hits = false;
        Player02TakeActionMultibutton.Hits = false;
    }

}
