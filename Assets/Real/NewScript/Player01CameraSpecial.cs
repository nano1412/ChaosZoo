using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player01CameraSpecial : MonoBehaviour
{
    public GameObject cameraSpecial;
    public float cooldown;
    public float cooldown_2;
    public GameObject playerHealthBar01;
    public GameObject playerHealthBar02;
    public GameObject BlackScene;
    public GameObject BlackScene2;

    public void CameraSetActive()
    {
        cameraSpecial.SetActive(true);
        StartCoroutine(ResetCamareSpecial());
    }

    public void CameraAtciveSpecial()
    {
        cameraSpecial.SetActive(true);
        playerHealthBar01.SetActive(false);
        playerHealthBar02.SetActive(false);
        BlackScene.SetActive(true);
        BlackScene2.SetActive(true);
        StartCoroutine(ResetCamareGrap());
    }
    public void cameraHCB()
    {
        cameraSpecial.SetActive(true);
        StartCoroutine(ResetCamareHCB());
    }

    public void SpecialCapybaraCamare()
    {
        cameraSpecial.SetActive(true);
        playerHealthBar01.SetActive(false);
        playerHealthBar02.SetActive(false);
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
        playerHealthBar01.SetActive(true);
        playerHealthBar02.SetActive(true);
    }

    IEnumerator ResetCamareHCB()
    {
        yield return new WaitForSeconds(cooldown_2);
        cameraSpecial.SetActive(false);
    }

    IEnumerator ResetCapybaraCamera(float timereset)
    {
        yield return new WaitForSeconds(timereset);
        playerHealthBar01.SetActive(true);
        playerHealthBar02.SetActive(true);
        cameraSpecial.SetActive(false);
    }
}
