using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player01TakeAction : MonoBehaviour
{
    public float defaultActionCooldown = 0.5f; // เวลา default
    public GameObject player01;
    public Player01Movement player01Movement;
    public SelectController selectController;
    public bool isPerformingAction = false;
    public static bool Hits = false;
    public bool hits => Hits;

    private Animator anim;

    public enum InputState {None, Down, Forward, Backward, ForwardAgain}
    public InputState inputState = InputState.None;
    public bool isQCInProgress = false;
    public float actionCooldown = 0.1f;
    public float lastInputTime;
    public float inputBufferTime = 0.2f;

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
        HandleQCF();
        
        if (isPerformingAction || isQCInProgress) return;

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
        if(isPerformingAction) return;
        
        string verticalInput = selectController.Selectjoystick01 ? "LeftAnalogY1" : "Vertical";
        string horizontalInput = selectController.Selectjoystick01 ? "LeftAnalogX1" : "Horizontal";
        bool Joystick = selectController.Selectjoystick01 ? true : false;

        if(Joystick)
        {
            if(-Input.GetAxis(verticalInput) < -0.4f)
            {
                inputState = InputState.Down;
                lastInputTime = Time.time;
                isQCInProgress = true;
            }
            else if(inputState == InputState.Down && Time.time - lastInputTime <= inputBufferTime)
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
                    if(Input.GetAxis(horizontalInput) < 0.4f)
                    {
                        inputState = InputState.Forward;
                        lastInputTime = Time.time;
                    }
                }
            }
            else if(inputState == InputState.Forward && Time.time - lastInputTime <= inputBufferTime)
            {
                if(Input.GetButtonDown("Player01Joystick01") && specialMoveToggles[0].isEnabled)
                {
                    Debug.Log("QCF " + (player01Movement.faceRight ? "Right" : "Left") + " " + "Punch" + "Special Action");
                    StartCoroutine(ResetQCState());
                }
                else if(Input.GetButtonDown("Player01Joystick02") && specialMoveToggles[1].isEnabled)
                {
                    Debug.Log("QCF " + (player01Movement.faceRight ? "Right" : "Left") + " " + "Kick" + "Special Action");
                    StartCoroutine(ResetQCState());
                }
                else if(Input.GetButtonDown("Player01Joystick03") && specialMoveToggles[2].isEnabled)
                {
                    Debug.Log("QCF " + (player01Movement.faceRight ? "Right" : "Left") + " " + "Slash" + "Special Action");
                    StartCoroutine(ResetQCState());
                }
                else if(Input.GetButtonDown("Player01Joystick04") && specialMoveToggles[3].isEnabled)
                {
                    Debug.Log("QCF " + (player01Movement.faceRight ? "Right" : "Left") + " " + "HeavySlash" + "Special Action");
                    StartCoroutine(ResetQCState());
                }
            }
            else if (Time.time - lastInputTime > inputBufferTime)
            {
                inputState = InputState.None;
                isQCInProgress = false;
            }
        }
        else if(!Joystick)
        {
            if(Input.GetAxis(verticalInput) < -0.4f)
            {
                inputState = InputState.Down;
                lastInputTime = Time.time;
                isQCInProgress = true;
            }
            else if(inputState == InputState.Down && Time.time - lastInputTime <= inputBufferTime)
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
                    if(Input.GetAxis(horizontalInput) < 0.4f)
                    {
                        inputState = InputState.Forward;
                        lastInputTime = Time.time;
                    }
                }
            }
            else if(inputState == InputState.Forward && Time.time - lastInputTime <= inputBufferTime)
            {
                if(Input.GetButtonDown("Player01Bt01") && specialMoveToggles[0].isEnabled)
                {
                    Debug.Log("QCF " + (player01Movement.faceRight ? "Right" : "Left") + " " + "Punch" + "Special Action");
                    StartCoroutine(ResetQCState());
                }
                else if(Input.GetButtonDown("Player01Bt02") && specialMoveToggles[1].isEnabled)
                {
                    Debug.Log("QCF " + (player01Movement.faceRight ? "Right" : "Left") + " " + "Kick" + "Special Action");
                    StartCoroutine(ResetQCState());
                }
                else if(Input.GetButtonDown("Player01Bt03") && specialMoveToggles[2].isEnabled)
                {
                    Debug.Log("QCF " + (player01Movement.faceRight ? "Right" : "Left") + " " + "Slash" + "Special Action");
                    StartCoroutine(ResetQCState());
                }
                else if(Input.GetButtonDown("Player01Bt04") && specialMoveToggles[3].isEnabled)
                {
                    Debug.Log("QCF " + (player01Movement.faceRight ? "Right" : "Left") + " " + "HeavySlash" + "Special Action");
                    StartCoroutine(ResetQCState());
                }
            }
            else if (Time.time - lastInputTime > inputBufferTime)
            {
                inputState = InputState.None;
                isQCInProgress = false;
            }
        }
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
                Debug.Log($"Crouch {actionName}");
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

    public void OnHits()
    {
        StopCoroutine(ResetIsPerformingAction(0));
        isPerformingAction = false;
        player01Movement.isPerformingAction = true;
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
    HCBF_Punch,
    HCBF_Kick,
    HCBF_Slash,
    HCBF_HeavySlash
}

[System.Serializable]
public class SpecialMoveToggle
{
    public SpecialMove moveName; // ชื่อท่าในรูปแบบ enum
    public bool isEnabled; // เปิดหรือปิดท่านั้น
}