using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player01TriggerInChallenge : MonoBehaviour
{
    public Collider Col;
    public int damage = 10;
    public float force;
    public bool Check = false;
    public bool Grab = false;
    public bool TakeAction = false;
    public bool TakeActionMultiButton = false;
    public string animationName;
    public Player01CameraInChallange player01CameraSpecial;

    void Start()
    {
        // คุณอาจต้องการรีเซ็ตค่าเริ่มต้นของ Collider ที่นี่
        Col.enabled = true;
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
                    GetComponentInParent<Player01TakeActionInChallenge>().GrapHCBFShark();
                    player01CameraSpecial.CameraAtciveSpecial();
                    Player01TakeActionInChallenge.Hits = true;
                }
                else if (animationName == "63214P_Shark")
                {
                    GetComponentInParent<Player01TakeAction>().GrapHCBShark();
                    Player01TakeActionInChallenge.Hits = true;
                }
                else if (animationName == "6LPRPLKRP_Capybara")
                {
                    // ไม่ตั้งค่า Hits เพื่อไม่ให้ปิด Collider
                    return;
                }
                else if (animationName == "4RPLPRKLK_Ken")
                {
                    Player01TakeActionInChallenge.Hits = true;
                    Player01TakeActionMultibutton.Hits = true;
                }
            }
            else if (!Grab)
            {
                Player01TakeActionInChallenge.Hits = true;
                Player01TakeActionMultibutton.Hits = true;
            }
            else if (Check)
            {
                GetComponentInParent<Player02TakeAction>().OnHits();
                Player01TakeAction.Hits = true;
                Player01TakeActionMultibutton.Hits = true;
            }

            StartCoroutine(ResetHits());
        }
    }

    private IEnumerator ResetHits()
    {
        yield return new WaitForSeconds(1f);
        Player01TakeActionInChallenge.Hits = false;
    }
}