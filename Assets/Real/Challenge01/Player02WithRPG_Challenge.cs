using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player02WithRPG_Challenge : MonoBehaviour
{
    public Player02Movement_Overdrive player02MoveInChallenge;

    public void RPGQCF()
    {
        player02MoveInChallenge.TakeDamage(0, 10, "236HS_AttackBox");
    }

    public void RPGHCBF()
    {
        player02MoveInChallenge.TakeDamage(0, 10, "632146S_AttackBox");
    }

    public void ShildAttack()
    {
        player02MoveInChallenge.TakeDamage(0, 10, "236P_AttackBox");
    }
}
