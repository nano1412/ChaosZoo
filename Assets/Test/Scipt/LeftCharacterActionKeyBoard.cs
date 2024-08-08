using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftCharacterActionKeyBoard : MonoBehaviour
{
    public LeftLookEnemy leftLookEnemy;
    public Animator animator;
    public bool facingRight = true;
    public int specialMoveEnergy = 100;

    public bool isPerformingAction = false;
    private float actionCooldown = 0.1f;
    public bool isQCInProgress = false;
    public bool isHCBFInProgress = false;

    private enum InputState { None, Down, Forward, Backward, ForwardAgain }
    private InputState inputState = InputState.None;
    private float lastInputTime;
    private float inputBufferTime = 0.2f;

    void Start()
    {
        inputState = InputState.None;
    }
    void Update()
    {

        HandleQCF();
        HandleQCB();
        HandleHCBF();

        if (isPerformingAction || isQCInProgress || isHCBFInProgress)
        {
            return;
        }


        // ตรวจสอบ QCF และ HCBF ก่อนการตรวจสอบปุ่มพื้นฐาน
        // ตรวจสอบการกดปุ่ม U, I, O, P
        if (Input.GetKeyDown(KeyCode.U))
        {
            PerformAction("Punch", KeyCode.U);
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            PerformAction("Kick", KeyCode.I);
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            PerformAction("Slash", KeyCode.O);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            PerformAction("Heavily Slash", KeyCode.P);
        }
    }

    private void HandleQCF()
    {
        if(isPerformingAction || isHCBFInProgress)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            inputState = InputState.Down;
            lastInputTime = Time.time;
            isQCInProgress = true;
        }
        else if (inputState == InputState.Down && Time.time - lastInputTime <= inputBufferTime)
        {
            if (Input.GetKeyDown(facingRight ? KeyCode.D : KeyCode.A))
            {
                inputState = InputState.Forward;
                lastInputTime = Time.time;
            }
        }
        else if (inputState == InputState.Forward && Time.time - lastInputTime <= inputBufferTime)
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                Debug.Log("QCF " + (facingRight ? "Right" : "Left") + " Punch Special Action");
                StartCoroutine(ResetQCState());
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                Debug.Log("QCF " + (facingRight ? "Right" : "Left") + " Kick Special Action");
                StartCoroutine(ResetQCState());
            }
            else if (Input.GetKeyDown(KeyCode.O))
            {
                Debug.Log("QCF " + (facingRight ? "Right" : "Left") + " Slash Special Action");
                StartCoroutine(ResetQCState());
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("QCF " + (facingRight ? "Right" : "Left") + " Heavily Slash Special Action");
                StartCoroutine(ResetQCState());
            }
        }
        else if (Time.time - lastInputTime > inputBufferTime)
        {
            inputState = InputState.None;
            isQCInProgress = false;
        }
    }

    private void HandleQCB()
    {
        if(isPerformingAction || isHCBFInProgress)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            inputState = InputState.Down;
            lastInputTime = Time.time;
            isQCInProgress = true;
        }
        else if (inputState == InputState.Down && Time.time - lastInputTime <= inputBufferTime)
        {
            if (Input.GetKeyDown(facingRight ? KeyCode.A : KeyCode.D))
            {
                inputState = InputState.Backward;
                lastInputTime = Time.time;
            }
        }
        else if (inputState == InputState.Backward && Time.time - lastInputTime <= inputBufferTime)
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                Debug.Log("QCB " + (facingRight ? "Right" : "Left") + " Punch Special Action");
                StartCoroutine(ResetQCState());
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                Debug.Log("QCB " + (facingRight ? "Right" : "Left") + " Kick Special Action");
                StartCoroutine(ResetQCState());
            }
            else if (Input.GetKeyDown(KeyCode.O))
            {
                Debug.Log("QCB " + (facingRight ? "Right" : "Left") + " Slash Special Action");
                StartCoroutine(ResetQCState());
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("QCB " + (facingRight ? "Right" : "Left") + " Heavily Slash Special Action");
                StartCoroutine(ResetQCState());
            }
        }
        else if (Time.time - lastInputTime > inputBufferTime)
        {
            inputState = InputState.None;
            isQCInProgress = false;
        }
    }

    private void HandleHCBF()
    {
        if(isQCInProgress)
        {
            return;
        }
        if (specialMoveEnergy != 100)
        {
            return;
        }

        if (inputState == InputState.None || inputState == InputState.ForwardAgain)
        {
            if (Input.GetKeyDown(facingRight ? KeyCode.D : KeyCode.A))
            {
                inputState = InputState.Forward;
                lastInputTime = Time.time;
                isHCBFInProgress = true;
                return;
            }
        }

        if (inputState == InputState.Forward && Time.time - lastInputTime <= inputBufferTime)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                inputState = InputState.Down;
                lastInputTime = Time.time;
                return;
            }
        }

        if (inputState == InputState.Down && Time.time - lastInputTime <= inputBufferTime)
        {
            if (Input.GetKeyDown(facingRight ? KeyCode.A : KeyCode.D))
            {
                inputState = InputState.Backward;
                lastInputTime = Time.time;
                return;
            }
        }

        if (inputState == InputState.Backward && Time.time - lastInputTime <= inputBufferTime)
        {
            if (Input.GetKeyDown(facingRight ? KeyCode.D : KeyCode.A))
            {
                inputState = InputState.ForwardAgain;
                lastInputTime = Time.time;
                return;
            }
        }

        if (inputState == InputState.ForwardAgain && Time.time - lastInputTime <= inputBufferTime)
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                Debug.Log("HCBF " + (facingRight ? "Right" : "Left") + " Punch Special Action");
                specialMoveEnergy = 0;
                StartCoroutine(ResetHCBFState());
                return;
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                Debug.Log("HCBF " + (facingRight ? "Right" : "Left") + " Kick Special Action");
                specialMoveEnergy = 0;
                StartCoroutine(ResetHCBFState());
                return;
            }
            else if (Input.GetKeyDown(KeyCode.O))
            {
                Debug.Log("HCBF " + (facingRight ? "Right" : "Left") + " Slash Special Action");
                specialMoveEnergy = 0;
                StartCoroutine(ResetHCBFState());
                return;
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("HCBF " + (facingRight ? "Right" : "Left") + " Heavily Slash Special Action");
                specialMoveEnergy = 0;
                StartCoroutine(ResetHCBFState());
                return;
            }
        }

        if (Time.time - lastInputTime > inputBufferTime)
        {
            inputState = InputState.None;
            isHCBFInProgress = false;
        }
    }

    private void PerformAction(string actionName, KeyCode actionKey)
    {
        isPerformingAction = true;

        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("Crouch" + actionName);
            animator.SetTrigger("Crouch" + actionName + "Trigger");
            ResetCrouchAnimation(actionName);
        }
        else if (!IsGrounded())
        {
            Debug.Log("Airborne" + actionName);
        }
        else if (facingRight)
        {
            if (Input.GetKey(KeyCode.D))
            {
                Debug.Log("Special" + actionName);
                animator.SetTrigger("Special" + actionName + "Trigger");
                ResetSpecialAnimation(actionName);
            }
            else
            {
                Debug.Log(actionName);
                animator.SetTrigger(actionName + "Trigger");
                ResetActionAnimation(actionName);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.A))
            {
                Debug.Log("Special" + actionName);
                animator.SetTrigger("Special" + actionName + "Trigger");
                ResetSpecialAnimation(actionName);
            }
            else
            {
                Debug.Log(actionName);
                animator.SetTrigger(actionName + "Trigger");
                ResetActionAnimation(actionName);
            }
        }

        StartCoroutine(ResetActionFlag());
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    private IEnumerator ResetActionFlag()
    {
        yield return new WaitForSeconds(actionCooldown);
        isPerformingAction = false;
    }

    private IEnumerator ResetQCState()
    {
        yield return new WaitForSeconds(actionCooldown);
        inputState = InputState.None;
        isQCInProgress = false;
    }

    private IEnumerator ResetHCBFState()
    {
        yield return new WaitForSeconds(actionCooldown);
        inputState = InputState.None;
        isHCBFInProgress = false;
    }

    private IEnumerator ResetCrouchAnimation(string actionName)
    {
        yield return new WaitForSeconds(actionCooldown);
        animator.ResetTrigger("Crouch" + actionName + "Trigger");
    }

    private IEnumerator ResetActionAnimation(string actionName)
    {
        yield return new WaitForSeconds(actionCooldown);
        animator.ResetTrigger(actionName + "Trigger");
    }

    private IEnumerator ResetSpecialAnimation(string actionName)
    {
        yield return new WaitForSeconds(actionCooldown);
        animator.SetTrigger("Special" + actionName + "Trigger");
    }
}
