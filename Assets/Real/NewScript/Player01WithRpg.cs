using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player01WithRpg : MonoBehaviour
{
    public Player01Health player01Health;
    
    public void RPGTIme()
    {
        if (player01Health != null)
        {
            player01Health.TakeDamage(30, 0, "no");
            Debug.Log("Player01 took 30 damage from RPG.");
        }
        else
        {
            Debug.LogError("Player02Health reference is not assigned.");
        }
    }

    public void RPGOverDrive()
    {
        player01Health.TakeDamage(10, 0 , "no");
    }

    public void ShildAttack()
    {
        player01Health.TakeDamage(10, 0, "no");
    }
}
