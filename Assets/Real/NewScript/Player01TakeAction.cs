using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player01TakeAction : MonoBehaviour
{
    public float defaultActionCooldown = 0.5f;
    public string nameCharacter;
    public GameObject player01;
    public Player01Movement player01Movement;
    public Player01CameraSpecial player01CameraSpecial;
    public Player01Health player01Health;
    public SelectController selectController;
    public bool isPerformingAction = false;
    public static bool Hits = false;
    public bool hits => Hits;

    private Animator anim;

    public enum InputState {None, Down, Forward, Backward, ForwardAgain}
    public InputState inputState = InputState.None;
    public bool isQCInProgress = false;
    public bool isHCBInProgress = false;
    public float actionCooldown = 0.1f;
    public float lastInputTime;
    public float inputBufferTime = 0.2f;
    public int specialMoveEnergy = 100;
    private const float holdThreshold = 0.4f;
    public bool holdbuttonVertical = false;
    public bool holdbuttonHorizontal = false;
    public float holdTimeVertical;
    public float holdTimeHorizontal;
    public int inputCount = 0; // ตัวแปรสำหรับนับจำนวนอินพุต

    [Header("Enable/Disable Actions")]
    public List<SpecialMoveToggle> specialMoveToggles = new List<SpecialMoveToggle>()
    {
        new SpecialMoveToggle { moveName = SpecialMove.QCF_Punch, isEnabled = true },
        new SpecialMoveToggle { moveName = SpecialMove.QCF_Kick, isEnabled = true },
        new SpecialMoveToggle { moveName = SpecialMove.QCF_Slash, isEnabled = true },
        new SpecialMoveToggle { moveName = SpecialMove.QCF_HeavySlash, isEnabled = true },
        new SpecialMoveToggle { moveName = SpecialMove.QCB_Punch, isEnabled = true },
        new SpecialMoveToggle { moveName = SpecialMove.QCB_Kick, isEnabled = true },
        new SpecialMoveToggle { moveName = SpecialMove.QCB_Slash, isEnabled = true },
        new SpecialMoveToggle { moveName = SpecialMove.QCB_HeavySlash, isEnabled = true },
        new SpecialMoveToggle { moveName = SpecialMove.HCBF_Punch, isEnabled = true },
        new SpecialMoveToggle { moveName = SpecialMove.HCBF_Kick, isEnabled = true },
        new SpecialMoveToggle { moveName = SpecialMove.HCBF_Slash, isEnabled = true },
        new SpecialMoveToggle { moveName = SpecialMove.HCBF_HeavySlash, isEnabled = true },
    };
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        string verticalInput = selectController.Selectjoystick01 ? "LeftAnalogY1" : "Vertical";
        string horizontalInput = selectController.Selectjoystick01 ? "LeftAnalogX1" : "Horizontal";
        bool Joystick = selectController.Selectjoystick01 ? true : false;

        HandleQCF();
        HandleQCB();
        HandleHCB();
        HandleHCBF();
        
        if(Input.GetAxis(verticalInput) < -0.4f)
        {
            holdTimeVertical += Time.deltaTime;
            if(holdTimeVertical > 0.2f)
            {
                isQCInProgress = false;
                isHCBInProgress = false;
                holdbuttonVertical = true;
            }
        }
        if(Input.GetAxis(horizontalInput) > 0.4f && player01Movement.faceRight)
        {
            holdTimeHorizontal += Time.deltaTime;
            if(holdTimeHorizontal > 0.2f)
            {
                isQCInProgress = false;
                isHCBInProgress = false;
                holdbuttonHorizontal = true;
            } 
        }
        if(Input.GetAxis(horizontalInput) < -0.4f && !player01Movement.faceRight)
        {
            holdTimeHorizontal += Time.deltaTime;
            if(holdTimeHorizontal > 0.2f)
            {
                isQCInProgress = false;
                isHCBInProgress = false;
                holdbuttonHorizontal = true;
            } 
        }
        else if (Input.GetAxis(verticalInput) == 0 && Input.GetAxis(horizontalInput) == 0)
        {
            holdbuttonVertical = false;
            holdbuttonHorizontal = false;
            holdTimeVertical = 0;
            holdTimeHorizontal = 0;
        }


        if (isPerformingAction || isQCInProgress || isHCBInProgress || player01Movement.isJump) return;

        if (selectController.Selectjoystick01)
        {
            if (Input.GetButtonDown("Player01Joystick01"))
            {
                PerformAction("Punch");
                Hits = false;
            }
            if (Input.GetButtonDown("Player01Joystick02"))
            {
                PerformAction("Kick");
                Hits = false;
            }
            if (Input.GetButtonDown("Player01Joystick03"))
            {
                PerformAction("Slash");
                Hits = false;
            }
            if (Input.GetButtonDown("Player01Joystick04"))
            {
                PerformAction("HeavySlash");
                Hits = false;
            }
        }
        else
        {
            if (Input.GetButtonDown("Player01Bt01"))
            {
                PerformAction("Punch");
                Hits = false;
            }
            if (Input.GetButtonDown("Player01Bt02"))
            {
                PerformAction("Kick");
                Hits = false;
            }
            if (Input.GetButtonDown("Player01Bt03"))
            {
                PerformAction("Slash");
                Hits = false;
            }
            if (Input.GetButtonDown("Player01Bt04"))
            {
                PerformAction("HeavySlash");
                Hits = false;
            }
        }
    }

    private void HandleQCF()
    {
        if (isPerformingAction || isHCBInProgress) return;

        string verticalInput = selectController.Selectjoystick01 ? "LeftAnalogY1" : "Vertical";
        string horizontalInput = selectController.Selectjoystick01 ? "LeftAnalogX1" : "Horizontal";
        bool Joystick = selectController.Selectjoystick01 ? true : false;

        if(Joystick)
        {
            if(-Input.GetAxis(verticalInput) < -0.4f && !holdbuttonVertical && !isQCInProgress)
            {
                inputState = InputState.Down;
                lastInputTime = Time.time;
                isQCInProgress = true;
            }

            else if(inputState == InputState.Down && Time.time - lastInputTime <= inputBufferTime && isQCInProgress)
            {
                if(player01Movement.faceRight)
                {
                    if(Input.GetAxis(horizontalInput) > 0.4f)
                    {
                        inputState = InputState.Forward;
                        lastInputTime = Time.time;
                    }
                }
                else
                {
                    if(Input.GetAxis(horizontalInput) < -0.4f)
                    {
                        inputState = InputState.Forward;
                        lastInputTime = Time.time;
                    }
                }
            }
            else if (inputState == InputState.Forward && Time.time - lastInputTime <= inputBufferTime && isQCInProgress)
            {
                if(Input.GetButtonDown("Player01Joystick01") && specialMoveToggles[0].isEnabled)
                {
                    ActionQCF("Punch");
                    Hits = false;
                }
                else if(Input.GetButtonDown("Player01Joystick02") && specialMoveToggles[1].isEnabled)
                {
                    ActionQCF("Kick");
                    Hits = false;
                }
                else if(Input.GetButtonDown("Player01Joystick03") && specialMoveToggles[2].isEnabled)
                {
                    ActionQCF("Slash");
                    Hits = false;
                }
                else if(Input.GetButtonDown("Player01Joystick04") && specialMoveToggles[3].isEnabled)
                {
                    ActionQCF("HeavySlash");
                    Hits = false;
                }
            }
            else if (Time.time - lastInputTime > inputBufferTime)
            {
                inputState = InputState.None;
                isQCInProgress = false;
            }
        }
        else
        {
            if(Input.GetAxis(verticalInput) < -0.4f && !holdbuttonVertical && !isQCInProgress)
            {
                inputState = InputState.Down;
                lastInputTime = Time.time;
                isQCInProgress = true;
            }

            else if(inputState == InputState.Down && Time.time - lastInputTime <= inputBufferTime && isQCInProgress)
            {
                if(player01Movement.faceRight)
                {
                    if(Input.GetAxis(horizontalInput) > 0.4f)
                    {
                        inputState = InputState.Forward;
                        lastInputTime = Time.time;
                    }
                }
                else
                {
                    if(Input.GetAxis(horizontalInput) < -0.4f)
                    {
                        inputState = InputState.Forward;
                        lastInputTime = Time.time;
                    }
                }
            }
            else if (inputState == InputState.Forward && Time.time - lastInputTime <= inputBufferTime && isQCInProgress)
            {
                if(Input.GetButtonDown("Player01Bt01") && specialMoveToggles[0].isEnabled)
                {
                    ActionQCF("Punch");
                    Hits = false;
                }
                else if(Input.GetButtonDown("Player01Bt02") && specialMoveToggles[1].isEnabled)
                {
                    ActionQCF("Kick");
                    Hits = false;
                }
                else if(Input.GetButtonDown("Player01Bt03") && specialMoveToggles[2].isEnabled)
                {
                    ActionQCF("Slash");
                    Hits = false;
                }
                else if(Input.GetButtonDown("Player01Bt04") && specialMoveToggles[3].isEnabled)
                {
                    ActionQCF("HeavySlash");
                    Hits = false;
                }
            }
            else if (Time.time - lastInputTime > inputBufferTime)
            {
                inputState = InputState.None;
                isQCInProgress = false;
            }
        }

    }
    private void HandleQCB()
    {   
        if(isPerformingAction || isHCBInProgress) return;
        
        string verticalInput = selectController.Selectjoystick01 ? "LeftAnalogY1" : "Vertical";
        string horizontalInput = selectController.Selectjoystick01 ? "LeftAnalogX1" : "Horizontal";
        bool Joystick = selectController.Selectjoystick01 ? true : false;


        if(Joystick)
        {
            if(-Input.GetAxis(verticalInput) < -0.4f && !holdbuttonVertical && !isQCInProgress)
            {
                inputState = InputState.Down;
                lastInputTime = Time.time;
                isQCInProgress = true;
            }
            else if(inputState == InputState.Down && Time.time - lastInputTime <= inputBufferTime && isQCInProgress)
            {
                if(player01Movement.faceRight)
                {
                    if(Input.GetAxis(horizontalInput) < -0.4f)
                    {
                        inputState = InputState.Backward;
                        lastInputTime = Time.time;
                    }
                }
                else
                {
                    if(Input.GetAxis(horizontalInput) > 0.4f)
                    {
                        inputState = InputState.Backward;
                        lastInputTime = Time.time;
                    }
                }
            }
            else if (inputState == InputState.Backward && Time.time - lastInputTime <= inputBufferTime && isQCInProgress)
            {
                if(Input.GetButtonDown("Player01Joystick01") && specialMoveToggles[4].isEnabled)
                {
                    ActionQCB("Punch");
                    Hits = false;
                }
                else if(Input.GetButtonDown("Player01Joystick02") && specialMoveToggles[5].isEnabled)
                {
                    ActionQCB("Kick");
                    Hits = false;
                }
                else if(Input.GetButtonDown("Player01Joystick03") && specialMoveToggles[6].isEnabled)
                {
                    ActionQCB("Slash");
                    Hits = false;
                }
                else if(Input.GetButtonDown("Player01Joystick04") && specialMoveToggles[7].isEnabled)
                {
                    ActionQCB("HeavySlash");
                    Hits = false;
                }
            }
            else if (Time.time - lastInputTime > inputBufferTime)
            {
                inputState = InputState.None;
                isQCInProgress = false;
            }
        }
        else
        {
            if(Input.GetAxis(verticalInput) < -0.4f && !holdbuttonVertical && !isQCInProgress)
            {
                inputState = InputState.Down;
                lastInputTime = Time.time;
                isQCInProgress = true;
            }
            else if(inputState == InputState.Down && Time.time - lastInputTime <= inputBufferTime && isQCInProgress)
            {
                if(player01Movement.faceRight)
                {
                    if(Input.GetAxis(horizontalInput) < -0.4f)
                    {
                        inputState = InputState.Backward;
                        lastInputTime = Time.time;
                    }
                }
                else
                {
                    if(Input.GetAxis(horizontalInput) > 0.4f)
                    {
                        inputState = InputState.Backward;
                        lastInputTime = Time.time;
                    }
                }
            }
            else if (inputState == InputState.Backward && Time.time - lastInputTime <= inputBufferTime && isQCInProgress)
            {
                if(Input.GetButtonDown("Player01Bt01") && specialMoveToggles[4].isEnabled)
                {
                    ActionQCB("Punch");
                    Hits = false;
                }
                else if(Input.GetButtonDown("Player01Bt02") && specialMoveToggles[5].isEnabled)
                {
                    ActionQCB("Kick");
                    Hits = false;
                }
                else if(Input.GetButtonDown("Player01Bt03") && specialMoveToggles[6].isEnabled)
                {
                    ActionQCB("Slash");
                    Hits = false;
                }
                else if(Input.GetButtonDown("Player01Bt04") && specialMoveToggles[7].isEnabled)
                {
                    ActionQCB("HeavySlash");
                    Hits = false;
                }
            }
            else if (Time.time - lastInputTime > inputBufferTime)
            {
                inputState = InputState.None;
                isQCInProgress = false;
            }
        }

    }

    private void HandleHCB()
    {
        if(isQCInProgress)
        {
            return;
        }

        string verticalInput = selectController.Selectjoystick01 ? "LeftAnalogY1" : "Vertical";
        string horizontalInput = selectController.Selectjoystick01 ? "LeftAnalogX1" : "Horizontal";
        bool Joystick = selectController.Selectjoystick01 ? true : false;


        if(inputState == InputState.None || inputState == InputState.ForwardAgain && !isHCBInProgress)
        {
            if(player01Movement.faceRight)
            {
                if(Input.GetAxis(horizontalInput) > 0.4f && !holdbuttonHorizontal)
                {
                    inputState = InputState.Forward;
                    lastInputTime = Time.time;
                    isHCBInProgress = true;
                    inputState++;
                    return;
                }
            }
            else
            {
                if(Input.GetAxis(horizontalInput) < -0.4f && !holdbuttonHorizontal)
                {
                    inputState = InputState.Forward;
                    lastInputTime = Time.time;
                    isHCBInProgress = true;
                    inputState++;
                    return;
                }
            }
        }
        if(inputState == InputState.Forward && Time.time - lastInputTime <= inputBufferTime && isHCBInProgress)
        {
            if(Joystick)
            {
                if(-Input.GetAxis(verticalInput) < -0.4f && !holdbuttonVertical)
                {
                    inputState = InputState.Down;
                    lastInputTime = Time.time;
                    isHCBInProgress = true;
                    inputState++;
                    return;
                }
            }
            else
            {
                if(Input.GetAxis(verticalInput) < -0.4f && !holdbuttonVertical)
                {
                    inputState = InputState.Down;
                    lastInputTime = Time.time;
                    isHCBInProgress = true;
                    inputState++;
                    return;
                }
            }
        }
        if(inputState == InputState.Down && Time.time - lastInputTime <= inputBufferTime && isHCBInProgress)
        {
            if(player01Movement.faceRight)
            {
                if(Input.GetAxis(horizontalInput) < -0.4f)
                {
                    inputState = InputState.Backward;
                    lastInputTime = Time.time;
                    isHCBInProgress = true;
                    inputState++;
                    return;
                }
            }
            else
            {
                if(Input.GetAxis(horizontalInput) > 0.4f)
                {
                    inputState = InputState.Backward;
                    lastInputTime = Time.time;
                    isHCBInProgress = true;
                    inputState++;
                }
            }
        }
        if(inputState == InputState.Backward && Time.time - lastInputTime <= inputBufferTime && isHCBInProgress)
        {
            if(Joystick)
            {
                if(Input.GetButtonDown("Player01Joystick01") && specialMoveToggles[8].isEnabled)
                {
                    ActionHCB("Punch");
                    Hits = false;
                    return;
                }
                else if(Input.GetButtonDown("Player01Joystick02") && specialMoveToggles[9].isEnabled)
                {
                    ActionHCB("Kick");
                    Hits = false;
                    return;
                }
                else if(Input.GetButtonDown("Player01Joystick03") && specialMoveToggles[10].isEnabled)
                {
                    ActionHCB("Slash");
                    Hits = false;
                    return;
                }
                else if(Input.GetButtonDown("Player01Joystick04") && specialMoveToggles[11].isEnabled)
                {
                    ActionHCB("HeavySlash");
                    Hits = false;
                    return;
                }
            }
            else
            {
                if(Input.GetButtonDown("Player01Bt01") && specialMoveToggles[8].isEnabled)
                {
                    ActionHCB("Punch");
                    Hits = false;
                    return;
                }
                else if(Input.GetButtonDown("Player01Bt02") && specialMoveToggles[9].isEnabled)
                {
                    ActionHCB("Kick");
                    Hits = false;
                    return;
                }
                else if(Input.GetButtonDown("Player01Bt03") && specialMoveToggles[10].isEnabled)
                {
                    ActionHCB("Slash");
                    Hits = false;
                    return;
                }
                else if(Input.GetButtonDown("Player01Bt04") && specialMoveToggles[11].isEnabled)
                {
                    ActionHCB("HeavySlash");
                    Hits = false;
                    return;
                }
                }
            }
        if (Time.time - lastInputTime > inputBufferTime)
        {
            inputState = InputState.None;
            isHCBInProgress = false;
        }  
    }

    private void HandleHCBF()
    {
        if (isQCInProgress || specialMoveEnergy < 50) return;

        string verticalInput = selectController.Selectjoystick01 ? "LeftAnalogY1" : "Vertical";
        string horizontalInput = selectController.Selectjoystick01 ? "LeftAnalogX1" : "Horizontal";
        bool Joystick = selectController.Selectjoystick01 ? true : false;

        if (inputState == InputState.None && !isHCBInProgress)
        {
            if (player01Movement.faceRight)
            {
                if (Input.GetAxis(horizontalInput) > 0.4f && !holdbuttonHorizontal)
                {
                    inputState = InputState.Forward;
                    lastInputTime = Time.time;
                    isHCBInProgress = true;
                    inputCount++; // เพิ่มจำนวนการกด direction
                }
            }
            else
            {
                if (Input.GetAxis(horizontalInput) < -0.4f && !holdbuttonHorizontal)
                {
                    inputState = InputState.Forward;
                    lastInputTime = Time.time;
                    isHCBInProgress = true;
                    inputCount++;
                }
            }
        }
        else if (inputState == InputState.Forward && Time.time - lastInputTime <= inputBufferTime && isHCBInProgress)
        {
            if (Joystick)
            {
                if (-Input.GetAxis(verticalInput) < -0.4f && !holdbuttonVertical)
                {
                    inputState = InputState.Down;
                    lastInputTime = Time.time;
                    inputCount++;
                }
            }
            else
            {
                if (Input.GetAxis(verticalInput) < -0.4f && !holdbuttonVertical)
                {
                    inputState = InputState.Down;
                    lastInputTime = Time.time;
                    inputCount++;
                }
            }
        }
        else if (inputState == InputState.Down && Time.time - lastInputTime <= inputBufferTime && isHCBInProgress)
        {
            if (player01Movement.faceRight)
            {
                if (Input.GetAxis(horizontalInput) < -0.4f)
                {
                    inputState = InputState.Backward;
                    lastInputTime = Time.time;
                    inputCount++;
                }
            }
            else
            {
                if (Input.GetAxis(horizontalInput) > 0.4f)
                {
                    inputState = InputState.Backward;
                    lastInputTime = Time.time;
                    inputCount++;
                }
            }
        }
        else if (inputState == InputState.Backward && Time.time - lastInputTime <= inputBufferTime && isHCBInProgress)
        {
            if (player01Movement.faceRight)
            {
                if (Input.GetAxis(horizontalInput) > 0.4f && !holdbuttonHorizontal)
                {
                    inputState = InputState.Forward;
                    lastInputTime = Time.time;
                    inputCount++;
                }
            }
            else
            {
                if (Input.GetAxis(horizontalInput) < -0.4f && !holdbuttonHorizontal)
                {
                    inputState = InputState.Forward;
                    lastInputTime = Time.time;
                    inputCount++;
                }
            }
        }

        // ตรวจสอบเงื่อนไขสำหรับ HCBF
        if (inputCount >= 3 && Time.time - lastInputTime <= inputBufferTime && isHCBInProgress)
        {
            if (Joystick)
            {
                if (Input.GetButtonDown("Player01Joystick01") && specialMoveToggles[12].isEnabled)
                {
                    ActionHCBF("Punch");
                    ResetHCBF();
                }
                else if (Input.GetButtonDown("Player01Joystick02") && specialMoveToggles[13].isEnabled)
                {
                    ActionHCBF("Kick");
                    ResetHCBF();
                }
                else if (Input.GetButtonDown("Player01Joystick03") && specialMoveToggles[14].isEnabled)
                {
                    ActionHCBF("Slash");
                    ResetHCBF();
                }
                else if (Input.GetButtonDown("Player01Joystick04") && specialMoveToggles[15].isEnabled)
                {
                    ActionHCBF("HeavySlash");
                    ResetHCBF();
                }
            }
            else
            {
                if (Input.GetButtonDown("Player01Bt01") && specialMoveToggles[12].isEnabled)
                {
                    ActionHCBF("Punch");
                    ResetHCBF();
                }
                else if (Input.GetButtonDown("Player01Bt02") && specialMoveToggles[13].isEnabled)
                {
                    ActionHCBF("Kick");
                    ResetHCBF();
                }
                else if (Input.GetButtonDown("Player01Bt03") && specialMoveToggles[14].isEnabled)
                {
                    ActionHCBF("Slash");
                    ResetHCBF();
                }
                else if (Input.GetButtonDown("Player01Bt04") && specialMoveToggles[15].isEnabled)
                {
                    ActionHCBF("HeavySlash");
                    ResetHCBF();
                }
            }
        }

        // รีเซ็ตสถานะหากหมดเวลา
        if (Time.time - lastInputTime > inputBufferTime)
        {
            ResetHCBF();
        }
    }

    private void ResetHCBF()
    {
        inputState = InputState.None;
        isHCBInProgress = false;
        inputCount = 0; // รีเซ็ตตัวแปรจำนวนการกด
    }

    private void PerformAction(string actionName)
    {   
        isPerformingAction = true;
        player01Movement.isPerformingAction = true;

        string verticalInput = selectController.Selectjoystick01 ? "LeftAnalogY1" : "Vertical";
        string horizontalInput = selectController.Selectjoystick01 ? "LeftAnalogX1" : "Horizontal";
        bool Joystick = selectController.Selectjoystick01 ? true : false;
        
        if(Joystick)
        {
            if (-Input.GetAxis(verticalInput) < -0.4f)
            {
                anim.SetTrigger("Crouch" + actionName + "Trigger");
            }
            else if (player01Movement.faceRight)
            {
                if (Input.GetAxis(horizontalInput) > 0.4f)
                {
                    anim.SetTrigger("Special" + actionName + "Trigger");
                }
                else
                {
                    anim.SetTrigger(actionName + "Trigger");
                }
            }
            else if (player01Movement.faceLeft)
            {
                if (Input.GetAxis(horizontalInput) < 0.4f)
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
            if (Input.GetAxis(verticalInput) < -0.4f)
            {
                anim.SetTrigger("Crouch" + actionName + "Trigger");
            }
            else if (player01Movement.faceRight)
            {
                if (Input.GetAxis(horizontalInput) > 0.4f)
                {
                    anim.SetTrigger("Special" + actionName + "Trigger");
                }
                else
                {
                    anim.SetTrigger(actionName + "Trigger");
                }
            }
            else if (player01Movement.faceLeft)
            {
                if (Input.GetAxis(horizontalInput) < 0f)
                {
                    anim.SetTrigger("Special" + actionName + "Trigger");             
                }
                else
                {
                    anim.SetTrigger(actionName + "Trigger");
                }
            }
        }

        StartCoroutine(ResetIsPerformingAction(0.5f));
    }

    private void ActionQCF(string actionName)
    {
        anim.SetTrigger("QCF_" + actionName);
        isPerformingAction = true;
        player01Movement.isPerformingAction = true;
        StartCoroutine(ResetQCState());
    }

    private void ActionQCB(string actionName)
    {
        anim.SetTrigger("QCB_" + actionName);
        isPerformingAction = true;
        player01Movement.isPerformingAction = true;
        StartCoroutine(ResetQCState());
    }
    private void ActionHCB(string actionName)
    {
        anim.SetTrigger("HCB_" + actionName);
        isPerformingAction = true;
        player01Movement.isPerformingAction = true;
        StartCoroutine(ResetHCBFState());
    }
    private void ActionHCBF(string actionName)
    {
        specialMoveEnergy -= 50;
        player01CameraSpecial.CameraSetActive();
        isPerformingAction = true;
        player01Movement.isPerformingAction = true;
        anim.SetTrigger("HCBF_"+ actionName);
        if(nameCharacter == "Shark")
        {
            player01Health.SharkDrive = true;
            StartCoroutine(ResetBoolSharkdrive());
        }
        StartCoroutine(ResetHCBFState());
    }
    public void OnHits()
    {
        StopCoroutine(ResetIsPerformingAction(0));
        isPerformingAction = false;
        player01Movement.isPerformingAction = true;
    }
    public void GrapHCBShark()
    {
        anim.SetTrigger("Grap_HCB");
        isPerformingAction = true;
        player01Movement.isPerformingAction = true;
        StartCoroutine(ResetGrap());
    }
    public void GrapHCBFShark()
    {
        anim.SetTrigger("Grap_HCBF");
        isPerformingAction = true;
        player01Movement.isPerformingAction = true;
        StartCoroutine(ResetGrap());
    }
    IEnumerator ResetIsPerformingAction(float delay)
    {
        yield return new WaitForSeconds(delay);
        isPerformingAction = false;
        player01Movement.isPerformingAction = false;
    }

    IEnumerator ResetQCState()
    {
        yield return new WaitForSeconds(actionCooldown);
        inputState = InputState.None;
        isQCInProgress = false;
        isPerformingAction = false;
        player01Movement.isPerformingAction = false;
    }

    IEnumerator ResetHCBFState()
    {
        yield return new WaitForSeconds(1f);
        inputState = InputState.None;
        isHCBInProgress = false;
        isPerformingAction = false;
        player01Movement.isPerformingAction = false;
    }
    IEnumerator ResetGrap()
    {
        yield return new WaitForSeconds(2f);
        isPerformingAction = false;
        player01Movement.isPerformingAction = false;
    }
    IEnumerator ResetBoolSharkdrive()
    {
        yield return new WaitForSeconds(5f);
        player01Health.SharkDrive = false;
    }
}
    
public enum SpecialMove
{
    QCF_Punch,
    QCF_Kick,
    QCF_Slash,
    QCF_HeavySlash,
    QCB_Punch,
    QCB_Kick,
    QCB_Slash,
    QCB_HeavySlash,
    HCB_Punch,
    HCB_Kick,
    HCB_Slash,
    HCB_HeavySlash,
    HCBF_Punch,
    HCBF_Kick,
    HCBF_Slash,
    HCBF_HeavySlash,
}

[System.Serializable]
public class SpecialMoveToggle
{
    public SpecialMove moveName; // ชื่อท่าในรูปแบบ enum
    public bool isEnabled; // เปิดหรือปิดท่านั้น
}