using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player01TakeActionInChallenge : MonoBehaviour
{
    public float defaultActionCooldown = 0.5f; // เวลา default
    public string nameCharacter;
    public GameObject player01;
    public Player01MovementChallenge player01Movement;
    public Player01CameraInChallange player01CameraInChallange;
    public SelectControllerInChallenge selectControllerInChallenge;
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
    public ChalllengeScripttable challengeData;
    public string verticalInput;
    public string horizontalInput;
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
    }

    void Update()
    {
        if(challengeData.CurrentRound <= 9)
        {
            verticalInput = selectControllerInChallenge.Selectjoystick01 ? "LeftAnalogY2" : "Vertical";
            horizontalInput = selectControllerInChallenge.Selectjoystick01 ? "LeftAnalogX2" : "Horizontal";
        }
        else if(challengeData.CurrentRound >= 10)
        {
            verticalInput = selectControllerInChallenge.Selectjoystick01 ? "LeftAnalogY1" : "Vertical";
            horizontalInput = selectControllerInChallenge.Selectjoystick01 ? "LeftAnalogX1" : "Horizontal";
        }
        bool Joystick = selectControllerInChallenge.Selectjoystick01 ? true : false;
        

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

        if (selectControllerInChallenge.Selectjoystick01)
        {
            if (Input.GetButtonDown("Player02Joystick01"))
            {
                PerformAction("Punch");
                Hits = false;
            }
            if (Input.GetButtonDown("Player02Joystick02"))
            {
                PerformAction("Kick");
                Hits = false;
            }
            if (Input.GetButtonDown("Player02Joystick03"))
            {
                PerformAction("Slash");
                Hits = false;
            }
            if (Input.GetButtonDown("Player02Joystick04"))
            {
                PerformAction("HeavySlash");
                Hits = false;
            }
        }
        else
        {
            if (Input.GetButtonDown("Player02Bt01"))
            {
                PerformAction("Punch");
                Hits = false;
            }
            if (Input.GetButtonDown("Player02Bt02"))
            {
                PerformAction("Kick");
                Hits = false;
            }
            if (Input.GetButtonDown("Player02Bt03"))
            {
                PerformAction("Slash");
                Hits = false;
            }
            if (Input.GetButtonDown("Player02Bt04"))
            {
                PerformAction("HeavySlash");
                Hits = false;
            }
        }
    }
    private void HandleQCF()
    {
        if (isPerformingAction || isHCBInProgress) return;

        // string verticalInput = selectControllerInChallenge.Selectjoystick01 ? "LeftAnalogY2" : "Vertical";
        // string horizontalInput = selectControllerInChallenge.Selectjoystick01 ? "LeftAnalogX2" : "Horizontal";
        bool Joystick = selectControllerInChallenge.Selectjoystick01 ? true : false;

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
                if(Input.GetButtonDown("Player02Joystick01") && specialMoveToggles[0].isEnabled)
                {
                    ActionQCF("Punch");
                    Hits = false;
                }
                else if(Input.GetButtonDown("Player02Joystick02") && specialMoveToggles[1].isEnabled)
                {
                    ActionQCF("Kick");
                    Hits = false;
                }
                else if(Input.GetButtonDown("Player02Joystick03") && specialMoveToggles[2].isEnabled)
                {
                    ActionQCF("Slash");
                    Hits = false;
                }
                else if(Input.GetButtonDown("Player02Joystick04") && specialMoveToggles[3].isEnabled)
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
                if(Input.GetButtonDown("Player02Bt01") && specialMoveToggles[0].isEnabled)
                {
                    ActionQCF("Punch");
                    Hits = false;
                }
                else if(Input.GetButtonDown("Player02Bt02") && specialMoveToggles[1].isEnabled)
                {
                    ActionQCF("Kick");
                    Hits = false;
                }
                else if(Input.GetButtonDown("Player02Bt03") && specialMoveToggles[2].isEnabled)
                {
                    ActionQCF("Slash");
                    Hits = false;
                }
                else if(Input.GetButtonDown("Player02Bt04") && specialMoveToggles[3].isEnabled)
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
        
        // string verticalInput = selectControllerInChallenge.Selectjoystick01 ? "LeftAnalogY2" : "Vertical";
        // string horizontalInput = selectControllerInChallenge.Selectjoystick01 ? "LeftAnalogX2" : "Horizontal";
        bool Joystick = selectControllerInChallenge.Selectjoystick01 ? true : false;


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
                if(Input.GetButtonDown("Player02Joystick01") && specialMoveToggles[4].isEnabled)
                {
                    ActionQCB("Punch");
                    Hits = false;
                }
                else if(Input.GetButtonDown("Player02Joystick02") && specialMoveToggles[5].isEnabled)
                {
                    ActionQCB("Kick");
                    Hits = false;
                }
                else if(Input.GetButtonDown("Player02Joystick03") && specialMoveToggles[6].isEnabled)
                {
                    ActionQCB("Slash");
                    Hits = false;
                }
                else if(Input.GetButtonDown("Player02Joystick04") && specialMoveToggles[7].isEnabled)
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
                if(Input.GetButtonDown("Player02Bt01") && specialMoveToggles[4].isEnabled)
                {
                    ActionQCB("Punch");
                    Hits = false;
                }
                else if(Input.GetButtonDown("Player02Bt02") && specialMoveToggles[5].isEnabled)
                {
                    ActionQCB("Kick");
                    Hits = false;
                }
                else if(Input.GetButtonDown("Player02Bt03") && specialMoveToggles[6].isEnabled)
                {
                    ActionQCB("Slash");
                    Hits = false;
                }
                else if(Input.GetButtonDown("Player02Bt04") && specialMoveToggles[7].isEnabled)
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

        // string verticalInput = selectControllerInChallenge.Selectjoystick01 ? "LeftAnalogY2" : "Vertical";
        // string horizontalInput = selectControllerInChallenge.Selectjoystick01 ? "LeftAnalogX2" : "Horizontal";
        bool Joystick = selectControllerInChallenge.Selectjoystick01 ? true : false;


        if(inputState == InputState.None || inputState == InputState.ForwardAgain && !isHCBInProgress)
        {
            if(player01Movement.faceRight)
            {
                if(Input.GetAxis(horizontalInput) > 0.4f && !holdbuttonHorizontal)
                {
                    inputState = InputState.Forward;
                    lastInputTime = Time.time;
                    isHCBInProgress = true;
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
                }
            }
        }
        if(inputState == InputState.Backward && Time.time - lastInputTime <= inputBufferTime && isHCBInProgress)
        {
            if(Joystick)
            {
                if(Input.GetButtonDown("Player02Joystick01") && specialMoveToggles[8].isEnabled)
                {
                    ActionHCB("Punch");
                    Hits = false;
                    return;
                }
                else if(Input.GetButtonDown("Player02Joystick02") && specialMoveToggles[9].isEnabled)
                {
                    ActionHCB("Kick");
                    Hits = false;
                    return;
                }
                else if(Input.GetButtonDown("Player02Joystick03") && specialMoveToggles[10].isEnabled)
                {
                    ActionHCB("Slash");
                    Hits = false;
                    return;
                }
                else if(Input.GetButtonDown("Player02Joystick04") && specialMoveToggles[11].isEnabled)
                {
                    ActionHCB("HeavySlash");
                    Hits = false;
                    return;
                }
            }
            else
            {
                if(Input.GetButtonDown("Player02Bt01") && specialMoveToggles[8].isEnabled)
                {
                    ActionHCB("Punch");
                    Hits = false;
                    return;
                }
                else if(Input.GetButtonDown("Player02Bt02") && specialMoveToggles[9].isEnabled)
                {
                    ActionHCB("Kick");
                    Hits = false;
                    return;
                }
                else if(Input.GetButtonDown("Player02Bt03") && specialMoveToggles[10].isEnabled)
                {
                    ActionHCB("Slash");
                    Hits = false;
                    return;
                }
                else if(Input.GetButtonDown("Player02Bt04") && specialMoveToggles[11].isEnabled)
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
        if(isQCInProgress)
        {
            return;
        }
        if (specialMoveEnergy < 50)
        {
            return;
        }

        // string verticalInput = selectControllerInChallenge.Selectjoystick01 ? "LeftAnalogY2" : "Vertical";
        // string horizontalInput = selectControllerInChallenge.Selectjoystick01 ? "LeftAnalogX2" : "Horizontal";
        bool Joystick = selectControllerInChallenge.Selectjoystick01 ? true : false;

        if(inputState == InputState.None || inputState == InputState.ForwardAgain && !isHCBInProgress)
        {
            if(player01Movement.faceRight)
            {
                if(Input.GetAxis(horizontalInput) > 0.4f && !holdbuttonHorizontal)
                {
                    inputState = InputState.Forward;
                    lastInputTime = Time.time;
                    isHCBInProgress = true;
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
                }
            }
        }
        if(inputState == InputState.Backward && Time.time - lastInputTime <= inputBufferTime && isHCBInProgress)
        {
            if(player01Movement.faceRight)
            {
                if(Input.GetAxis(horizontalInput) > 0.4f && !holdbuttonHorizontal)
                {
                    inputState = InputState.ForwardAgain;
                    lastInputTime = Time.time;
                    isHCBInProgress = true;
                    return;
                }
            }
            else
            {
                if(Input.GetAxis(horizontalInput) < -0.4f && !holdbuttonHorizontal)
                {
                    inputState = InputState.ForwardAgain;
                    lastInputTime = Time.time;
                    isHCBInProgress = true;
                }
            }
        }
        if(inputState == InputState.ForwardAgain && Time.time - lastInputTime <= inputBufferTime && isHCBInProgress)
        {
            if(Joystick)
            {
                if(Input.GetButtonDown("Player02Joystick01") && specialMoveToggles[12].isEnabled)
                {
                    ActionHCBF("Punch");
                    Hits = false;
                    return;
                }
                else if(Input.GetButtonDown("Player02Joystick02") && specialMoveToggles[13].isEnabled)
                {
                    ActionHCBF("Kick");
                    Hits = false;
                    return;
                }
                else if(Input.GetButtonDown("Player02Joystick03") && specialMoveToggles[14].isEnabled)
                {
                    ActionHCBF("Slash");
                    Hits = false;
                    return;
                }
                else if(Input.GetButtonDown("Player02Joystick04") && specialMoveToggles[15].isEnabled)
                {
                    ActionHCBF("HeavySlash");
                    Hits = false;
                    return;
                }
            }
            else
            {
                if(Input.GetButtonDown("Player02Bt01") && specialMoveToggles[12].isEnabled)
                {
                    ActionHCBF("Punch");
                    Hits = false;
                    return;
                }
                else if(Input.GetButtonDown("Player02Bt02") && specialMoveToggles[13].isEnabled)
                {
                    ActionHCBF("Kick");
                    Hits = false;
                    return;
                }
                else if(Input.GetButtonDown("Player02Bt03") && specialMoveToggles[14].isEnabled)
                {
                    ActionHCBF("Slash");
                    Hits = false;
                    return;
                }
                else if(Input.GetButtonDown("Player02Bt04") && specialMoveToggles[15].isEnabled)
                {
                    ActionHCBF("HeavySlash");
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

    private void PerformAction(string actionName)
    {   
        isPerformingAction = true;
        player01Movement.isPerformingAction = true;

        // string verticalInput = selectControllerInChallenge.Selectjoystick01 ? "LeftAnalogY2" : "Vertical";
        // string horizontalInput = selectControllerInChallenge.Selectjoystick01 ? "LeftAnalogX2" : "Horizontal";
        bool Joystick = selectControllerInChallenge.Selectjoystick01 ? true : false;
        
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
        //specialMoveEnergy -= 50;
        isPerformingAction = true;
        player01Movement.isPerformingAction = true;
        anim.SetTrigger("HCBF_"+ actionName);
        if(nameCharacter == "Shark")
        {
            //player01Health.SharkDrive = true;
            player01CameraInChallange.CameraSetActive();
            StartCoroutine(ResetBoolSharkdrive());
            StartCoroutine(ResetHCBFState(1f));
        }
        if(nameCharacter == "Pengang")
        {
            player01CameraInChallange.SpecialPengang();
            NumberRPG = true;
            boxColliderPangeng.enabled = false;

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
        //player01Health.SharkDrive = false;
    }
    IEnumerator ResetBoxCollider(float time)
    {
        yield return new WaitForSeconds(time);
        boxColliderPangeng.enabled = true;
    }

    
}
