using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftCharacterActionKeyBoard : MonoBehaviour
{
    public LeftLookEnemy leftLookEnemy;
    public bool facingRight = true;

    private bool isPerformingAction = false;
    private float actionCooldown = 0.1f;
    private bool isQCFInProgress = false;
    private bool isHCBFInProgress = false;

    private enum InputState { None, Down, Forward, Backward, ForwardAgain }
    private InputState inputState = InputState.None;
    private float lastInputTime;
    private float inputBufferTime = 0.2f;

    void Update()
    {
        HandleQCF();
        HandleHCBF();

        if (isPerformingAction || isQCFInProgress || isHCBFInProgress)
        {
            return;
        }

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
        if (Input.GetKeyDown(KeyCode.S))
        {
            inputState = InputState.Down;
            lastInputTime = Time.time;
            isQCFInProgress = true;
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
                Debug.Log("QCF " + (facingRight ? "Right" : "Left") + " Special Action");
                StartCoroutine(ResetQCFState());
            }
        }
        else if (Time.time - lastInputTime > inputBufferTime)
        {
            inputState = InputState.None;
            isQCFInProgress = false;
        }
    }

    private void HandleHCBF()
    {
        if (Input.GetKeyDown(facingRight ? KeyCode.D : KeyCode.A))
        {
            inputState = InputState.Forward;
            lastInputTime = Time.time;
            isHCBFInProgress = true;
        }
        else if (inputState == InputState.Forward && Time.time - lastInputTime <= inputBufferTime)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                inputState = InputState.Backward;
                lastInputTime = Time.time;
            }
        }
        else if (inputState == InputState.Backward && Time.time - lastInputTime <= inputBufferTime)
        {
            if (Input.GetKeyDown(facingRight ? KeyCode.D : KeyCode.A))
            {
                inputState = InputState.ForwardAgain;
                lastInputTime = Time.time;
            }
        }
        else if (inputState == InputState.ForwardAgain && Time.time - lastInputTime <= inputBufferTime)
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                Debug.Log("HCBF " + (facingRight ? "Right" : "Left") + " Special Action");
                StartCoroutine(ResetHCBFState());
            }
        }
        else if (Time.time - lastInputTime > inputBufferTime)
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
            Debug.Log("Crouch and " + actionName);
        }
        else if (!IsGrounded())
        {
            Debug.Log("Airborne " + actionName);
        }
        else if (facingRight)
        {
            if (Input.GetKey(KeyCode.D))
            {
                Debug.Log("Special " + actionName);
            }
            else
            {
                Debug.Log(actionName);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.A))
            {
                Debug.Log("Special " + actionName);
            }
            else
            {
                Debug.Log(actionName);
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

    private IEnumerator ResetQCFState()
    {
        yield return new WaitForSeconds(actionCooldown);
        inputState = InputState.None;
        isQCFInProgress = false;
    }

    private IEnumerator ResetHCBFState()
    {
        yield return new WaitForSeconds(actionCooldown);
        inputState = InputState.None;
        isHCBFInProgress = false;
    }
}