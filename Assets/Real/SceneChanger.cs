using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerButton : MonoBehaviour
{
    public string sceneToLoad = "01";
    public string sceneToLoadInChallenge01 = "Challenge01";
    public string sceneToLoadInChallenge02 = "Challenge02";
    public string sceneToLoadInChallenge03 = "Challenge03";
    public ChalllengeScripttable challlengeScripttable01;
    public ChalllengeScripttable challlengeScripttable02;
    public ChalllengeScripttable challlengeScripttable03;
    public GameObject selectlevel;

    public void ChangeTraining()
    {
        // เปลี่ยนไปยัง Scene ที่กำหนด
        SceneManager.LoadScene(sceneToLoad);
    }

    public void SelectLevelInChallenge()
    {
        selectlevel.SetActive(true);
    }

    public void CloseLevelInChallenge()
    {
        selectlevel.SetActive(false);
    }

    public void SelectChallenge01()
    {
        SceneManager.LoadScene(sceneToLoadInChallenge01);
        ClearChallengeData(challlengeScripttable01);

    }
    public void SelectChallenge02()
    {
        SceneManager.LoadScene(sceneToLoadInChallenge02);
        ClearChallengeData(challlengeScripttable02);

    }
    public void SelectChallenge03()
    {
        SceneManager.LoadScene(sceneToLoadInChallenge03);
        ClearChallengeData(challlengeScripttable03);

    }

    private void ClearChallengeData(ChalllengeScripttable challengeData)
    {
        // Reset CurrentRound to 0
        challengeData.CurrentRound = 0;

        // Clear the boolList
        challengeData.boolList.Clear(); // ต้องใช้วงเล็บ () เพื่อเรียกใช้เมธอด
    }
}
