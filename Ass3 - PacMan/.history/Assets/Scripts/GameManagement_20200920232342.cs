using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
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

    }


    public void GameOver(bool isWin)
    {
        isGameStart = false;
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            
        }
    }
}

