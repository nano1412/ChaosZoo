using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetSceneInChallenge : MonoBehaviour
{
    public void ResetScene()
    {
        // รอ 1 วินาทีก่อนทำการรีเซ็ตฉาก
        StartCoroutine(ResetAfterDelay());
    }

    private IEnumerator ResetAfterDelay()
    {
        // รอ 1 วินาที
        yield return new WaitForSeconds(1f);

        // ทำการรีเซ็ตฉาก
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
