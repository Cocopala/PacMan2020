using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    public static GameManagement Instance;
    public bool isGameStart
    {
        get;
        private set;
    }
    private const float WAIT_RELOAD = 2F;

    void Awake()  // initial
    {
        Instance = this;
        isGameStart = false;
    }


    public void EatenDot()
    {

    }


    public void GameStart()
    {
        isGameStart = true;
    }


    public void GameOver(bool isWin) 
    {
        isGameStart = false;
        SceneManager.LoadScene(0);  // I cannot implement respawn effect. so follow the tutorial instruction doing a reload.
        Invoke("ReloadScene", WAIT_RELOAD);
    }


    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))  // Press space bar to start
        {
            GameStart();
        }
    }
}

