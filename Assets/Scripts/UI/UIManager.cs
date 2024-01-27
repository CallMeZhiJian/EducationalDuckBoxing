using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject settingScreen;
    private GameObject audioPage;
    private GameObject controlsPage;

    //private GameObject inGameStuff;
    //private GameObject pauseScreen;

    //private bool isPaused;

    private void Start()
    {
        settingScreen = GameObject.Find("SettingScreen");
        //pauseScreen = GameObject.Find("PauseScreen");
        //inGameStuff = GameObject.Find("InGame");
        audioPage = GameObject.Find("AudioPage");
        controlsPage = GameObject.Find("ControlsPage");

        //isPaused = false;
        //if(pauseScreen != null)
        //{
        //    pauseScreen.SetActive(false);
        //}
    }

    private void Update()
    {
        //if(inGameStuff != null)
        //{
        //    if (SceneManager.GetActiveScene().name != "MainTitle")
        //    {
        //        inGameStuff.SetActive(true);
        //    }
        //    else
        //    {
        //        inGameStuff.SetActive(false);
        //    }
        //}
            
        //Setting
        if(settingScreen != null)
        {
            if (settingScreen.GetComponent<Animator>().GetBool("onSetting"))
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (audioPage.activeInHierarchy)
                    {
                        ControlsPage();
                    }
                    else if (controlsPage.activeInHierarchy)
                    {
                        AudioPage();
                    }
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    if (audioPage.activeInHierarchy)
                    {
                        ControlsPage();
                    }
                    else if (controlsPage.activeInHierarchy)
                    {
                        AudioPage();
                    }
                }
            }
        }   
        
        //if(pauseScreen != null)
        //{
        //    if (Input.GetKeyDown(KeyCode.Escape))
        //    {
        //        UnpausePauseGame();
        //    }
        //}  
    }

    public void OnOffSetting()
    {
        Animator anim = settingScreen.GetComponent<Animator>();

        if (anim != null)
        {
            bool currBool = anim.GetBool("onSetting");

            anim.SetBool("onSetting", !currBool);
        }

        AudioPage();
    }

    public void AudioPage()
    {
        controlsPage.SetActive(false);
        audioPage.SetActive(true);
    }

    public void ControlsPage()
    {
        controlsPage.SetActive(true);
        audioPage.SetActive(false);
    }

    //Audio


    //InGame Usage
    //public void UnpausePauseGame()
    //{
    //    if (isPaused)
    //    {
    //        pauseScreen.SetActive(false);
    //        Time.timeScale = 1;
    //        isPaused = false;
    //    }
    //    else
    //    {
    //        pauseScreen.SetActive(true);
    //        Time.timeScale = 0;
    //        isPaused = true;
    //    }
    //}

    //public void BackToMainMenu()
    //{
    //    Time.timeScale = 1;
    //    SceneManager.LoadScene("MainTitle");
    //}
}
