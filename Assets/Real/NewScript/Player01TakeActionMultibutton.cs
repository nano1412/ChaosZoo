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
                    if(RP && LP) return TriggerAction("BackDownRightPunchLeftPunchTrigger");
                    if(RK && LK) return TriggerAction("BackDownRightKickLeftKickTrigger");
                    if(RP && LK) return TriggerAction("BackDownRightPunchLeftKickTrigger");
                    if(RK && LP) return TriggerAction("BackDownRightKickLeftPunchTrigger");
                    if(RP) return TriggerAction("BackDownRightPunchTrigger");
                    if(LP) return TriggerAction("BackDownLeftPunchTrigger");
                    if(RK) return TriggerAction("BackDownRightKickTrigger");
                    if(LK) return TriggerAction("BackDownLeftKickTrigger");
                }
                else if(horizontal > 0.4f)
                {
                    if(RP && LP) return TriggerAction("ForwardDownRightPunchLeftPunchTrigger");
                    if(RK && LK) return TriggerAction("ForwardDownRightKickLeftKickTrigger");
                    if(RP && LK) return TriggerAction("ForwardDownRightPunchLeftKickTrigger");
                    if(RK && LP) return TriggerAction("ForwardDownRightKickLeftPunchTrigger");
                    if(RP) return TriggerAction("ForwardDownRightPunchTrigger");
                    if(LP) return TriggerAction("ForwardDownLeftPunchTrigger");
                    if(RK) return TriggerAction("ForwardDownRightKickTrigger");
                    if(LK) return TriggerAction("ForwardDownLeftKickTrigger");
                }
            }
            else if(player01Movement.faceLeft)
            {
                if(horizontal < 0f)
                {
                    if(RP && LP) return TriggerAction("ForwardDownRightPunchLeftPunchTrigger");
                    if(RK && LK) return TriggerAction("ForwardDownRightKickLeftKickTrigger");
                    if(RP && LK) return TriggerAction("ForwardDownRightPunchLeftKickTrigger");
                    if(RK && LP) return TriggerAction("ForwardDownRightKickLeftPunchTrigger");
                    if(RP) return TriggerAction("ForwardDownRightPunchTrigger");
                    if(LP) return TriggerAction("ForwardDownLeftPunchTrigger");
                    if(RK) return TriggerAction("ForwardDownRightKickTrigger");
                    if(LK) return TriggerAction("ForwardDownLeftKickTrigger");
                }
                else if(horizontal > 0.4f)
                {
                    if(RP && LP) return TriggerAction("BackDownRightPunchLeftPunchTrigger");
                    if(RK && LK) return TriggerAction("BackDownRightKickLeftKickTrigger");
                    if(RP && LK) return TriggerAction("BackDownRightPunchLeftKickTrigger");
                    if(RK && LP) return TriggerAction("BackDownRightKickLeftPunchTrigger");
                    if(RP) return TriggerAction("BackDownRightPunchTrigger");
                    if(LP) return TriggerAction("BackDownLeftPunchTrigger");
                    if(RK) return TriggerAction("BackDownRightKickTrigger");
                    if(LK) return TriggerAction("BackDownLeftKickTrigger");
                }
            }
        }
        else if(horizontal < 0f)
        {
            if(player01Movement.faceRight)
            {
                if(RP && LP) return TriggerAction("BackRightPunchLeftPunchTrigger");
                if(RK && LK) return TriggerAction("BackRightKickLeftKickTrigger");
                if(RP && LK) return TriggerAction("BackRightPunchLeftKickTrigger");
                if(RK && LP) return TriggerAction("BackRightKickLeftPunchTrigger");
            }
            else if(player01Movement.faceLeft)
            {
                if(RP && LP) return TriggerAction("ForwardRightPunchLeftPunchTrigger");
                if(RK && LK) return TriggerAction("ForwardightKickLeftKickTrigger");
                if(RP && LK) return TriggerAction("ForwardRightPunchLeftKickTrigger");
                if(RK && LP) return TriggerAction("ForwardRightKickLeftPunchTrigger");
            }
        }
        else if(horizontal > 0.4f)
        {
            if(player01Movement.faceRight)
            {
                if(RP && LP) return TriggerAction("ForwardRightPunchLeftPunchTrigger");
                if(RK && LK) return TriggerAction("ForwardightKickLeftKickTrigger");
                if(RP && LK) return TriggerAction("ForwardRightPunchLeftKickTrigger");
                if(RK && LP) return TriggerAction("ForwardRightKickLeftPunchTrigger");
            }
            else if(player01Movement.faceLeft)
            {
                if(RP && LP) return TriggerAction("BackRightPunchLeftPunchTrigger");
                if(RK && LK) return TriggerAction("BackRightKickLeftKickTrigger");
                if(RP && LK) return TriggerAction("BackRightPunchLeftKickTrigger");
                if(RK && LP) return TriggerAction("BackRightKickLeftPunchTrigger");
            } 
        }

        return false;
    }

    private bool TriggerAction(string action)
    {
        anim.SetTrigger(action);
        Debug.Log(action);
        isPerformingAction = true;
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
    ForwardRPLK
}

[System.Serializable]
public class SpeacialMoveMultibuttonToggle
{
    public SpeacialMoveMultibutton moveNameButton;
    public bool isEnabled;
}
