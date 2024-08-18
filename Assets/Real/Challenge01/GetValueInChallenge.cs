using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RedGreenValue
{
    public GameObject Green;
    public GameObject Red;
}
public class GetValueInChallenge : MonoBehaviour
{
    public List<RedGreenValue> ValueGreenRed;
    public GameObject KeyBoardGroup;
    public GameObject JoyStickGroup;
    public GameObject ArcadeStickGroup;
    public GameObject Success;
    public GameObject Failed;
    public GameObject KeyBoard;
    public GameObject JoyStick;
    public GameObject ArcadeStick;
    public int CurrentIndex = 0;
    public ChalllengeScripttable challlengeScripttable;

    void Update()
    {
        if(challlengeScripttable.CurrentRound == 0)
        {
            KeyBoardGroup.SetActive(true);
            JoyStickGroup.SetActive(false);
            ArcadeStickGroup.SetActive(false);
            KeyBoard.SetActive(true);
            JoyStick.SetActive(false);
            ArcadeStick.SetActive(false);
        }
        else if(challlengeScripttable.CurrentRound == 5)
        {
            StartCoroutine(ChangelayoutToJoyStick());
        }
        else if(challlengeScripttable.CurrentRound == 5)
        {
            StartCoroutine(ChangelayoutToArcadeStrick());
        }
    }

    public void GreenUpdate()
    {
        ValueGreenRed[CurrentIndex].Green.SetActive(true);
        CurrentIndex++;
        StartCoroutine(SucessSetActive());
    }

    public void RedUpdate()
    {
        ValueGreenRed[CurrentIndex].Red.SetActive(true);
        CurrentIndex++;
        StartCoroutine(FailedActive());

    }

    IEnumerator SucessSetActive()
    {
        yield return new WaitForSeconds(0.5f);
        Success.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Success.SetActive(false);
    }

    IEnumerator FailedActive()
    {
        yield return new WaitForSeconds(0.5f);
        Failed.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Failed.SetActive(false);
    }

    IEnumerator ChangelayoutToJoyStick()
    {
        yield return new WaitForSeconds(2f);
        KeyBoardGroup.SetActive(false);
        JoyStickGroup.SetActive(true);
        ArcadeStickGroup.SetActive(false);
        KeyBoard.SetActive(false);
        JoyStick.SetActive(true);
        ArcadeStick.SetActive(false);
    }

    IEnumerator ChangelayoutToArcadeStrick()
    {
        yield return new WaitForSeconds(2f);
        KeyBoardGroup.SetActive(false);
        JoyStickGroup.SetActive(false);
        ArcadeStickGroup.SetActive(true);
        KeyBoard.SetActive(false);
        JoyStick.SetActive(false);
        ArcadeStick.SetActive(true);
    }
}
