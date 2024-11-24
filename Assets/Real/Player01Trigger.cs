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
    public string animationNameGrab;
    public Player02Health player02Health;
    public Player01CameraSpecial player01CameraSpecial;


    void Start()
    {
        Col.enabled = true;
        player02Health = GameObject.FindGameObjectWithTag("Player02Health").GetComponent<Player02Health>();
    }

    void Update()
    {
        Col.enabled = !Player01Action.Hits;
    }

     private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player02Health")
        {
            Player01TakeAction.Hits = true;
            if(Grab)
            {
                if(animationNameGrab == "63214P_Shark")
                {
                    GetComponentInParent<Player01TakeAction>().GrapHCBShark();
                    player02Health.TakeDamage(damage, force, animationNameGrab);
                }
                if(animationNameGrab == "632146S_Shark")
                {
                    GetComponentInParent<Player01TakeAction>().GrapHCBFShark();
                    player02Health.TakeDamage(damage, force, animationNameGrab);
                    player01CameraSpecial.CameraAtciveSpecial();
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
