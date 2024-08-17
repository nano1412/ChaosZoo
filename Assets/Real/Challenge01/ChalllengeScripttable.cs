using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChalllengeScripttable", menuName = "ScriptableObjects/ChalllengeScripttable", order = 1)]
public class ChalllengeScripttable : ScriptableObject
{
    public int CurrentRound;
    public List<bool> boolList = new List<bool>();

}
