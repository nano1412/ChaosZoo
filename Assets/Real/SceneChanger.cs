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
    public GameObject selectChallengeShark;
    public GameObject selectChallengeCapybara;
    public GameObject selectChallengeKen;
    public GameObject selectChallengePengang;
    public GameObject camera01;
    public GameObject camera02;
    public GameObject cameraSharkChallenge;
    public GameObject cameraCapybaraChallenge;
    public GameObject cameraKenChallenge;
    public GameObject cameraPengangChallenge;
    public GameObject SelectMapShark;
    public GameObject SelectMapCapybara;
    public GameObject SelectMapKen;
    public GameObject SelectMapPengang;

    void Start()
    {
        camera01.SetActive(false);
        camera02.SetActive(false);
    }


    public void SelectTrainingRoom()
    {
        camera01.SetActive(true);
        selectTrainingRoom.SetActive(true);
        trainingModeButton.SetActive(false);
        challengeModeButton.SetActive(false);
    }

    public void CloseSelectTrainingRoom()
    {
        camera01.SetActive(false);
        selectTrainingRoom.SetActive(false);
        trainingModeButton.SetActive(true);
        challengeModeButton.SetActive(true);
    }

    public void SelectChallenge()
    {
        camera02.SetActive(true);
        selectChallenge.SetActive(true);
        trainingModeButton.SetActive(false);
        challengeModeButton.SetActive(false);
    }

    public void CloseSelectChallenge()
    {
        camera02.SetActive(false);
        selectChallenge.SetActive(false);
        trainingModeButton.SetActive(true);
        challengeModeButton.SetActive(true);
    }

    public void SelectChallengeShark()
    {
        cameraSharkChallenge.SetActive(true);
        cameraCapybaraChallenge.SetActive(false);
        cameraKenChallenge.SetActive(false);
        cameraPengangChallenge.SetActive(false);
        selectChallengeShark.SetActive(true);
        selectChallengeCapybara.SetActive(false);
        selectChallengeKen.SetActive(false);
        selectChallengePengang.SetActive(false);
        selectChallenge.SetActive(false);
    }

    public void SelectChallengeCapybara()
    {
        cameraSharkChallenge.SetActive(false);
        cameraCapybaraChallenge.SetActive(true);
        cameraKenChallenge.SetActive(false);
        cameraPengangChallenge.SetActive(false);

        selectChallengeShark.SetActive(false);
        selectChallengeCapybara.SetActive(true);
        selectChallengeKen.SetActive(false);
        selectChallengePengang.SetActive(false);
        selectChallenge.SetActive(false);
    }

    public void SelectChallengeKen()
    {
        cameraSharkChallenge.SetActive(false);
        cameraCapybaraChallenge.SetActive(false);
        cameraKenChallenge.SetActive(true);
        cameraPengangChallenge.SetActive(false);

        selectChallengeShark.SetActive(false);
        selectChallengeCapybara.SetActive(false);
        selectChallengeKen.SetActive(true);
        selectChallengePengang.SetActive(false);
        selectChallenge.SetActive(false);
    }

    public void SelectChallengePengang()
    {
        cameraSharkChallenge.SetActive(false);
        cameraCapybaraChallenge.SetActive(false);
        cameraKenChallenge.SetActive(false);
        cameraPengangChallenge.SetActive(true);

        selectChallengeShark.SetActive(false);
        selectChallengeCapybara.SetActive(false);
        selectChallengeKen.SetActive(false);
        selectChallengePengang.SetActive(true);
        selectChallenge.SetActive(false);
    }

    public void ExitChallengeSelectNumber()
    {
        cameraSharkChallenge.SetActive(false);
        cameraCapybaraChallenge.SetActive(false);
        cameraKenChallenge.SetActive(false);
        cameraPengangChallenge.SetActive(false);
        
        selectChallengeShark.SetActive(false);
        selectChallengeCapybara.SetActive(false);
        selectChallengeKen.SetActive(false);
        selectChallengePengang.SetActive(false);
        selectChallenge.SetActive(true);
    }

    public void SharkSelectMap()
    {
        selectTrainingRoom.SetActive(false);
        SelectMapShark.SetActive(true);
        SelectMapCapybara.SetActive(false);
        SelectMapKen.SetActive(false);
        SelectMapPengang.SetActive(false);
    }
    public void CapybaraSelectMap()
    {
        selectTrainingRoom.SetActive(false);
        SelectMapShark.SetActive(false);
        SelectMapCapybara.SetActive(true);
        SelectMapKen.SetActive(false);
        SelectMapPengang.SetActive(false);
    }
    public void KenSelectMap()
    {
        selectTrainingRoom.SetActive(false);
        SelectMapShark.SetActive(false);
        SelectMapCapybara.SetActive(false);
        SelectMapKen.SetActive(true);
        SelectMapPengang.SetActive(false);
    }
    public void PengangSelectMap()
    {
        selectTrainingRoom.SetActive(false);
        SelectMapShark.SetActive(false);
        SelectMapCapybara.SetActive(false);
        SelectMapKen.SetActive(false);
        SelectMapPengang.SetActive(true);   
    }
    public void CloseAllSelectMap()
    {
        selectTrainingRoom.SetActive(true);
        SelectMapShark.SetActive(false);
        SelectMapCapybara.SetActive(false);
        SelectMapKen.SetActive(false);
        SelectMapPengang.SetActive(false);
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

    public void CityMapShark()
    {
        SceneManager.LoadScene(sceneToLoad[23]);
    }
    public void CityMapCapybara()
    {
        SceneManager.LoadScene(sceneToLoad[20]);
    }
    public void CityMapKen()
    {
        SceneManager.LoadScene(sceneToLoad[21]);
    }
    public void CityMapPengang()
    {
        SceneManager.LoadScene(sceneToLoad[22]);
    }
    public void floatingIslandShark()
    {
        SceneManager.LoadScene(sceneToLoad[27]);
    }
    public void floatingIslandCapybara()
    {
        SceneManager.LoadScene(sceneToLoad[24]);
    }
    public void floatingIslandKen()
    {
        SceneManager.LoadScene(sceneToLoad[25]);
    }
    public void floatingIslandPengang()
    {
        SceneManager.LoadScene(sceneToLoad[26]);
    }
    
    public void ChallengeShark01()
    {
        SceneManager.LoadScene(sceneToLoad[16]);
        ClearChallengeData(challengeScriptable[12]);
        ClearChallengeData(challengeScriptable[13]);
        ClearChallengeData(challengeScriptable[14]);
        ClearChallengeData(challengeScriptable[15]);
    }

    public void ChallengeShark02()
    {
        SceneManager.LoadScene(sceneToLoad[17]);
        ClearChallengeData(challengeScriptable[12]);
        ClearChallengeData(challengeScriptable[13]);
        ClearChallengeData(challengeScriptable[14]);
        ClearChallengeData(challengeScriptable[15]);
    }
    public void ChallengeShark03()
    {
        SceneManager.LoadScene(sceneToLoad[18]);
        ClearChallengeData(challengeScriptable[12]);
        ClearChallengeData(challengeScriptable[13]);
        ClearChallengeData(challengeScriptable[14]);
        ClearChallengeData(challengeScriptable[15]);
    }

    public void ChallengeSharkOD()
    {
        SceneManager.LoadScene(sceneToLoad[19]);
        ClearChallengeData(challengeScriptable[12]);
        ClearChallengeData(challengeScriptable[13]);
        ClearChallengeData(challengeScriptable[14]);
        ClearChallengeData(challengeScriptable[15]);
    }

    public void ChallengeCapybara01()
    {
        SceneManager.LoadScene(sceneToLoad[4]);
        ClearChallengeData(challengeScriptable[0]);
        ClearChallengeData(challengeScriptable[1]);
        ClearChallengeData(challengeScriptable[2]);
        ClearChallengeData(challengeScriptable[3]);
    }

    public void ChallengeCapybara02()
    {
        SceneManager.LoadScene(sceneToLoad[5]);
        ClearChallengeData(challengeScriptable[0]);
        ClearChallengeData(challengeScriptable[1]);
        ClearChallengeData(challengeScriptable[2]);
        ClearChallengeData(challengeScriptable[3]);
    }

    public void ChallengeCapybara03()
    {
        SceneManager.LoadScene(sceneToLoad[6]);
        ClearChallengeData(challengeScriptable[0]);
        ClearChallengeData(challengeScriptable[1]);
        ClearChallengeData(challengeScriptable[2]);
        ClearChallengeData(challengeScriptable[3]);
    }

    public void ChallengeCapybaraOD()
    {
        SceneManager.LoadScene(sceneToLoad[7]);
        ClearChallengeData(challengeScriptable[0]);
        ClearChallengeData(challengeScriptable[1]);
        ClearChallengeData(challengeScriptable[2]);
        ClearChallengeData(challengeScriptable[3]);
    }

    public void ChallengeKen01()
    {
        SceneManager.LoadScene(sceneToLoad[8]);
        ClearChallengeData(challengeScriptable[4]);
        ClearChallengeData(challengeScriptable[5]);
        ClearChallengeData(challengeScriptable[6]);
        ClearChallengeData(challengeScriptable[7]);
    }

    public void ChallengeKen02()
    {
        SceneManager.LoadScene(sceneToLoad[9]);
        ClearChallengeData(challengeScriptable[4]);
        ClearChallengeData(challengeScriptable[5]);
        ClearChallengeData(challengeScriptable[6]);
        ClearChallengeData(challengeScriptable[7]);
    }

    public void ChallengeKen03()
    {
        SceneManager.LoadScene(sceneToLoad[10]);
        ClearChallengeData(challengeScriptable[4]);
        ClearChallengeData(challengeScriptable[5]);
        ClearChallengeData(challengeScriptable[6]);
        ClearChallengeData(challengeScriptable[7]);
    }

    public void ChallengeKenOD()
    {
        SceneManager.LoadScene(sceneToLoad[11]);
        ClearChallengeData(challengeScriptable[4]);
        ClearChallengeData(challengeScriptable[5]);
        ClearChallengeData(challengeScriptable[6]);
        ClearChallengeData(challengeScriptable[7]);
    }

    public void ChallengePengang01()
    {
        SceneManager.LoadScene(sceneToLoad[12]);
        ClearChallengeData(challengeScriptable[8]);
        ClearChallengeData(challengeScriptable[9]);
        ClearChallengeData(challengeScriptable[10]);
        ClearChallengeData(challengeScriptable[11]);
    }
    public void ChallengePengang02()
    {
        SceneManager.LoadScene(sceneToLoad[13]);
        ClearChallengeData(challengeScriptable[8]);
        ClearChallengeData(challengeScriptable[9]);
        ClearChallengeData(challengeScriptable[10]);
        ClearChallengeData(challengeScriptable[11]);
    }
    public void ChallengePengang03()
    {
        SceneManager.LoadScene(sceneToLoad[14]);
        ClearChallengeData(challengeScriptable[8]);
        ClearChallengeData(challengeScriptable[9]);
        ClearChallengeData(challengeScriptable[10]);
        ClearChallengeData(challengeScriptable[11]);
    }
    public void ChallengePengangOD()
    {
        SceneManager.LoadScene(sceneToLoad[15]);
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
