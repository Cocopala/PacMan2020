using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HighScores : MonoBehaviour
{
    private GameObject[] m_ScoresBoard;
    private GameObject[] m_TimeBoard;
    private GameObject m_Scores;
    private string[] m_ScoreString = new string[3];
    private string[] m_TimeString = new string[3];
    public static Dictionary<int, string> scores = new Dictionary<int, string>();
    void Start()
    {
        //m_Scores = GameObject.FindGameObjectWithTag("FinalScores");
        m_ScoresBoard = GameObject.FindGameObjectsWithTag("FinalScores");
        m_TimeBoard = GameObject.FindGameObjectsWithTag("FinalTime");

        int i = 0;
        var dicSort = from objDic in scores orderby objDic.Key descending select objDic;
        foreach (KeyValuePair<int, string> kvp in dicSort)
        {
            if (i == 3)
            {
                break;
            }

            Debug.Log(kvp.Key + "：" + kvp.Value);
            m_ScoreString[i] = kvp.Key.ToString();
            m_TimeString[i] = kvp.Value;
            i++;
        }

        i = 0;
        foreach (GameObject score in m_ScoresBoard)
        {
            if (i == 3)
            {
                break;
            }
            score.GetComponent<UnityEngine.UI.Text>().text = m_ScoreString[i];
            i++;
        }
        i = 0;
        foreach (GameObject time in m_TimeBoard)
        {
            if (i == 3)
            {
                break;
            }
            time.GetComponent<UnityEngine.UI.Text>().text = m_TimeString[i];
            i++;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
