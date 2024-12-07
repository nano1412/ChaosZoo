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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
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
}
