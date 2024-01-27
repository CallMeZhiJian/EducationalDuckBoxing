using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUI : UIManager
{
    [SerializeField] private Transform[] movePos;
    private GameObject duckHead;

    private void Start()
    {
        duckHead = GameObject.Find("DuckHead");
    }

    public void StartGame()
    {  
        duckHead.transform.position = movePos[0].position;
        SceneManager.LoadScene("GameScene");
    }

    public void MainSetting()
    {
        duckHead.transform.position = movePos[1].position;
        OnOffSetting();
    }

    public void QuitGame()
    {
        duckHead.transform.position = movePos[2].position;
        Application.Quit();
    }
}
