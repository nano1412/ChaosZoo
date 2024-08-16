using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerButton : MonoBehaviour
{
    public string sceneToLoad = "01";
    public string sceneToLoadInChallenge01 = "Challenge01";
     public string sceneToLoadInChallenge02 = "Challenge02";
      public string sceneToLoadInChallenge03 = "Challenge03";
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
    }
    public void SelectChallenge02()
    {
        SceneManager.LoadScene(sceneToLoadInChallenge02);
    }
    public void SelectChallenge03()
    {
        SceneManager.LoadScene(sceneToLoadInChallenge03);
    }

}
