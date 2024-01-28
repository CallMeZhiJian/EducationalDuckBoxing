using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUI : UIManager
{
    private GameObject pauseScreen;

    private bool isPaused;

    private void Start()
    {
        pauseScreen = GameObject.Find("PauseScreen");

        isPaused = false;
        if (pauseScreen != null)
        {
            pauseScreen.SetActive(false);
        }

        AudioManager.instance.PlayBGM();
    }

    private void Update()
    {
        if (pauseScreen != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UnpausePauseGame();
            }
        }
    }

    public void UnpausePauseGame()
    {
        if (isPaused)
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
        }
        else
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
        }
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainTitle");
    }
}
