using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftCharacterActionKeyBoard : MonoBehaviour
{
    public LeftLookEnemy leftLookEnemy;
    public LeftCharacterMovementKeyBoard leftCharacterMovementKeyBoard;
    public Animator animator;
    public bool facingRight = true;
    public int specialMoveEnergy = 100;

    public bool isPerformingAction = false;
    private float actionCooldown = 1f;
    public bool isQCInProgress = false;
    public bool isHCBFInProgress = false;

    public List<SpecialMove> qcfMoves = new List<SpecialMove>();
    public List<SpecialMove> qcbMoves = new List<SpecialMove>();
    public List<SpecialMove> hcbfMoves = new List<SpecialMove>();

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
        if (isPerformingAction || isHCBFInProgress)
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
            if (Input.GetKeyDown(KeyCode.U) && qcfMoves.Contains(SpecialMove.QCF_Punch))
            {
                Debug.Log("QCF " + (facingRight ? "Right" : "Left") + " Punch Special Action");
                StartCoroutine(ResetQCState());
            }
            else if (Input.GetKeyDown(KeyCode.I) && qcfMoves.Contains(SpecialMove.QCF_Kick))
            {
                Debug.Log("QCF " + (facingRight ? "Right" : "Left") + " Kick Special Action");
                StartCoroutine(ResetQCState());
            }
            else if (Input.GetKeyDown(KeyCode.O) && qcfMoves.Contains(SpecialMove.QCF_Slash))
            {
                Debug.Log("QCF " + (facingRight ? "Right" : "Left") + " Slash Special Action");
                StartCoroutine(ResetQCState());
            }
            else if (Input.GetKeyDown(KeyCode.P) && qcfMoves.Contains(SpecialMove.QCF_HeavySlash))
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
        if (isPerformingAction || isHCBFInProgress)
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
            if (Input.GetKeyDown(KeyCode.U) && qcbMoves.Contains(SpecialMove.QCB_Punch))
            {
                Debug.Log("QCB " + (facingRight ? "Right" : "Left") + " Punch Special Action");
                StartCoroutine(ResetQCState());
            }
            else if (Input.GetKeyDown(KeyCode.I) && qcbMoves.Contains(SpecialMove.QCB_Kick))
            {
                Debug.Log("QCB " + (facingRight ? "Right" : "Left") + " Kick Special Action");
                StartCoroutine(ResetQCState());
            }
            else if (Input.GetKeyDown(KeyCode.O) && qcbMoves.Contains(SpecialMove.QCB_Slash))
            {
                Debug.Log("QCB " + (facingRight ? "Right" : "Left") + " Slash Special Action");
                StartCoroutine(ResetQCState());
            }
            else if (Input.GetKeyDown(KeyCode.P) && qcbMoves.Contains(SpecialMove.QCB_HeavySlash))
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
        if (isQCInProgress)
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
            if (Input.GetKeyDown(KeyCode.U) && hcbfMoves.Contains(SpecialMove.HCBF_Punch))
            {
                Debug.Log("HCBF " + (facingRight ? "Right" : "Left") + " Punch Special Action");
                specialMoveEnergy = 0;
                StartCoroutine(ResetHCBFState("Punch"));
                return;
            }
            else if (Input.GetKeyDown(KeyCode.I) && hcbfMoves.Contains(SpecialMove.HCBF_Kick))
            {
                Debug.Log("HCBF " + (facingRight ? "Right" : "Left") + " Kick Special Action");
                animator.SetTrigger("HCBF_" + "Kick" + "Trigger");
                animator.SetBool("HCBF",true);
                specialMoveEnergy = 0;
                StartCoroutine(ResetHCBFState("Kick"));
                return;
            }
            else if (Input.GetKeyDown(KeyCode.O) && hcbfMoves.Contains(SpecialMove.HCBF_Slash))
            {
                Debug.Log("HCBF " + (facingRight ? "Right" : "Left") + " Slash Special Action");
                specialMoveEnergy = 0;
                StartCoroutine(ResetHCBFState("Slash"));
                return;
            }
            else if (Input.GetKeyDown(KeyCode.P) && hcbfMoves.Contains(SpecialMove.HCBF_HeavySlash))
            {
                Debug.Log("HCBF " + (facingRight ? "Right" : "Left") + " Heavily Slash Special Action");
                specialMoveEnergy = 0;
                StartCoroutine(ResetHCBFState("HeavilySlash"));
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
        leftCharacterMovementKeyBoard.isPerformingAction = true;

        if (Input.GetKey(KeyCode.S)) 
        {
            Debug.Log("Crouch" + actionName);
            animator.SetTrigger("Crouch" + actionName + "Trigger");
            StartCoroutine(ResetCrouchAnimation(actionName));
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
                StartCoroutine(ResetSpecialAnimation(actionName));
            }
            else
            {
                Debug.Log(actionName);
                animator.SetTrigger(actionName + "Trigger");
                StartCoroutine(ResetActionAnimation(actionName));
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.A))
            {
                Debug.Log("Special" + actionName);
                animator.SetTrigger("Special" + actionName + "Trigger");
                StartCoroutine(ResetSpecialAnimation(actionName));
            }
            else
            {
                Debug.Log(actionName);
                animator.SetTrigger(actionName + "Trigger");
                StartCoroutine(ResetActionAnimation(actionName));
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

    private IEnumerator ResetHCBFState(string actionHCBF)
    {
        yield return new WaitForSeconds(actionCooldown);
        inputState = InputState.None;
        yield return new WaitForSeconds(2f);
        animator.ResetTrigger("HCBF_" + actionHCBF + "Trigger");
        animator.SetBool("HCBF", false);
        isHCBFInProgress = false;
    }

    private IEnumerator ResetCrouchAnimation(string actionName)
    {
        yield return new WaitForSeconds(actionCooldown);
        Debug.Log("Reset");
        animator.ResetTrigger("Crouch" + actionName + "Trigger");
        leftCharacterMovementKeyBoard.isPerformingAction = false;
    }

    private IEnumerator ResetActionAnimation(string actionName)
    {
        yield return new WaitForSeconds(actionCooldown);
        animator.ResetTrigger(actionName + "Trigger");
        leftCharacterMovementKeyBoard.isPerformingAction = false;
    }

    private IEnumerator ResetSpecialAnimation(string actionName)
    {
        yield return new WaitForSeconds(actionCooldown);
        animator.ResetTrigger("Special" + actionName + "Trigger");
        leftCharacterMovementKeyBoard.isPerformingAction = false;
    }
}
