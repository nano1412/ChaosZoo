using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnimationFrameConfigInChallenge
{
    public string animationName;
    public int frameCount;  // จำนวนเฟรม
}

public class Player01TakeActionInChallenge : MonoBehaviour
{
    public float defaultActionCooldown = 0.5f; // เวลา default
    public List<AnimationFrameConfig> animationFrameConfigs;
    public GameObject player01;
    public Player01MovementChallenge player01Movement;
    public SelectControllerInChallenge selectControllerInChallenge;
    public bool isPerformingAction = false;
    public static bool Hits = false;
    public bool hits => Hits;

    private Animator anim;
    private float fps = 60f;  // จำนวนเฟรมต่อวินาทีของเกม (สามารถปรับให้เหมาะสม)

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isPerformingAction) return;

        if (selectControllerInChallenge.Selectjoystick)
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

    private void PerformAction(string actionName)
    {   
        isPerformingAction = true;
        player01Movement.isPerformingAction = true;

        string verticalInput = selectControllerInChallenge.Selectjoystick ? "VerticalJoystick" : "Vertical";
        string horizontalInput = selectControllerInChallenge.Selectjoystick ? "HorizontalJoyStick" : "Horizontal";

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
            if (Input.GetAxis(horizontalInput) < 0.4f)
            {
                anim.SetTrigger("Special" + actionName + "Trigger");
            }
            else
            {
                anim.SetTrigger(actionName + "Trigger");
            }
        }

        float actionCooldown = GetFrameDelay(actionName);
        StartCoroutine(ResetIsPerformingAction(actionCooldown));
    }

    private float GetFrameDelay(string actionName)
    {
        foreach (var config in animationFrameConfigs)
        {
            if (config.animationName == actionName)
            {
                return config.frameCount / fps;
            }
        }
        return defaultActionCooldown;
    }

    IEnumerator ResetIsPerformingAction(float delay)
    {
        yield return new WaitForSeconds(delay);
        isPerformingAction = false;
        player01Movement.isPerformingAction = false;
    }

    public void OnHits()
    {
        StopCoroutine(ResetIsPerformingAction(0));
        isPerformingAction = false;
        player01Movement.isPerformingAction = true;
    }
}
