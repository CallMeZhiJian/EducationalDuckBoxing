using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject settingScreen;
    private GameObject audioPage;
    private GameObject controlsPage;

    private void Awake()
    {
        settingScreen = GameObject.Find("SettingScreen");
        
        //inGameStuff = GameObject.Find("InGame");
        audioPage = GameObject.Find("AudioPage");
        controlsPage = GameObject.Find("ControlsPage");
    }

    private void Update()
    {            
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
    
}
