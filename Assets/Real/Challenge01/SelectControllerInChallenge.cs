using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectControllerInChallenge : MonoBehaviour
{
    public GameObject player01;
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
        actionScript = player01.GetComponent<Player01TakeActionInChallenge>();
    }

    void Update()
    {
        // Update control settings based on any runtime changes if needed
        // UpdateControlSettings(); 
    }

    public void ResetScene()
    {
        // Disabling the scripts before resetting the scene
        DisableScripts();
        
        StartCoroutine(ResetAfterDelay());
    }

    private void UpdateControlSettings()
    {
        if (challlengeScripttable.CurrentRound <= 5)
        {
            SelectKeyBoard = true;
            Selectjoystick = false;
            Debug.Log("Switched to Keyboard control");
        }
        else if (challlengeScripttable.CurrentRound > 5)
        {
            SelectKeyBoard = false;
            Selectjoystick = true;
            Debug.Log("Switched to Joystick control");
        }
    }

    private void DisableScripts()
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

    private IEnumerator ResetAfterDelay()
    {
        // รอ 1 วินาที
        yield return new WaitForSeconds(1f);

        // ทำการรีเซ็ตฉาก
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
