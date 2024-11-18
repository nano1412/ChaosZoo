using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player01Trigger : MonoBehaviour
{
    public Collider Col;
    public int damage = 10;
    public float force;
    public bool Check = false;
    public Player02Health player02Health;

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
            player02Health.TakeDamage(damage, force);
            if(Check)
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
