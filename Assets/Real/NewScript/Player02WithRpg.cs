using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player02WithRpg : MonoBehaviour
{
    public Player02Health player02Health;
    
    public void RPGTIme()
    {
        if (player02Health != null)
        {
            player02Health.TakeDamage(30, 0, "no");
            Debug.Log("Player02 took 30 damage from RPG.");
        }
        else
        {
            Debug.LogError("Player02Health reference is not assigned.");
        }
    }

    public void RPGOverDrive()
    {
        player02Health.TakeDamage(10, 0 , "no");
    }
}
