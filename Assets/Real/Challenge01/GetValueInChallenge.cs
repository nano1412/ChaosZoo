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
    public GameObject Success;
    public GameObject Failed;
    public int CurrentIndex = 0;

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
}
