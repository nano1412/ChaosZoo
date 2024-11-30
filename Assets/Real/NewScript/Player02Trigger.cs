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
        else if(!TakeAction && TakeActionMultiButton)
        {
            Col.enabled = !Player02TakeActionMultibutton.Hits;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player01Health")
        {
            Player02TakeAction.Hits = true;
            if(Grap)
            {
                if(animationNameGrap == "632146S_Shark")
                {
                    GetComponentInParent<Player02TakeAction>().GrapHCBFShark();
                    player01Health.TakeDamage(damage, force, animationNameGrap);
                    player02CameraSpecial.CameraAtciveSpecial();
                }
                if(animationNameGrap == "63214P_Shark")
                {
                    GetComponentInParent<Player02TakeAction>().GrapHCBShark();
                    player01Health.TakeDamage(damage, force, animationNameGrap);
                }
            }
            else if(!Grap)
            {
                player01Health.TakeDamage(damage, force, "no");
            }
            else if(Check)
            {
                GetComponentInParent<Player02TakeAction>().OnHits();
            }

            StartCoroutine(ResetHits());
        }
    }

    private IEnumerator ResetHits()
    {
        yield return new WaitForSeconds(1f);
        Player02TakeAction.Hits = false;
    }
}
