using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player02MoveInChallenge : MonoBehaviour
{
    public GameObject player02;
    public GameObject player01;
    public GameObject opponent;
    public Player01MovementChallenge movementScript;
    public Player01TakeActionInChallenge  actionScript;
    public Vector3 oppPosition;
    private Animator anim;
    private Rigidbody rb;
    private bool FaceingLeft = true;
    private bool FaceingRight = false;
    public bool faceLeft => FaceingLeft;
    [SerializeField] private List<string> validTags = new List<string>();
    [SerializeField] private ChalllengeScripttable challengeData;
    public SelectControllerInChallenge selectControllerInChallenge;

    private int currentTagIndex = 0;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        StartCoroutine(FaceLeft());
    }

    void Update()
    {
        oppPosition = opponent.transform.position;
        if (oppPosition.x > player02.transform.position.x && !FaceingRight)
        {
            StartCoroutine(FaceRight());
        }
        else if (oppPosition.x < player02.transform.position.x && !FaceingLeft)
        {
            StartCoroutine(FaceLeft());
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
            if (currentTagIndex >= validTags.Count)
            {
                challengeData.boolList.Add(true);
                challengeData.CurrentRound++;
                currentTagIndex = 0; // Reset tag index for the next round

                Time.timeScale = 0;
                DisablePlayerControls(); // Disable player controls
                selectControllerInChallenge.DisableScripts();
                selectControllerInChallenge.ResetScene();
            }
        }
        else
        {
            Debug.Log("Tag mismatch: " + other.tag + ". Expected: " + validTags[currentTagIndex]);
            challengeData.boolList.Add(false);
            challengeData.CurrentRound++;
            currentTagIndex = 0; // Reset tag index for the next round

            Time.timeScale = 0;
            DisablePlayerControls(); // Disable player controls
            selectControllerInChallenge.DisableScripts();
            selectControllerInChallenge.ResetScene();
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
