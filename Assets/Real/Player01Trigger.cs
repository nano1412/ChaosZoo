using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player01Trigger : MonoBehaviour
{
    public Collider Col;
    void Start()
    {
        
    }

    void Update()
    {
        if(Player01Action.Hits == false)
        {
            Col.enabled = true;
        }
        else
        {
            Col.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player02")
        {
            Player01TakeAction.Hits = true;
            GetComponentInParent<Player01TakeAction>().OnHits();
        }
    }
}
