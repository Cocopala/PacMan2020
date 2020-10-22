using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    private float m_currentTime;
    private const float WAIT_RELOAD = 2F;
    private const float Super_Dot_Time = 5F;
    private GameObject CountDownTimer;
    private GameObject GameOverLabel;
    private GameObject ScoreBoard;
    private GameObject Timer;
    private Text m_timeText;
    private string tempTime;
    private int minute, second, milliScecond, hour;

    public static GameManagement Instance;
    public int Score;
    public bool isGameStart
    {
        get;
        private set;
    }
    private bool isGameOver;

    void Awake()  // initial
    {
        Instance = this;
        isGameStart = false;
    }
    void Start()
    {
        m_currentTime = 0;

        if (isStage2())
        {
            CountDownTimer = GameObject.FindGameObjectWithTag("GhostMoveCountDown");
            Score = 0;
            //ScoreBoard = GameObject.FindGameObjectWithTag("Score");
            Timer = GameObject.FindGameObjectWithTag("Timer");
            GameOverLabel = GameObject.FindGameObjectWithTag("GameOver");
            GameOverLabel.SetActive(false);
            isGameOver = false;
        }
        if (isStage1())
        {
            CountDownTimer = GameObject.FindGameObjectWithTag("GhostMoveCountDown");
            Score = 0;
            ScoreBoard = GameObject.FindGameObjectWithTag("Score");
            Timer = GameObject.FindGameObjectWithTag("Timer");
            GameOverLabel = GameObject.FindGameObjectWithTag("GameOver");
            GameOverLabel.SetActive(false);
            isGameOver = false;
        }

    }

    public void EatenDot(bool isSuper)
    {

    }


    public void GameStart()
    {
        isGameStart = true;
    }


    public void GameOver(bool isWin)
    {
        isGameStart = false;
        GameOverLabel.SetActive(true); 
        HighScores.scores.Add(Score, tempTime);
        // SceneManager.LoadScene(0);  
        Invoke("ReloadScene", WAIT_RELOAD); // I cannot implement respawn effect. so follow the tutorial instruction doing a reload.
    }

    private bool isStage1()
    {
        if (SceneManager.GetActiveScene().name == "Level 1")
        {
            return true;
        }
        return false;
    }
    private bool isStage2()
    {
        if (SceneManager.GetActiveScene().name == "Level 2")
        {
            return true;
        }
        return false;
    }

    void ReloadScene()
    {
        isGameStart = false;
        SceneManager.LoadScene(0);
    }

    // private IEnumerator GenSuperDot()  // generate super dot
    // {
    //     yield return new WaitForSecond(Super_Dot_Time);
    //     Pacdot[] allDots = GameObject.FindObjectsOfType<Pacdot>();
    //     Pacdot superDot = allDots[RandomPath.Range(0, allDots.Length)];
    //     superDot.MakeDotSuper();
    // }

    private void Update()
    {
        //count down for 3 second
        if (isStage1())
        {
            if (!isGameStart)
            {
                if (m_currentTime >= 1)
                {
                    CountDownTimer.GetComponent<UnityEngine.UI.Text>().text = "2";
                    if (m_currentTime >= 2)
                    {
                        CountDownTimer.GetComponent<UnityEngine.UI.Text>().text = "1";
                        if (m_currentTime >= 3)
                        {
                            GameStart();
                            CountDownTimer.GetComponent<UnityEngine.UI.Text>().text = "GO!";
                        }
                    }
                }
            }

            else
            {
                //Timer.GetComponent<UnityEngine.UI.Text>().text = m_currentTime.ToString();

                //DateTime Time1 = DateTime.Now.ToLocalTime();
                //m_timeText.text = Time1.ToString("yyyy-MM-dd HH:mm:ss");

                hour = (int)m_currentTime / 3600;
                minute = (int)(m_currentTime - hour * 3600) / 60;
                second = (int)(m_currentTime - hour * 3600 - minute * 60);
                milliScecond = (int)((m_currentTime - (int)m_currentTime) * 10);
                Timer.GetComponent<UnityEngine.UI.Text>().text = string.Format("{0:D2}:{1:D2}:{2:D2}", minute, second, (int)milliScecond);
                tempTime = string.Format("{0:D2}:{1:D2}:{2:D2}", minute, second, (int)milliScecond);

                if (m_currentTime >= 3 && m_currentTime <= 3.3)
                {
                    CountDownTimer.GetComponent<UnityEngine.UI.Text>().text = "GO!";
                }
                else
                {
                    ScoreBoard.GetComponent<UnityEngine.UI.Text>().text = Score.ToString();
                    CountDownTimer.SetActive(false);
                }
            }
        }

        if (isStage2())
        {
            if (!isGameStart)
            {
                if (m_currentTime >= 1)
                {
                    CountDownTimer.GetComponent<UnityEngine.UI.Text>().text = "2";
                    if (m_currentTime >= 2)
                    {
                        CountDownTimer.GetComponent<UnityEngine.UI.Text>().text = "1";
                        if (m_currentTime >= 3)
                        {
                            GameStart();
                            CountDownTimer.GetComponent<UnityEngine.UI.Text>().text = "GO!";
                        }
                    }
                }
            }

            else
            {
                hour = (int)m_currentTime / 3600;
                minute = (int)(m_currentTime - hour * 3600) / 60;
                second = (int)(m_currentTime - hour * 3600 - minute * 60);
                milliScecond = (int)((m_currentTime - (int)m_currentTime) * 10);
                Timer.GetComponent<UnityEngine.UI.Text>().text = string.Format("{0:D2}:{1:D2}:{2:D2}", minute, second, (int)milliScecond);

                if (m_currentTime >= 3 && m_currentTime <= 3.3)
                {
                    CountDownTimer.GetComponent<UnityEngine.UI.Text>().text = "GO!";
                }
                else
                {
                    CountDownTimer.SetActive(false);
                }
            }
        }

        m_currentTime += Time.deltaTime;


    }
}

