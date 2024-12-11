using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player01TakeActionInTrainingRoom : MonoBehaviour
{
    public float defaultActionCooldown = 0.5f;
    public string nameCharacter;
    public GameObject player01;
    public GameObject opponent;
    //public Player02Movement player02Movement;
    public Player01MovementInTrainingRoom player01Movement;
    public Player01CameraSpecial player01CameraSpecial;
    public Player01Health player01Health;
    public SelectController selectController;
    public BoxCollider boxColliderPangeng;
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
    public bool NumberRPG = false;

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
        opponent = GameObject.FindGameObjectWithTag("PlayerCharacter02Tpose");
        //player02Movement = GameObject.FindGameObjectWithTag("Player02").GetComponent<Player02Movement>();
    }

    void Update()
    {
        string verticalInput = selectController.Selectjoystick01 ? "LeftAnalogY" : "Vertical";
        string horizontalInput = selectController.Selectjoystick01 ? "LeftAnalogX" : "Horizontal";
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
        if(Joystick)
        {
            if(-Input.GetAxis(verticalInput) < -0.4f)
            {
                holdTimeVertical += Time.deltaTime;
                if(holdTimeVertical > 0.2f)
                {
                    isQCInProgress = false;
                    isHCBInProgress = false;
                    holdbuttonVertical = true;
                }
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
        else if (Input.GetAxis(verticalInput) <= 0.1 && Input.GetAxis(horizontalInput) <= 0.1)
        {
            holdbuttonVertical = false;
            holdbuttonHorizontal = false;
            holdTimeVertical = 0;
            holdTimeHorizontal = 0;
        }


        if (isPerformingAction || isQCInProgress || isHCBInProgress || player01Movement.isJump) return;

        if (selectController.Selectjoystick01)
        {
            if (Input.GetButtonDown("PlayerJoystick01"))
            {
                PerformAction("Punch");
                Hits = false;
            }
            if (Input.GetButtonDown("PlayerJoystick02"))
            {
                PerformAction("Kick");
                Hits = false;
            }
            if (Input.GetButtonDown("PlayerJoystick03"))
            {
                PerformAction("Slash");
                Hits = false;
            }
            if (Input.GetButtonDown("PlayerJoystick04"))
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

        string verticalInput = selectController.Selectjoystick01 ? "LeftAnalogY" : "Vertical";
        string horizontalInput = selectController.Selectjoystick01 ? "LeftAnalogX" : "Horizontal";
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
                if(Input.GetButtonDown("PlayerJoystick01") && specialMoveToggles[0].isEnabled)
                {
                    ActionQCF("Punch");
                    Hits = false;
                }
                else if(Input.GetButtonDown("PlayerJoystick02") && specialMoveToggles[1].isEnabled)
                {
                    ActionQCF("Kick");
                    Hits = false;
                }
                else if(Input.GetButtonDown("PlayerJoystick03") && specialMoveToggles[2].isEnabled)
                {
                    ActionQCF("Slash");
                    Hits = false;
                }
                else if(Input.GetButtonDown("PlayerJoystick04") && specialMoveToggles[3].isEnabled)
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
        
        string verticalInput = selectController.Selectjoystick01 ? "LeftAnalogY" : "Vertical";
        string horizontalInput = selectController.Selectjoystick01 ? "LeftAnalogX" : "Horizontal";
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
                if(Input.GetButtonDown("PlayerJoystick01") && specialMoveToggles[4].isEnabled)
                {
                    ActionQCB("Punch");
                    Hits = false;
                }
                else if(Input.GetButtonDown("PlayerJoystick02") && specialMoveToggles[5].isEnabled)
                {
                    ActionQCB("Kick");
                    Hits = false;
                }
                else if(Input.GetButtonDown("PlayerJoystick03") && specialMoveToggles[6].isEnabled)
                {
                    ActionQCB("Slash");
                    Hits = false;
                }
                else if(Input.GetButtonDown("PlayerJoystick04") && specialMoveToggles[7].isEnabled)
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

        string verticalInput = selectController.Selectjoystick01 ? "LeftAnalogY" : "Vertical";
        string horizontalInput = selectController.Selectjoystick01 ? "LeftAnalogX" : "Horizontal";
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
                    inputCount++;
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
                    inputCount++;
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
                    inputCount++;
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
                    inputCount++;
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
                    inputCount++;
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
                    inputCount++;
                }
            }
        }
        if(inputCount >= 2 && Time.time - lastInputTime <= inputBufferTime && isHCBInProgress)
        {
            if(Joystick)
            {
                if(Input.GetButtonDown("PlayerJoystick01") && specialMoveToggles[8].isEnabled)
                {
                    ActionHCB("Punch");
                    Hits = false;
                    return;
                }
                else if(Input.GetButtonDown("PlayerJoystick02") && specialMoveToggles[9].isEnabled)
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

        string verticalInput = selectController.Selectjoystick01 ? "LeftAnalogY" : "Vertical";
        string horizontalInput = selectController.Selectjoystick01 ? "LeftAnalogX" : "Horizontal";
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
                    inputCount++;
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
                if (Input.GetButtonDown("PlayerJoystick01") && specialMoveToggles[12].isEnabled)
                {
                    ActionHCBF("Punch");
                    ResetHCBF();
                }
                else if (Input.GetButtonDown("PlayerJoystick02") && specialMoveToggles[13].isEnabled)
                {
                    ActionHCBF("Kick");
                    ResetHCBF();
                }
                else if (Input.GetButtonDown("PlayerJoystick03") && specialMoveToggles[14].isEnabled)
                {
                    ActionHCBF("Slash");
                    ResetHCBF();
                }
                else if (Input.GetButtonDown("PlayerJoystick04") && specialMoveToggles[15].isEnabled)
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

        string verticalInput = selectController.Selectjoystick01 ? "LeftAnalogY" : "Vertical";
        string horizontalInput = selectController.Selectjoystick01 ? "LeftAnalogX" : "Horizontal";
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
        if(nameCharacter == "Pengang")
        {
            boxColliderPangeng.enabled = false;
            StartCoroutine(ResetBoxCollider(1f));
        }
        isPerformingAction = true;
        player01Movement.isPerformingAction = true;
        StartCoroutine(ResetQCState());
    }

    private void ActionQCB(string actionName)
    {
        anim.SetTrigger("QCB_" + actionName);
        if(nameCharacter == "Pengang")
        {
            boxColliderPangeng.enabled = false;
            StartCoroutine(ResetBoxCollider(1f));
        }
        isPerformingAction = true;
        player01Movement.isPerformingAction = true;
        StartCoroutine(ResetQCState());
    }
    private void ActionHCB(string actionName)
    {
        anim.SetTrigger("HCB_" + actionName);
        isPerformingAction = true;
        player01Movement.isPerformingAction = true;
        StartCoroutine(ResetHCBFState(1f));
    }
    private void ActionHCBF(string actionName)
    {
        specialMoveEnergy -= 50;
        isPerformingAction = true;
        player01Movement.isPerformingAction = true;
        anim.SetTrigger("HCBF_"+ actionName);
        if(nameCharacter == "Shark")
        {
            player01CameraSpecial.CameraSetActive();
            player01Health.SharkDrive = true;
            Animator opponentAnimator = opponent.GetComponent<Animator>();
            if (opponentAnimator != null)
            {
                opponentAnimator.enabled = false;
                //player02Movement.enabled = false;
            }

            StartCoroutine(ResetBoolSharkdrive());
            StartCoroutine(ResetHCBFState(1f));
        }
        if(nameCharacter == "Pengang")
        {
            player01CameraSpecial.SpecialPengang();
            NumberRPG = true;
            boxColliderPangeng.enabled = false;

            Animator opponentAnimator = opponent.GetComponent<Animator>();
            if (opponentAnimator != null)
            {
                opponentAnimator.enabled = false;
                //player02Movement.enabled = false;
            }
            StartCoroutine(ResetMovement(1.8f));
            StartCoroutine(ResetBoxCollider(3.2f));
            StartCoroutine(ResetHCBFState(5f));
        }
    }
    public void OnHits()
    {
        StopCoroutine(ResetIsPerformingAction(0));
        StartCoroutine(ResetCheckBool(0.25f));
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
    IEnumerator ResetCheckBool(float delay)
    {
        yield return new WaitForSeconds(delay);
        isPerformingAction = false;
        player01Movement.isPerformingAction = true;
    }

    IEnumerator ResetQCState()
    {
        yield return new WaitForSeconds(actionCooldown);
        inputState = InputState.None;
        isQCInProgress = false;
        isPerformingAction = false;
        player01Movement.isPerformingAction = false;
    }

    IEnumerator ResetHCBFState(float time)
    {
        yield return new WaitForSeconds(time);
        inputState = InputState.None;
        isHCBInProgress = false;
        isPerformingAction = false;
        player01Movement.isPerformingAction = false;
        NumberRPG = false;

        Animator opponentAnimator = opponent.GetComponent<Animator>();
        if (opponentAnimator != null)
        {
            opponentAnimator.enabled = true;
            //player02Movement.enabled = true;
        }
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
    IEnumerator ResetBoxCollider(float time)
    {
        yield return new WaitForSeconds(time);
        boxColliderPangeng.enabled = true;
    }

    IEnumerator ResetMovement(float time)
    {
        yield return new WaitForSeconds(time);
        Animator opponentAnimator = opponent.GetComponent<Animator>();
        if (opponentAnimator != null)
        {
            opponentAnimator.enabled = true;
            //player02Movement.enabled = true;
        }
    }
}
