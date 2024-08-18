using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SelectControllerInChallenge : MonoBehaviour
{
    public GameObject player01;
    public GameObject player02;
    public Transform position01;
    public Transform position02;

    public bool SelectKeyBoard = true; // ค่าเริ่มต้นเป็นการใช้คีย์บอร์ด
    public bool Selectjoystick = false;
    public ChalllengeScripttable challlengeScripttable;

    // ตัวแปรเพื่อเก็บสคริปต์ที่ต้องการปิดใช้งาน
    private Player01MovementChallenge movementScript;
    private Player01TakeActionInChallenge actionScript;

    void Start()
    {
        // ตั้งค่าควบคุมเริ่มต้น
        UpdateControlSettings();

        // เข้าถึงสคริปต์ที่ต้องการปิดใช้งาน
        movementScript = player01.GetComponent<Player01MovementChallenge>();
        actionScript = player01.GetComponentInChildren<Player01TakeActionInChallenge>();
    }

    void Update()
    {
        
        UpdateControlSettings(); 
    }

    public void ResetScene()
    {
        // Move players to their respective positions
        StartCoroutine(WaitForMovePlayers());

        // Resume the game by setting Time.timeScale back to 1
        Time.timeScale = 1;
    }

    private void UpdateControlSettings()
    {
        if (challlengeScripttable.CurrentRound <= 5)
        {
            SelectKeyBoard = true;
            Selectjoystick = false;
        }
        else if (challlengeScripttable.CurrentRound > 5)
        {
            SelectKeyBoard = false;
            Selectjoystick = true;
        }
    }

    public void DisableScripts()
    {
        if (movementScript != null)
        {
            movementScript.enabled = false;
        }

        if (actionScript != null)
        {
            actionScript.enabled = false;
        }

        Debug.Log("Scripts disabled");
    }

    private void EnabledScripts()
    {
        if (movementScript != null)
        {
            movementScript.enabled = true;
        }

        if (actionScript != null)
        {
            actionScript.enabled = true;
        }
    }

    private void MovePlayersToPositions()
    {
        if (player01 != null && position01 != null)
        {
            player01.transform.position = position01.position;
        }

        if (player02 != null && position02 != null)
        {
            player02.transform.position = position02.position;
        }

        Debug.Log("Players moved to new positions");
    }

    IEnumerator  WaitForMovePlayers()
    {
        yield return new WaitForSeconds(2f);
        EnabledScripts();
        MovePlayersToPositions();
    }
}
