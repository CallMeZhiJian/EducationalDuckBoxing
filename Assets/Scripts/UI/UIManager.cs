using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject settingScreen;
    private GameObject audioPage;
    private GameObject controlsPage;

    [SerializeField] private Slider _MusicSlider;
    [SerializeField] private Slider _SFXSlider;

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

        _MusicSlider.value = AudioManager.instance._BGMSource.volume;
        _SFXSlider.value = AudioManager.instance._SFXSource.volume;
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
    public void AdjustMusicVolume()
    {
        AudioManager.instance._BGMSource.volume = _MusicSlider.value;
    }

    public void AdjustSFXVolume()
    {
        AudioManager.instance._SFXSource.volume = _SFXSlider.value;
    }
}
