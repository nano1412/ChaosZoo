using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player02Movement_Overdrive : MonoBehaviour
{
    public GameObject player02;
    public GameObject oppenent;
    public Player01MovementChallenge movementScript;
    public Player01TakeActionInChallenge  actionScript;
    public Player01TakeActionMultiButtonInChallange actionMultiButtonInChallange;
    public Vector3 oppPosition;
    public Animator anim;
    public Rigidbody rb;
    private bool FaceingLeft = true;
    private bool FaceingRight = false;
    public bool faceLeft => FaceingLeft;
    [SerializeField] private List<string> validTags = new List<string>();
    [SerializeField] private ChalllengeScripttable challengeData;
    public SelectControllerInChallenge selectControllerInChallenge;
    public GetValueInChallenge getValueInChallenge;
    public string animationOverdrive;

    public int currentTagIndex = 0;
    public int currentProgress = 0;
    public int currenthits = 0;
    public int IndexTag = 0;
    public int limitTime = 0;
    public float time;
    public bool stopAttack = false;
    private bool hasKenAttackExecuted = false;
    public bool isValidTagCompleted = false; // เพิ่มตัวแปรนี้
    public List<RedGreenValue> ProgressInput = new List<RedGreenValue>();
    public List<int> numberHits = new List<int>();
    void Start()
    {
        StartCoroutine(FaceLeft());

    }

    void Update()
    {
        oppPosition = oppenent.transform.position;
        if (oppPosition.x > player02.transform.position.x && !FaceingRight)
        {
            StartCoroutine(FaceRight());
        }
        else if (oppPosition.x < player02.transform.position.x && !FaceingLeft)
        {
            StartCoroutine(FaceLeft());
        }
    
        if(currentTagIndex == IndexTag)
        {
            isValidTagCompleted = true;
        }
        if(time > limitTime)
        {
            StopCountingTime();
            challengeData.boolList.Add(false);
            hasKenAttackExecuted = true;
            getValueInChallenge.RedUpdate();
            RedUpdate();

            Time.timeScale = 0;
            DisablePlayerControls(); // Disable player controls
            selectControllerInChallenge.DisableScripts();
            selectControllerInChallenge.ResetScene();
            challengeData.CurrentRound++;
            stopAttack = false;
            //StartCoroutine(ResetCurrenttext(0.5f));
            StartCoroutine(ResetCurrenttext(2f));
        }
    }

    public void TakeDamage(int damage, float force, string actionGrapName)
    {
        time = 0;
        StartCoroutine(StartCountingTime());

        if(isValidTagCompleted)
        {
            StartCoroutine(ResetCurrenttext(2f));
            StopCountingTime();
            return;
        }
        if (animationOverdrive == "Challenge")
        {
            if (actionGrapName == validTags[currentTagIndex])
            {
                anim.SetTrigger("Hurt");
                currentTagIndex++;
                if (currenthits < numberHits.Count && currentTagIndex == numberHits[currenthits])
                {
                    GreenUpdate();
                    currenthits++; // ไปยัง hit ถัดไป
                }
                if (stopAttack)
                {
                    // StopCountingTime();
                    // challengeData.boolList.Add(false);
                    // getValueInChallenge.RedUpdate();
                    // RedUpdate();


                    // Time.timeScale = 0;
                    // DisablePlayerControls(); // Disable player controls
                    // selectControllerInChallenge.DisableScripts();
                    // selectControllerInChallenge.ResetScene();
                    
                    // challengeData.CurrentRound++;
                    // stopAttack = false;
                    // //StartCoroutine(ResetCurrenttext(0.5f));
                    // StartCoroutine(ResetCurrenttext(2f));
                }
                if (currentTagIndex >= validTags.Count && !stopAttack && !hasKenAttackExecuted)
                {
                    StopCountingTime();
                    isValidTagCompleted = true;
                    hasKenAttackExecuted = true;
                    challengeData.boolList.Add(true);
                    getValueInChallenge.GreenUpdate();
                    challengeData.CurrentRound++;

                    Time.timeScale = 0;
                    DisablePlayerControls(); // Disable player controls
                    selectControllerInChallenge.DisableScripts();
                    selectControllerInChallenge.ResetScene();
                    //StartCoroutine(ResetCurrenttext(0.5f));
                    StartCoroutine(ResetCurrenttext(2f));
                }
            }
            else if(!isValidTagCompleted && actionGrapName != validTags[currentTagIndex])
            {
                StopCountingTime();
                isValidTagCompleted = true;
                anim.SetTrigger("Knock");
                challengeData.boolList.Add(false);
                hasKenAttackExecuted = true;
                getValueInChallenge.RedUpdate();
                RedUpdate();


                Time.timeScale = 0;
                DisablePlayerControls(); // Disable player controls
                selectControllerInChallenge.DisableScripts();
                selectControllerInChallenge.ResetScene();
                challengeData.CurrentRound++;
                stopAttack = false;
                //StartCoroutine(ResetCurrenttext(0.5f));
                StartCoroutine(ResetCurrenttext(2f));
            }
        }
        else if(animationOverdrive != "Challenge")
        {
            if(actionGrapName == "no")
            {
                if(!hasKenAttackExecuted)
                {
                    StopCountingTime();
                    anim.SetTrigger("Knock");
                    challengeData.boolList.Add(false);
                    currentTagIndex = 0; // Reset tag index for the next round
                    getValueInChallenge.RedUpdate();
                    RedUpdate();

                    Time.timeScale = 0;
                    DisablePlayerControls(); // Disable player controls
                    selectControllerInChallenge.DisableScripts();
                    selectControllerInChallenge.ResetScene();
                    challengeData.CurrentRound++;
                    hasKenAttackExecuted = true;
                    StartCoroutine(ResethaskanAttack());
                }
            }
            else if(animationOverdrive == "632146S_Shark" && actionGrapName == "632146S_Shark")
            {
                StopCountingTime();
                anim.SetTrigger("Shark_grab_HCBF");
                StartCoroutine(GetValue(5f));
            }
            else if(animationOverdrive == "6LPRPLKRP_Capybara" && actionGrapName == "6LPRPLKRP_Capybara")
            {
                StopCountingTime();
                anim.SetTrigger("Hurt");
                StartCoroutine(GetValue(1f));
            }
            else if(animationOverdrive == "4RPLPRKLK_Ken" && actionGrapName == "4RPLPRKLK_Ken")
            {
                if(!hasKenAttackExecuted)
                {
                    StopCountingTime();
                    anim.SetTrigger("Ken_4RPLPRKLK");
                    StartCoroutine(GetValue(3f));
                    hasKenAttackExecuted = true;
                }
            }
        }
    }

    private void DisablePlayerControls()
    {

        if (movementScript != null)
        {
            movementScript.enabled = false;
        }

        if (actionScript != null)
        {
            actionScript.enabled = false;
            actionMultiButtonInChallange.enabled = false;
        }

        Player02MoveInChallenge player02MovementScript = player02.GetComponent<Player02MoveInChallenge>();
        if (player02MovementScript != null)
        {
            player02MovementScript.enabled = false;
        }
        time = 0;
        hasKenAttackExecuted = false;

        // Disable any other scripts or controls as needed
    }
    IEnumerator FaceLeft()
    {
        if (!FaceingLeft)
        {
            FaceingLeft = true;
            FaceingRight = false;
            yield return new WaitForSeconds(0.15f);

            Vector3 newScale = transform.localScale;
            newScale.x = Mathf.Abs(newScale.x) * -1;
            transform.localScale = newScale;
        }
    }

    IEnumerator FaceRight()
    {
        if (!FaceingRight)
        {
            FaceingRight = true;
            FaceingLeft = false;
            yield return new WaitForSeconds(0.15f);

            Vector3 newScale = transform.localScale;
            newScale.x = Mathf.Abs(newScale.x);
            transform.localScale = newScale;
        }
    }

    IEnumerator GetValue(float time)
    {
        yield return new WaitForSeconds(time);
        challengeData.boolList.Add(true);
        currentTagIndex = 0; // Reset tag index for the next round
        getValueInChallenge.GreenUpdate();
        GreenUpdate();
        challengeData.CurrentRound++;
        time = 0;

        Time.timeScale = 0;
        DisablePlayerControls(); // Disable player controls
        selectControllerInChallenge.DisableScripts();
        selectControllerInChallenge.ResetScene();
        hasKenAttackExecuted = false;

    }

    IEnumerator ResethaskanAttack()
    {
        yield return new WaitForSeconds(0.5f);
        hasKenAttackExecuted = false;

    }

    IEnumerator ResetCurrenttext(float delay)
    {
        yield return new WaitForSeconds(delay);
        //currentTagIndex = 0;
        isValidTagCompleted = false;
        stopAttack = false;
    }
    private IEnumerator StartCountingTime()
    {
        while (true)
        {
            time += Time.deltaTime;
            yield return null;
        }
    }
    private void StopCountingTime()
    {
        StopAllCoroutines();
        time = 0;
    }

    public void GreenUpdate()
    {
        ProgressInput[currentProgress].Green.SetActive(true);
        currentProgress++;
    }

    public void RedUpdate()
    {
        ProgressInput[currentProgress].Red.SetActive(true);
        currentProgress++;
    }

    public void ResetGreenRed()
    {
        for(int i = 0; i < ProgressInput.Count; i++)
        {
            ProgressInput[i].Green.SetActive(false);
            ProgressInput[i].Red.SetActive(false);
            
        }
        currentProgress = 0;
        currenthits = 0;
    }

    public void SkipPhaseController()
    {
        if(challengeData.CurrentRound == 0 || challengeData.CurrentRound == 5)
        {
            StopCountingTime();
            isValidTagCompleted = true;

            for(int i = 0; i < 5; i++)
            {
                challengeData.boolList.Add(false);
                challengeData.CurrentRound++;
                getValueInChallenge.RedUpdate();    
            }
            selectControllerInChallenge.DisableScripts();
            selectControllerInChallenge.ResetScene();
            stopAttack = false;
            StartCoroutine(ResetCurrenttext(2f));
        }
    }
}
