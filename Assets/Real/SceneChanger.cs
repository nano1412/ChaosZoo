using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SceneChangerButton : MonoBehaviour
{
    public List<string> sceneToLoad = new List<string>();
    public List<ChalllengeScripttable> challengeScriptable = new List<ChalllengeScripttable>();
    public GameObject selectTrainingRoom;
    public GameObject selectChallenge;
    public GameObject trainingModeButton;
    public GameObject challengeModeButton;


    public void SelectTrainingRoom()
    {
        selectTrainingRoom.SetActive(true);
        trainingModeButton.SetActive(false);
        challengeModeButton.SetActive(false);
    }

    public void CloseSelectTrainingRoom()
    {
        selectTrainingRoom.SetActive(false);
        trainingModeButton.SetActive(true);
        challengeModeButton.SetActive(true);
    }

    public void SelectChallenge()
    {
        selectChallenge.SetActive(true);
        trainingModeButton.SetActive(false);
        challengeModeButton.SetActive(false);
    }

    public void CloseSelectChallenge()
    {
        selectChallenge.SetActive(false);
        trainingModeButton.SetActive(true);
        challengeModeButton.SetActive(true);
    }

    public void TrainingRoomShark()
    {
        SceneManager.LoadScene(sceneToLoad[3]);
    }

    public void TrainingRoomCapybara()
    {
        SceneManager.LoadScene(sceneToLoad[0]);
    }

    public void TrainingRoomKen()
    {
        SceneManager.LoadScene(sceneToLoad[1]);
    }

    public void TrainingRoomPengang()
    {
        SceneManager.LoadScene(sceneToLoad[2]);
    }

    public void ChallengeShark()
    {
        SceneManager.LoadScene(sceneToLoad[7]);
        ClearChallengeData(challengeScriptable[12]);
        ClearChallengeData(challengeScriptable[13]);
        ClearChallengeData(challengeScriptable[14]);
        ClearChallengeData(challengeScriptable[15]);
    }

    public void ChallengeCapybara()
    {
        SceneManager.LoadScene(sceneToLoad[4]);
        ClearChallengeData(challengeScriptable[0]);
        ClearChallengeData(challengeScriptable[1]);
        ClearChallengeData(challengeScriptable[2]);
        ClearChallengeData(challengeScriptable[3]);
    }

    public void ChallengeKen()
    {
        SceneManager.LoadScene(sceneToLoad[5]);
        ClearChallengeData(challengeScriptable[4]);
        ClearChallengeData(challengeScriptable[5]);
        ClearChallengeData(challengeScriptable[6]);
        ClearChallengeData(challengeScriptable[7]);
    }

    public void ChallengePengang()
    {
        SceneManager.LoadScene(sceneToLoad[6]);
        ClearChallengeData(challengeScriptable[8]);
        ClearChallengeData(challengeScriptable[9]);
        ClearChallengeData(challengeScriptable[10]);
        ClearChallengeData(challengeScriptable[11]);
    }

    private void ClearChallengeData(ChalllengeScripttable challengeData)
    {
        // Reset CurrentRound to 0
        challengeData.CurrentRound = 0;

        // Clear the boolList
        challengeData.boolList.Clear(); // ต้องใช้วงเล็บ () เพื่อเรียกใช้เมธอด
    }
}
