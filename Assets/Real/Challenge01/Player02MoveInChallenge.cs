using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player02MoveInChallenge : MonoBehaviour
{
    public GameObject player02;
    public GameObject oppenent;
    public Player01MovementChallenge movementScript;
    public Player01TakeActionInChallenge  actionScript;
    public Player01TakeActionMultiButtonInChallange actionMultiButton;
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

    private int currentTagIndex = 0;
    public float time;
    public bool stopAttack = false;

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
        
        if(currentTagIndex >= 1)
        {
            time += Time.deltaTime;
            if(time > 4)
            {
                stopAttack = true;
                challengeData.boolList.Add(false);
                currentTagIndex = 0; // Reset tag index for the next round
                getValueInChallenge.RedUpdate();

                Time.timeScale = 0;
                DisablePlayerControls(); // Disable player controls
                selectControllerInChallenge.DisableScripts();
                selectControllerInChallenge.ResetScene();
                challengeData.CurrentRound++;
                time = 0;
                stopAttack = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentTagIndex < validTags.Count)
        {
            if (other.tag == validTags[currentTagIndex])
            {
                anim.SetTrigger("Hurt");
                currentTagIndex++;
                if(stopAttack)
                {
                    Debug.Log("Tag mismatch: " + other.tag + ". Expected: " + validTags[currentTagIndex]);
                    challengeData.boolList.Add(false);
                    currentTagIndex = 0; // Reset tag index for the next round
                    getValueInChallenge.RedUpdate();

                    Time.timeScale = 0;
                    DisablePlayerControls(); // Disable player controls
                    selectControllerInChallenge.DisableScripts();
                    selectControllerInChallenge.ResetScene();
                    challengeData.CurrentRound++;
                    stopAttack = false;
                }
                if (currentTagIndex >= validTags.Count && !stopAttack)
                {
                    challengeData.boolList.Add(true);
                    currentTagIndex = 0; // Reset tag index for the next round
                    getValueInChallenge.GreenUpdate();
                    challengeData.CurrentRound++;
                    time = 0;

                    Time.timeScale = 0;
                    DisablePlayerControls(); // Disable player controls
                    selectControllerInChallenge.DisableScripts();
                    selectControllerInChallenge.ResetScene();
                    stopAttack = false;
                }
            }
            else
            {
                anim.SetTrigger("Knock");
                Debug.Log("Tag mismatch: " + other.tag + ". Expected: " + validTags[currentTagIndex]);
                challengeData.boolList.Add(false);
                currentTagIndex = 0; // Reset tag index for the next round
                getValueInChallenge.RedUpdate();

                Time.timeScale = 0;
                DisablePlayerControls(); // Disable player controls
                selectControllerInChallenge.DisableScripts();
                selectControllerInChallenge.ResetScene();
                challengeData.CurrentRound++;
                time = 0;
                stopAttack = false;
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
            actionMultiButton.enabled = false;
        }

        Player02MoveInChallenge player02MovementScript = player02.GetComponent<Player02MoveInChallenge>();
        if (player02MovementScript != null)
        {
            player02MovementScript.enabled = false;
        }

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
}
