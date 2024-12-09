using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject sharkMove;
    public GameObject pengangMove;
    public bool pauseState;

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

    public void BackToPause()
    {
        pauseMenuUI.SetActive(true);
        sharkMove.SetActive(false);
        pengangMove.SetActive(false);
    }

    void TurnOffAll()
    {
        pauseMenuUI.SetActive(false);
        sharkMove.SetActive(false);
        pengangMove.SetActive(false);
    }
}
