using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject sharkMove;
    public GameObject pengangMove;
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
            pauseState = false;
            Time.timeScale = 1;
        }
    }

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

    public void SharkMove()
    {
        pauseMenuUI.SetActive(false);
        sharkMove.SetActive(true);
    }
    public void PengangMove()
    {
        pauseMenuUI.SetActive(false);
        pengangMove.SetActive(true);
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
        sharkMove.SetActive(false);
        pengangMove.SetActive(false);
        capyMove.SetActive(false);
        kenMove.SetActive(false);
        vdoSample.SetActive(false);
    }

    void TurnOffAll()
    {
        pauseMenuUI.SetActive(false);
        sharkMove.SetActive(false);
        pengangMove.SetActive(false);
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
