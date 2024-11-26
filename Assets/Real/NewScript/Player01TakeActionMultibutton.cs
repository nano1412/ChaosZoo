using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player01TakeActionMultibutton : MonoBehaviour
{
    public float defaultActionCooldown = 0.5f;
    public string nameCharacter;
    public GameObject player01;
    public Player01Movement player01Movement;
    public Player01Health player01Health;
    public SelectController selectController;
    public bool isPerformingAction = false;
    public static bool Hits = false;
    public bool hits => Hits;

    private Animator anim;

    [Header("Enable/Disble ACtions")]
    public List<SpeacialMoveMultibuttonToggle> specialMoveMultibuttonToggle = new List<SpeacialMoveMultibuttonToggle>();

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(isPerformingAction) return;
        if (HandleMultibutton()) return;

        if(selectController.Selectjoystick01)
        {
            if (Input.GetButtonDown("Player01Joystick01"))
            {
                PerformAction("RightPunch");
                Hits = false;
            }
            if (Input.GetButtonDown("Player01Joystick02"))
            {
                PerformAction("RightKick");
                Hits = false;
            }
            if (Input.GetButtonDown("Player01Joystick03"))
            {
                PerformAction("LeftPunch");
                Hits = false;
            }
            if (Input.GetButtonDown("Player01Joystick04"))
            {
                PerformAction("LeftKick");
                Hits = false;
            }
        }
        else
        {
            if (Input.GetButtonDown("Player01Bt01"))
            {
                PerformAction("RightPunch");
                Hits = false;
            }
            if (Input.GetButtonDown("Player01Bt02"))
            {
                PerformAction("RightKick");
                Hits = false;
            }
            if (Input.GetButtonDown("Player01Bt03"))
            {
                PerformAction("LeftPunch");
                Hits = false;
            }
            if (Input.GetButtonDown("Player01Bt04"))
            {
                PerformAction("LeftKick");
                Hits = false;
            }
        }
    }
    public bool HandleMultibutton()
    {
        string verticalInput = selectController.Selectjoystick01 ? "LeftAnalogY1" : "Vertical";
        string horizontalInput = selectController.Selectjoystick01 ? "LeftAnalogX1" : "Horizontal";
        bool Joystick = selectController.Selectjoystick01 ? true : false;

        bool RP = false, LP = false, RK = false, LK = false;
        float vertical = 0f, horizontal = 0f;

        if (selectController.Selectjoystick01)
        {
            RP = Input.GetButton("Player01Joystick01");
            LP = Input.GetButton("Player01Joystick03");
            RK = Input.GetButton("Player01Joystick02");
            LK = Input.GetButton("Player01Joystick04");
            vertical = -Input.GetAxis(verticalInput);
            horizontal = Input.GetAxis(horizontalInput);
        }
        else
        {
            RP = Input.GetButton("Player01Bt01");
            LP = Input.GetButton("Player01Bt03");
            RK = Input.GetButton("Player01Bt02");
            LK = Input.GetButton("Player01Bt04");
            vertical = Input.GetAxis(verticalInput);
            horizontal = Input.GetAxis(horizontalInput);
        }

        if(vertical < -0.4f)
        {
            if(player01Movement.faceRight)
            {
                if(horizontal < 0f)
                {
                    if(RP && LP) return TriggerAction("BackDownRightPunchLeftPunchTrigger",1);
                    if(RK && LK) return TriggerAction("BackDownRightKickLeftKickTrigger",1);
                    if(RP && LK) return TriggerAction("BackDownRightPunchLeftKickTrigger",1);
                    if(RK && LP) return TriggerAction("BackDownRightKickLeftPunchTrigger",1);
                    if(RP) return TriggerAction("BackDownRightPunchTrigger",1);
                    if(LP) return TriggerAction("BackDownLeftPunchTrigger",1);
                    if(RK) return TriggerAction("BackDownRightKickTrigger",1);
                    if(LK) return TriggerAction("BackDownLeftKickTrigger",1);
                }
                else if(horizontal > 0.4f)
                {
                    if(RP && LP) return TriggerAction("ForwardDownRightPunchLeftPunchTrigger",1);
                    if(RK && LK) return TriggerAction("ForwardDownRightKickLeftKickTrigger",1);
                    if(RP && LK) return TriggerAction("ForwardDownRightPunchLeftKickTrigger",1);
                    if(RK && LP) return TriggerAction("ForwardDownRightKickLeftPunchTrigger",1);
                    if(RP) return TriggerAction("ForwardDownRightPunchTrigger",1);
                    if(LP) return TriggerAction("ForwardDownLeftPunchTrigger",1);
                    if(RK) return TriggerAction("ForwardDownRightKickTrigger",1);
                    if(LK) return TriggerAction("ForwardDownLeftKickTrigger",1);
                }
            }
            else if(player01Movement.faceLeft)
            {
                if(horizontal < 0f)
                {
                    if(RP && LP) return TriggerAction("ForwardDownRightPunchLeftPunchTrigger",1);
                    if(RK && LK) return TriggerAction("ForwardDownRightKickLeftKickTrigger",1);
                    if(RP && LK) return TriggerAction("ForwardDownRightPunchLeftKickTrigger",1);
                    if(RK && LP) return TriggerAction("ForwardDownRightKickLeftPunchTrigger",1);
                    if(RP) return TriggerAction("ForwardDownRightPunchTrigger",1);
                    if(LP) return TriggerAction("ForwardDownLeftPunchTrigger",1);
                    if(RK) return TriggerAction("ForwardDownRightKickTrigger",1);
                    if(LK) return TriggerAction("ForwardDownLeftKickTrigger",1);
                }
                else if(horizontal > 0.4f)
                {
                    if(RP && LP) return TriggerAction("BackDownRightPunchLeftPunchTrigger",1);
                    if(RK && LK) return TriggerAction("BackDownRightKickLeftKickTrigger",1);
                    if(RP && LK) return TriggerAction("BackDownRightPunchLeftKickTrigger",1);
                    if(RK && LP) return TriggerAction("BackDownRightKickLeftPunchTrigger",1);
                    if(RP) return TriggerAction("BackDownRightPunchTrigger",1);
                    if(LP) return TriggerAction("BackDownLeftPunchTrigger",1);
                    if(RK) return TriggerAction("BackDownRightKickTrigger",1);
                    if(LK) return TriggerAction("BackDownLeftKickTrigger",1);
                }
            }
            if(RP && LP) return TriggerAction("DownRightPunchLeftPunchTrigger",1);
            if(RK && LK) return TriggerAction("DownRightKickLeftKickTrigger",1);
            if(RP && LK) return TriggerAction("DownRightPunchLeftKickTrigger",1);
            if(RK && LP) return TriggerAction("DownRightKickLeftPunchTrigger",1);
            
        }
        else if(horizontal < 0f)
        {
            if(player01Movement.faceRight)
            {
                if(RP && LP) return TriggerAction("BackRightPunchLeftPunchTrigger",1);
                if(RK && LK) return TriggerAction("BackRightKickLeftKickTrigger",1);
                if(RP && LK) return TriggerAction("BackRightPunchLeftKickTrigger",1);
                if(RK && LP) return TriggerAction("BackRightKickLeftPunchTrigger",1);
            }
            else if(player01Movement.faceLeft)
            {
                if(RP && LP && RK && LK) return TriggerAction("ForwardRightPunchLeftPunchRightKickLeftKickTrigger", 1);
                if(RP && LP) return TriggerAction("ForwardRightPunchLeftPunchTrigger",1);
                if(RK && LK) return TriggerAction("ForwardRightKickLeftKickTrigger",1);
                if(RP && LK) return TriggerAction("ForwardRightPunchLeftKickTrigger",1);
                if(RK && LP) return TriggerAction("ForwardRightKickLeftPunchTrigger",1);
            }
        }
        else if(horizontal > 0.4f)
        {
            if(player01Movement.faceRight)
            {
                if(RP && LP && RK && LK) return TriggerAction("ForwardRightPunchLeftPunchRightKickLeftKickTrigger", 1);
                if(RP && LP) return TriggerAction("ForwardRightPunchLeftPunchTrigger",1);
                if(RK && LK) return TriggerAction("ForwardRightKickLeftKickTrigger",1);
                if(RP && LK) return TriggerAction("ForwardRightPunchLeftKickTrigger",1);
                if(RK && LP) return TriggerAction("ForwardRightKickLeftPunchTrigger",1);
            }
            else if(player01Movement.faceLeft)
            {
                if(RP && LP) return TriggerAction("BackRightPunchLeftPunchTrigger",1);
                if(RK && LK) return TriggerAction("BackRightKickLeftKickTrigger",1);
                if(RP && LK) return TriggerAction("BackRightPunchLeftKickTrigger",1);
                if(RK && LP) return TriggerAction("BackRightKickLeftPunchTrigger",1);
            } 
        }
        if(RP && LP && RK && LK) return TriggerAction("RightPunchLeftPunchRightKickLeftKickTrigger", 1);
        if(RP && LP) return TriggerAction("RightPunchLeftPunchTrigger",1);
        if(RK && LK) return TriggerAction("RightKickLeftKickTrigger",1);
        if(RP && LK) return TriggerAction("RightPunchLeftKickTrigger",1);
        if(RK && LP) return TriggerAction("RightKickLeftPunchTrigger",1);

        return false;
    }

    private bool TriggerAction(string action, int numberReset)
    {
        anim.SetTrigger(action);
        isPerformingAction = true;
        player01Movement.isPerformingAction = true;
        switch(numberReset)
        {
            case 1: StartCoroutine(ResetIsPerformingAction(0.5f));
            break;
            case 2: StartCoroutine(ResetIsPerformingAction(1f));
            break;
            case 3: StartCoroutine(ResetIsPerformingAction(2f));
            break;
        }

        StartCoroutine(ResetIsPerformingAction(0.5f));
        return true;
    }

    private void PerformAction(string actionName)
    {
        isPerformingAction = true;
        player01Movement.isPerformingAction = true;

        string verticalInput = selectController.Selectjoystick01 ? "LeftAnalogY1" : "Vertical";
        string horizontalInput = selectController.Selectjoystick01 ? "LeftAnalogX1" : "Horizontal";
        bool Joystick = selectController.Selectjoystick01 ? true : false;

        if(Input.GetAxis(verticalInput) < -0.4f)
        {
            anim.SetTrigger("Crouch" + actionName + "Trigger");
            Debug.Log("Crouch" + actionName);
        }
        else if(player01Movement.faceRight)
        {
            if(Input.GetAxis(horizontalInput) < 0f)
            {
                anim.SetTrigger("BackSpecial" + actionName + "Trigger");
                Debug.Log("BackSpecial" + actionName);
            }
            else if(Input.GetAxis(horizontalInput) > 0.4f)
            {
                anim.SetTrigger("Special" + actionName + "Trigger");
                Debug.Log("Special" + actionName);
            }
            else
            {
                anim.SetTrigger(actionName + "Trigger");
                Debug.Log(actionName + "Trigger");
            }
        }
        else if(player01Movement.faceLeft)
        {
            if(Input.GetAxis(horizontalInput) > 0.4f)
            {
                anim.SetTrigger("BackSpecial" + actionName + "Trigger");
                Debug.Log("BackSpecial" + actionName);
            }
            else if(Input.GetAxis(horizontalInput) < 0f)
            {
                anim.SetTrigger("Special" + actionName + "Trigger");
                Debug.Log("Special" + actionName);
            }
            else
            {
                anim.SetTrigger(actionName + "Trigger");
                Debug.Log(actionName + "Trigger");
            }
        }
        StartCoroutine(ResetIsPerformingAction(0.5f));
    }

    IEnumerator ResetIsPerformingAction(float time)
    {
        yield return new WaitForSeconds(time);
        isPerformingAction = false;
        player01Movement.isPerformingAction = false;
    }
}

public enum SpeacialMoveMultibutton
{
    BackDownLP,
    BackDownRP,
    BackDownLK,
    BackDownRK,
    BackDownLPRP,
    BackDownLKRK,
    BackDownLPRK,
    BackDownRPLK,
    ForwardDownLP,
    ForwardDownRP,
    ForwardDownLK,
    ForwardDownRK,
    ForwardDownLPRP,
    ForwardDownLKRK,
    ForwardDownLPRK,
    ForwardDownRPLK,
    BackLPRP,
    BackLKRK,
    BackLPRK,
    BackRPLK,
    ForwardLPRP,
    ForwardLKRK,
    ForwardLPRK,
    ForwardRPLK,
    DownLPRP,
    DownLKRK,
    DownLPRK,
    DownLKRP,
    LPRP,
    LKRK,
    LPRK,
    LKRP,
    LPRPLKRK,
    ForwardLPRPLKRK

}


[System.Serializable]
public class SpeacialMoveMultibuttonToggle
{
    public SpeacialMoveMultibutton moveNameButton;
    public bool isEnabled;
}
