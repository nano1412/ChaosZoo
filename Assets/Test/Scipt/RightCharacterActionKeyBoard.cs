using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightCharacterActionKeyBoard : MonoBehaviour
{
    public RightLookEnemy rightLookEnemy;
    public bool facingLeft = true;
    public int specialMoveEnergy = 100;

    private bool isPerformingAction = false;
    private float actionCooldown = 0.1f;
    private bool isQCInProgress = false;
    private bool isHCBFInProgress = false;

    private enum InputState { None, Down, Forward, Backward, ForwardAgain }
    private InputState inputState = InputState.None;
    private float lastInputTime;
    private float inputBufferTime = 0.2f;

    void Update()
    {
        HandleQCF();
        HandleQCE();
        HandleHCBF();

        if (isPerformingAction || isQCInProgress || isHCBFInProgress)
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
            if (Input.GetKeyDown(facingLeft ? KeyCode.A : KeyCode.D))
            {
                inputState = InputState.Forward;
                lastInputTime = Time.time;
            }
        }
        else if (inputState == InputState.Forward && Time.time - lastInputTime <= inputBufferTime)
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                Debug.Log("QCF " + (facingLeft ? "Left" : "Right") + " " + "Punch" + " Special Action");
                StartCoroutine(ResetQCFState());
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                Debug.Log("QCF " + (facingLeft ? "Left" : "Right") + " " + " Kick " + " Special Action");
                StartCoroutine(ResetQCFState());
            }
            else if (Input.GetKeyDown(KeyCode.O))
            {
                Debug.Log("QCF " + (facingLeft ? "Left" : "Right") + " " + " Slash " + " Special Action");
                StartCoroutine(ResetQCFState());
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("QCF " + (facingLeft ? "Left" : "Right") + " " + " Hevily Slash " + " Special Action");
                StartCoroutine(ResetQCFState());
            }
        }
        else if (Time.time - lastInputTime > inputBufferTime)
        {
            inputState = InputState.None;
            isQCInProgress = false;
        }
    }

     private void HandleQCE()
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
            if (Input.GetKeyDown(facingLeft ? KeyCode.D : KeyCode.A))
            {
                inputState = InputState.Backward;
                lastInputTime = Time.time;
            }
        }
        else if (inputState == InputState.Backward && Time.time - lastInputTime <= inputBufferTime)
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                Debug.Log("QCE " + (facingLeft ? "Left" : "Right") + " " + "Punch" + " Special Action");
                StartCoroutine(ResetQCFState());
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                Debug.Log("QCE " + (facingLeft ? "Left" : "Right") + " " + " Kick " + " Special Action");
                StartCoroutine(ResetQCFState());
            }
            else if (Input.GetKeyDown(KeyCode.O))
            {
                Debug.Log("QCE " + (facingLeft ? "Left" : "Right") + " " + " Slash " + " Special Action");
                StartCoroutine(ResetQCFState());
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("QCE " + (facingLeft ? "Left" : "Right") + " " + " Hevily Slash " + " Special Action");
                StartCoroutine(ResetQCFState());
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
            if (Input.GetKeyDown(facingLeft ? KeyCode.A : KeyCode.D))
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
            if (Input.GetKeyDown(facingLeft ? KeyCode.D : KeyCode.A))
            {
                inputState = InputState.Backward;
                lastInputTime = Time.time;
                return;
            }
        }

        if (inputState == InputState.Backward && Time.time - lastInputTime <= inputBufferTime)
        {
            if (Input.GetKeyDown(facingLeft ? KeyCode.A : KeyCode.D))
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
                Debug.Log("HCBF " + (facingLeft ? "Left" : "Right") + " " + " Punch " +  " Special Action");
                specialMoveEnergy = 0;
                StartCoroutine(ResetHCBFState());
                return;
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                Debug.Log("HCBF " + (facingLeft ? "Left" : "Right") + " " + " KicK " +  " Special Action");
                specialMoveEnergy = 0;
                StartCoroutine(ResetHCBFState());
                return;
            }
            else if (Input.GetKeyDown(KeyCode.O))
            {
                Debug.Log("HCBF " + (facingLeft ? "Left" : "Right") + " " + " Slash " +  " Special Action");
                specialMoveEnergy = 0;
                StartCoroutine(ResetHCBFState());
                return;
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("HCBF " + (facingLeft ? "Left" : "Right") + " " + " Hevily Slash " +  " Special Action");
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
            Debug.Log("Crouch and " + actionName);
        }
        else if (!IsGrounded())
        {
            Debug.Log("Airborne " + actionName);
        }
        else if (facingLeft)
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
        else
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
        isQCInProgress = false;
    }

    private IEnumerator ResetHCBFState()
    {
        yield return new WaitForSeconds(actionCooldown);
        inputState = InputState.None;
        isHCBFInProgress = false;
    }
}