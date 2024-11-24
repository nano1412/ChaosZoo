using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player01CameraSpecial : MonoBehaviour
{
    public GameObject cameraSpecial;
    public float cooldown;

    public void CameraSetActive()
    {
        cameraSpecial.SetActive(true);
        StartCoroutine(ResetCamareSpecial());
    }

    public void CameraAtciveSpecial()
    {
        cameraSpecial.SetActive(true);
        StartCoroutine(ResetCamareGrap());

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

    }
}
