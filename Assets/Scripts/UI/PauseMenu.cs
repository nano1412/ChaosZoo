using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject sharkMoveKB;
    public GameObject sharkMoveJS;
    public GameObject pengangMoveKB;
    public GameObject pengangMoveJS;
    public GameObject capyMove;
    public GameObject kenMove;
    public GameObject vdoSample;
    public bool pauseState;

    public VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        pauseState = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseState == false)
        {
            pauseMenuUI.SetActive(true);
            pauseState = true;
            Time.timeScale = 0;
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && pauseState == true)
        {
            TurnOffAll();
            pauseMenuUI.SetActive(false);
            pauseState = false;
            Time.timeScale = 1;
        }
    }

    // General Function

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        pauseState = false;
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        pauseState = false;
        SceneManager.LoadScene("Mainmenu");
    }

    // Open Move List UI

    public void SharkMove()
    {
        pauseMenuUI.SetActive(false);
        sharkMoveKB.SetActive(true);
    }
    public void PengangMove()
    {
        pauseMenuUI.SetActive(false);
        pengangMoveKB.SetActive(true);
    }

    public void CapyMove()
    {
        pauseMenuUI.SetActive(false);
        capyMove.SetActive(true);
    }

    public void KenMove()
    {
        pauseMenuUI.SetActive(false);
        kenMove.SetActive(true);
    }

    // Switch Move List UI

    public void ToSharkJS()
    {
        sharkMoveKB.SetActive(false);
        sharkMoveJS.SetActive(true);
    }

    public void ToSharkKB()
    {
        sharkMoveKB.SetActive(true);
        sharkMoveJS.SetActive(false);
    }

    public void ToPengangJS()
    {
        pengangMoveKB.SetActive(false);
        pengangMoveJS.SetActive(true);
    }

    public void ToPengangKB()
    {
        pengangMoveKB.SetActive(true);
        pengangMoveJS.SetActive(false);
    }

    // Video Handle

    public void VDOSample()
    {
        pauseMenuUI.SetActive(false);
        vdoSample.SetActive(true);
        videoPlayer.Stop();
        videoPlayer.Play();
    }

    public void BackToPause()
    {
        pauseMenuUI.SetActive(true);
        TurnOffAll();
    }

    void TurnOffAll()
    {
        sharkMoveKB.SetActive(false);
        pengangMoveKB.SetActive(false);
        sharkMoveJS.SetActive(false);
        pengangMoveJS.SetActive(false);
        capyMove.SetActive(false);
        kenMove.SetActive(false);
        vdoSample.SetActive(false);
    }

    public void OnVideoReplayClicked()
    {
        if(videoPlayer)
        {
            videoPlayer.Stop();
            videoPlayer.Play();
        }
    }
}
