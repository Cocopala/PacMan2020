using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum MentalState
{
    normal,
    recovering,
    scared,
    gameover
};

public class GameStateMachine : MonoBehaviour
{
    public MentalState mentalState = MentalState.normal;
    private SoundMgr soundMgr;
    // Start is called before the first frame update
    void Start()
    {
        mentalState = MentalState.normal;
        soundMgr = FindObjectOfType<SoundMgr>();

        if (SceneManager.GetActiveScene().name == "Level 2") changeToNormalState();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeToNormalState()
    {

        mentalState = MentalState.scared;
        soundMgr.StopPlay("Scared");
        soundMgr.Play("Normal");
    }

    public void changeToScaredState()
    {
        mentalState = MentalState.scared;
        soundMgr.StopPlay("Normal");   
        soundMgr.Play("Scared");
    }
}
