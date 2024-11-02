using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player02Trigger : MonoBehaviour
{
    public Collider Col;
    public int damage = 10;
    public bool Check = false;
    public Player01Health player01Health;

    void Start()
    {
        Col.enabled = true;
        player01Health = GameObject.FindGameObjectWithTag("Player01").GetComponent<Player01Health>();
    }

    void Update()
    {
        Col.enabled = !Player02TakeAction.Hits;
    }

     private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player01")
        {
            Player02TakeAction.Hits = true;
            player01Health.TakeDamage(damage);
            if(Check)
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
