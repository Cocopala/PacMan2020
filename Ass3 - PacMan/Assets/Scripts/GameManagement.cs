using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    private float m_currentTime;
    private const float WAIT_RELOAD = 2F;
    private const float Super_Dot_Time = 5F;
    public static GameManagement Instance;
    private GameObject CountDownTimer;
    private GameObject GameOverLabel;
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
        m_currentTime = Time.time;
        CountDownTimer = GameObject.FindWithTag("GhostMoveCountDown");
        GameOverLabel = GameObject.FindWithTag("GameOver");
        GameOverLabel.SetActive(false);
        isGameOver = false;
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
        // SceneManager.LoadScene(0);  
        Invoke("ReloadScene", WAIT_RELOAD); // I cannot implement respawn effect. so follow the tutorial instruction doing a reload.
    }


    void ReloadScene()
    {
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
            if (m_currentTime >= 3 && m_currentTime <= 3.3)
            {
                CountDownTimer.GetComponent<UnityEngine.UI.Text>().text = "GO!";
            }
            else
            {
                Destroy(CountDownTimer);
            }
        }







        m_currentTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))  // Press space bar to start
        {
            GameStart();
        }
    }
}

