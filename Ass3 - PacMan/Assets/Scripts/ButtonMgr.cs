using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ButtonMgr : MonoBehaviour
{
    public AudioClip clickAudio;
    public AudioSource audioSource;
    private Button button;

    void Start()
    {
        audioSource = GameObject.Find("Audio").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        audioSource.PlayOneShot(clickAudio);
    }


}
