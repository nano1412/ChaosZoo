using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player02TakeActionMultibutton : MonoBehaviour
{
    public float defaultActionCooldown = 0.5f;
    public string nameCharacter;
    public GameObject player01;
    public Player02Movement player02Movement;
    public Player02Health player02Health;
    public Player02CameraSpecial player02CameraSpecial;
    public SelectController selectController;
    public bool isPerformingAction = false;
    public static bool Hits = false;
    public bool hits => Hits;
    public int specialMoveEnergy = 100;
    public bool actionHits = false;

    private Animator anim;

    [Header("Enable/Disble ACtions")]
    public List<SpeacialMoveMultibuttonToggle> specialMoveMultibuttonToggle = new List<SpeacialMoveMultibuttonToggle>();

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(isPerformingAction || player02Movement.isJump) return;
        if (HandleMultibutton()) return;

        if(selectController.Selectjoystick02)
        {
            if (Input.GetButtonDown("Player02Joystick01"))
            {
                PerformAction("RightPunch");
                Hits = false;
            }
            if (Input.GetButtonDown("Player02Joystick02"))
            {
                PerformAction("RightKick");
                Hits = false;
            }
            if (Input.GetButtonDown("Player02Joystick03"))
            {
                PerformAction("LeftPunch");
                Hits = false;
            }
            if (Input.GetButtonDown("Player02Joystick04"))
            {
                PerformAction("LeftKick");
                Hits = false;
            }
        }
        else
        {
            if (Input.GetButtonDown("Player02Bt01"))
            {
                PerformAction("RightPunch");
                Hits = false;
            }
            if (Input.GetButtonDown("Player02Bt02"))
            {
                PerformAction("RightKick");
                Hits = false;
            }
            if (Input.GetButtonDown("Player02Bt03"))
            {
                PerformAction("LeftPunch");
                Hits = false;
            }
            if (Input.GetButtonDown("Player02Bt04"))
            {
                PerformAction("LeftKick");
                Hits = false;
            }
        }
    }

    public bool HandleMultibutton()
    {
        string verticalInput = selectController.Selectjoystick02 ? "LeftAnalogY2" : "Vertical";
        string horizontalInput = selectController.Selectjoystick02 ? "LeftAnalogX2" : "Horizontal";
        bool Joystick = selectController.Selectjoystick02 ? true : false;

        bool RP = false, LP = false, RK = false, LK = false;
        float vertical = 0f, horizontal = 0f;

        if (selectController.Selectjoystick02)
        {
            RP = Input.GetButton("Player02Joystick01");
            LP = Input.GetButton("Player02Joystick03");
            RK = Input.GetButton("Player02Joystick02");
            LK = Input.GetButton("Player02Joystick04");
            vertical = -Input.GetAxis(verticalInput);
            horizontal = Input.GetAxis(horizontalInput);
        }
        else
        {
            RP = Input.GetButton("Player02Bt01");
            LP = Input.GetButton("Player02Bt03");
            RK = Input.GetButton("Player02Bt02");
            LK = Input.GetButton("Player02Bt04");
            vertical = Input.GetAxis(verticalInput);
            horizontal = Input.GetAxis(horizontalInput);
        }

        if(vertical < -0.4f)
        {
            if(player02Movement.faceRight)
            {
                if(horizontal < 0f)
                {
                    if(RP && LP && specialMoveMultibuttonToggle[4].isEnabled) return TriggerAction("BackDownRightPunchLeftPunchTrigger",specialMoveMultibuttonToggle[4].numberReset);
                    if(RK && LK && specialMoveMultibuttonToggle[5].isEnabled) return TriggerAction("BackDownRightKickLeftKickTrigger",specialMoveMultibuttonToggle[5].numberReset);
                    if(RP && LK && specialMoveMultibuttonToggle[7].isEnabled) return TriggerAction("BackDownRightPunchLeftKickTrigger",specialMoveMultibuttonToggle[7].numberReset);
                    if(RK && LP && specialMoveMultibuttonToggle[6].isEnabled) return TriggerAction("BackDownRightKickLeftPunchTrigger",specialMoveMultibuttonToggle[6].numberReset);
                    if(RP && specialMoveMultibuttonToggle[1].isEnabled) return TriggerAction("BackDownRightPunchTrigger",specialMoveMultibuttonToggle[1].numberReset);
                    if(LP && specialMoveMultibuttonToggle[0].isEnabled) return TriggerAction("BackDownLeftPunchTrigger",specialMoveMultibuttonToggle[0].numberReset);
                    if(RK && specialMoveMultibuttonToggle[3].isEnabled) return TriggerAction("BackDownRightKickTrigger",specialMoveMultibuttonToggle[3].numberReset);
                    if(LK && specialMoveMultibuttonToggle[2].isEnabled) return TriggerAction("BackDownLeftKickTrigger",specialMoveMultibuttonToggle[2].numberReset);
                }
                else if(horizontal > 0.4f)
                {
                    if(RP && LP && specialMoveMultibuttonToggle[12].isEnabled) return TriggerAction("ForwardDownRightPunchLeftPunchTrigger",specialMoveMultibuttonToggle[12].numberReset);
                    if(RK && LK && specialMoveMultibuttonToggle[13].isEnabled) return TriggerAction("ForwardDownRightKickLeftKickTrigger",specialMoveMultibuttonToggle[13].numberReset);
                    if(RP && LK && specialMoveMultibuttonToggle[15].isEnabled) return TriggerAction("ForwardDownRightPunchLeftKickTrigger",specialMoveMultibuttonToggle[15].numberReset);
                    if(RK && LP && specialMoveMultibuttonToggle[14].isEnabled) return TriggerAction("ForwardDownRightKickLeftPunchTrigger",specialMoveMultibuttonToggle[14].numberReset);
                    if(RP && specialMoveMultibuttonToggle[9].isEnabled) return TriggerAction("ForwardDownRightPunchTrigger",specialMoveMultibuttonToggle[9].numberReset);
                    if(LP && specialMoveMultibuttonToggle[8].isEnabled) return TriggerAction("ForwardDownLeftPunchTrigger",specialMoveMultibuttonToggle[8].numberReset);
                    if(RK && specialMoveMultibuttonToggle[11].isEnabled) return TriggerAction("ForwardDownRightKickTrigger",specialMoveMultibuttonToggle[11].numberReset);
                    if(LK && specialMoveMultibuttonToggle[10].isEnabled) return TriggerAction("ForwardDownLeftKickTrigger",specialMoveMultibuttonToggle[10].numberReset);
                }
            }
            else if(player02Movement.faceLeft)
            {
                if(horizontal < 0f)
                {
                    if(RP && LP && specialMoveMultibuttonToggle[12].isEnabled) return TriggerAction("ForwardDownRightPunchLeftPunchTrigger",specialMoveMultibuttonToggle[12].numberReset);
                    if(RK && LK && specialMoveMultibuttonToggle[13].isEnabled) return TriggerAction("ForwardDownRightKickLeftKickTrigger",specialMoveMultibuttonToggle[13].numberReset);
                    if(RP && LK && specialMoveMultibuttonToggle[15].isEnabled) return TriggerAction("ForwardDownRightPunchLeftKickTrigger",specialMoveMultibuttonToggle[15].numberReset);
                    if(RK && LP && specialMoveMultibuttonToggle[14].isEnabled) return TriggerAction("ForwardDownRightKickLeftPunchTrigger",specialMoveMultibuttonToggle[14].numberReset);
                    if(RP && specialMoveMultibuttonToggle[9].isEnabled) return TriggerAction("ForwardDownRightPunchTrigger",specialMoveMultibuttonToggle[9].numberReset);
                    if(LP && specialMoveMultibuttonToggle[8].isEnabled) return TriggerAction("ForwardDownLeftPunchTrigger",specialMoveMultibuttonToggle[8].numberReset);
                    if(RK && specialMoveMultibuttonToggle[11].isEnabled) return TriggerAction("ForwardDownRightKickTrigger",specialMoveMultibuttonToggle[11].numberReset);
                    if(LK && specialMoveMultibuttonToggle[10].isEnabled) return TriggerAction("ForwardDownLeftKickTrigger",specialMoveMultibuttonToggle[10].numberReset);
                }
                else if(horizontal > 0.4f)
                {
                    if(RP && LP && specialMoveMultibuttonToggle[4].isEnabled) return TriggerAction("BackDownRightPunchLeftPunchTrigger",specialMoveMultibuttonToggle[4].numberReset);
                    if(RK && LK && specialMoveMultibuttonToggle[5].isEnabled) return TriggerAction("BackDownRightKickLeftKickTrigger",specialMoveMultibuttonToggle[5].numberReset);
                    if(RP && LK && specialMoveMultibuttonToggle[7].isEnabled) return TriggerAction("BackDownRightPunchLeftKickTrigger",specialMoveMultibuttonToggle[7].numberReset);
                    if(RK && LP && specialMoveMultibuttonToggle[6].isEnabled) return TriggerAction("BackDownRightKickLeftPunchTrigger",specialMoveMultibuttonToggle[6].numberReset);
                    if(RP && specialMoveMultibuttonToggle[1].isEnabled) return TriggerAction("BackDownRightPunchTrigger",specialMoveMultibuttonToggle[1].numberReset);
                    if(LP && specialMoveMultibuttonToggle[0].isEnabled) return TriggerAction("BackDownLeftPunchTrigger",specialMoveMultibuttonToggle[0].numberReset);
                    if(RK && specialMoveMultibuttonToggle[3].isEnabled) return TriggerAction("BackDownRightKickTrigger",specialMoveMultibuttonToggle[3].numberReset);
                    if(LK && specialMoveMultibuttonToggle[2].isEnabled) return TriggerAction("BackDownLeftKickTrigger",specialMoveMultibuttonToggle[2].numberReset);
                }
            }
            if(RP && LP && specialMoveMultibuttonToggle[25].isEnabled) return TriggerAction("DownRightPunchLeftPunchTrigger",specialMoveMultibuttonToggle[25].numberReset);
            if(RK && LK && specialMoveMultibuttonToggle[26].isEnabled) return TriggerAction("DownRightKickLeftKickTrigger",specialMoveMultibuttonToggle[26].numberReset);
            if(RP && LK && specialMoveMultibuttonToggle[28].isEnabled) return TriggerAction("DownRightPunchLeftKickTrigger",specialMoveMultibuttonToggle[28].numberReset);
            if(RK && LP && specialMoveMultibuttonToggle[27].isEnabled) return TriggerAction("DownRightKickLeftPunchTrigger",specialMoveMultibuttonToggle[27].numberReset);
            
        }
        else if(horizontal < 0f)
        {
            if(player02Movement.faceRight)
            {
                if(RP && LP && specialMoveMultibuttonToggle[16].isEnabled) return TriggerAction("BackRightPunchLeftPunchTrigger",specialMoveMultibuttonToggle[16].numberReset);
                if(RK && LK && specialMoveMultibuttonToggle[17].isEnabled) return TriggerAction("BackRightKickLeftKickTrigger",specialMoveMultibuttonToggle[17].numberReset);
                if(RP && LK && specialMoveMultibuttonToggle[19].isEnabled) return TriggerAction("BackRightPunchLeftKickTrigger",specialMoveMultibuttonToggle[19].numberReset);
                if(RK && LP && specialMoveMultibuttonToggle[18].isEnabled) return TriggerAction("BackRightKickLeftPunchTrigger",specialMoveMultibuttonToggle[18].numberReset);
            }
            else if(player02Movement.faceLeft)
            {
                if(RP && LP && RK && LK && specialMoveMultibuttonToggle[24].isEnabled) return SpecialTriggerAction("ForwardRightPunchLeftPunchRightKickLeftKickTrigger", specialMoveMultibuttonToggle[24].numberReset);
                if(RP && LP && specialMoveMultibuttonToggle[20].isEnabled) return TriggerAction("ForwardRightPunchLeftPunchTrigger",specialMoveMultibuttonToggle[20].numberReset);
                if(RK && LK && specialMoveMultibuttonToggle[21].isEnabled) return TriggerAction("ForwardRightKickLeftKickTrigger",specialMoveMultibuttonToggle[21].numberReset);
                if(RP && LK && specialMoveMultibuttonToggle[23].isEnabled) return TriggerAction("ForwardRightPunchLeftKickTrigger",specialMoveMultibuttonToggle[23].numberReset);
                if(RK && LP && specialMoveMultibuttonToggle[22].isEnabled) return TriggerAction("ForwardRightKickLeftPunchTrigger",specialMoveMultibuttonToggle[22].numberReset);
            }
        }
        else if(horizontal > 0.4f)
        {
            if(player02Movement.faceRight)
            {
                if(RP && LP && RK && LK && specialMoveMultibuttonToggle[24].isEnabled) return SpecialTriggerAction("ForwardRightPunchLeftPunchRightKickLeftKickTrigger", specialMoveMultibuttonToggle[24].numberReset);
                if(RP && LP && specialMoveMultibuttonToggle[20].isEnabled) return TriggerAction("ForwardRightPunchLeftPunchTrigger",specialMoveMultibuttonToggle[20].numberReset);
                if(RK && LK && specialMoveMultibuttonToggle[21].isEnabled) return TriggerAction("ForwardRightKickLeftKickTrigger",specialMoveMultibuttonToggle[21].numberReset);
                if(RP && LK && specialMoveMultibuttonToggle[23].isEnabled) return TriggerAction("ForwardRightPunchLeftKickTrigger",specialMoveMultibuttonToggle[23].numberReset);
                if(RK && LP && specialMoveMultibuttonToggle[24].isEnabled) return TriggerAction("ForwardRightKickLeftPunchTrigger",specialMoveMultibuttonToggle[24].numberReset);
            }
            else if(player02Movement.faceLeft)
            {
                if(RP && LP && specialMoveMultibuttonToggle[16].isEnabled) return TriggerAction("BackRightPunchLeftPunchTrigger",specialMoveMultibuttonToggle[16].numberReset);
                if(RK && LK && specialMoveMultibuttonToggle[17].isEnabled) return TriggerAction("BackRightKickLeftKickTrigger",specialMoveMultibuttonToggle[17].numberReset);
                if(RP && LK && specialMoveMultibuttonToggle[19].isEnabled) return TriggerAction("BackRightPunchLeftKickTrigger",specialMoveMultibuttonToggle[19].numberReset);
                if(RK && LP && specialMoveMultibuttonToggle[18].isEnabled) return TriggerAction("BackRightKickLeftPunchTrigger",specialMoveMultibuttonToggle[18].numberReset);
            } 
        }
        if(RP && LP && RK && LK && specialMoveMultibuttonToggle[33].isEnabled) return SpecialTriggerAction("RightPunchLeftPunchRightKickLeftKickTrigger", specialMoveMultibuttonToggle[33].numberReset);
        if(RP && LP && specialMoveMultibuttonToggle[29].isEnabled) return TriggerAction("RightPunchLeftPunchTrigger",specialMoveMultibuttonToggle[29].numberReset);
        if(RK && LK && specialMoveMultibuttonToggle[30].isEnabled) return TriggerAction("RightKickLeftKickTrigger",specialMoveMultibuttonToggle[30].numberReset);
        if(RP && LK && specialMoveMultibuttonToggle[32].isEnabled) return TriggerAction("RightPunchLeftKickTrigger",specialMoveMultibuttonToggle[32].numberReset);
        if(RK && LP && specialMoveMultibuttonToggle[31].isEnabled) return TriggerAction("RightKickLeftPunchTrigger",specialMoveMultibuttonToggle[31].numberReset);

        return false;
    }

    private bool TriggerAction(string action, int numberReset)
    {
        anim.SetTrigger(action);
        isPerformingAction = true;
        player02Movement.isPerformingAction = true;
        Hits = false;
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

    private bool SpecialTriggerAction(string action, int numberReset)
    {
        if(specialMoveEnergy < 50)
        {
            return false;
        }
        else
        {
            player02CameraSpecial.SpecialCapybaraCamare();
            anim.SetTrigger(action);
            specialMoveEnergy -= 50;
            isPerformingAction = true;
            player02Movement.isPerformingAction = true;
            StartCoroutine(ResetIsPerformingAction(2.5f));
            return true;
        }
        return false;
    }

    private void PerformAction(string actionName)
    {
        isPerformingAction = true;
        player02Movement.isPerformingAction = true;

        string verticalInput = selectController.Selectjoystick02 ? "LeftAnalogY2" : "Vertical";
        string horizontalInput = selectController.Selectjoystick02 ? "LeftAnalogX2" : "Horizontal";
        bool Joystick = selectController.Selectjoystick02 ? true : false;

        if(Joystick)
        {
            if(-Input.GetAxis(verticalInput) < -0.4f)
            {
                anim.SetTrigger("Crouch" + actionName + "Trigger");
            }
             else if(player02Movement.faceRight)
            {
                if(Input.GetAxis(horizontalInput) < 0f)
                {
                    anim.SetTrigger("BackSpecial" + actionName + "Trigger");
                }
                else if(Input.GetAxis(horizontalInput) > 0.4f)
                {
                    anim.SetTrigger("Special" + actionName + "Trigger");
                }
                else
                {
                    anim.SetTrigger(actionName + "Trigger");
                }
            }
            else if(player02Movement.faceLeft)
            {
                if(Input.GetAxis(horizontalInput) > 0.4f)
                {
                    anim.SetTrigger("BackSpecial" + actionName + "Trigger");
                }
                else if(Input.GetAxis(horizontalInput) < 0f)
                {
                    anim.SetTrigger("Special" + actionName + "Trigger");
                }
                else
                {
                    anim.SetTrigger(actionName + "Trigger");
                }
            }
        }
        else
        {
            if(Input.GetAxis(verticalInput) < -0.4f)
            {
                anim.SetTrigger("Crouch" + actionName + "Trigger");
            }
            else if(player02Movement.faceRight)
            {
                if(Input.GetAxis(horizontalInput) < 0f)
                {
                    anim.SetTrigger("BackSpecial" + actionName + "Trigger");
                }
                else if(Input.GetAxis(horizontalInput) > 0.4f)
                {
                    anim.SetTrigger("Special" + actionName + "Trigger");
                }
                else
                {
                    anim.SetTrigger(actionName + "Trigger");
                }
            }
            else if(player02Movement.faceLeft)
            {
                if(Input.GetAxis(horizontalInput) > 0.4f)
                {
                    anim.SetTrigger("BackSpecial" + actionName + "Trigger");
                }
                else if(Input.GetAxis(horizontalInput) < 0f)
                {
                    anim.SetTrigger("Special" + actionName + "Trigger");
                }
                else
                {
                    anim.SetTrigger(actionName + "Trigger");
                }
            }
            StartCoroutine(ResetIsPerformingAction(0.5f));
        }
    }

    IEnumerator ResetIsPerformingAction(float time)
    {
        yield return new WaitForSeconds(time);
        isPerformingAction = false;
        player02Movement.isPerformingAction = false;
    }
}


