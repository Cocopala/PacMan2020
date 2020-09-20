using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    private const float WAIT_RELOAD = 2F;
    public static GameManagement Instance;
    public bool isGameStart
    {
        get;
        private set;
    }

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
        // SceneManager.LoadScene(0);  
        Invoke("ReloadScene", WAIT_RELOAD); // I cannot implement respawn effect. so follow the tutorial instruction doing a reload.
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

