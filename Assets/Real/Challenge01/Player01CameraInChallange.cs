using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player01CameraInChallange : MonoBehaviour
{
    public GameObject cameraSpecial;
    public float cooldown;
    public float cooldown_2;
    public GameObject BlackScene;
    public GameObject BlackScene2;
    public GameObject canvas;

    public void CameraSetActive()
    {
        cameraSpecial.SetActive(true);
        StartCoroutine(ResetCamareSpecial());
    }

    public void CameraAtciveSpecial()
    {
        cameraSpecial.SetActive(true);
        canvas.SetActive(false);
        BlackScene.SetActive(true);
        BlackScene2.SetActive(true);
        StartCoroutine(ResetCamareGrap());
    }
     public void SpecialCapybaraCamare()
    {
        cameraSpecial.SetActive(true);
        canvas.SetActive(false);
        StartCoroutine(ResetCapybaraCamera(1f));
    }

    IEnumerator ResetCamareSpecial()
    {
        yield return new WaitForSeconds(cooldown);
        cameraSpecial.SetActive(false);

    }

    IEnumerator ResetCamareGrap()
    {
        yield return new WaitForSeconds(4f);
        cameraSpecial.SetActive(false);
        BlackScene.SetActive(false);
        BlackScene2.SetActive(false);
        canvas.SetActive(true);
    }

     IEnumerator ResetCapybaraCamera(float timereset)
    {
        yield return new WaitForSeconds(timereset);
        cameraSpecial.SetActive(false);
        canvas.SetActive(true);
    }
}
