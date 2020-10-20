using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ButtonMgr : MonoBehaviour
{
    private SoundMgr soundMgr;
    private Button button;

    void Start()
    {
        soundMgr = FindObjectOfType<SoundMgr>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        soundMgr.PlayOnce("click");
    }

}
