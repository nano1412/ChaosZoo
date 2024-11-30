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
        else if(!TakeAction && TakeActionMultiButton)
        {
            Col.enabled = !Player01TakeActionMultibutton.Hits;
        }
    }

     private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player02Health")
        {
            Player01TakeAction.Hits = true;
            if(Grab)
            {
                if(animationName == "63214P_Shark")
                {
                    GetComponentInParent<Player01TakeAction>().GrapHCBShark();
                    player02Health.TakeDamage(damage, force, animationName);
                }
                if(animationName == "632146S_Shark")
                {
                    GetComponentInParent<Player01TakeAction>().GrapHCBFShark();
                    player02Health.TakeDamage(damage, force, animationName);
                    player01CameraSpecial.CameraAtciveSpecial();
                }
                if(animationName == "6LPRPLKRP_Capybara")
                {
                    player02Health.TakeDamage(damage, force, animationName);
                    Debug.Log(animationName);
                    return;
                }
            }
            else if(!Grab)
            {
                player02Health.TakeDamage(damage, force, "no");
            }
            else if(Check)
            {
                GetComponentInParent<Player01TakeAction>().OnHits();
            }

            StartCoroutine(ResetHits());
        }
    }

    private IEnumerator ResetHits()
    {
        yield return new WaitForSeconds(1f);
        Player01TakeAction.Hits = false;
    }
}
