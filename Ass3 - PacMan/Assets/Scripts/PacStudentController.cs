using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

enum WalkingState
{
    walking,
    stopped,
};

public class PacStudentController : MonoBehaviour
{
    public GameObject pacStudent;
    public int[,] levelMap = GameStatic.levelMap;
    private Vector2Int lastInput = Vector2Int.zero;
    private Vector2Int currentWalkingDirection = Vector2Int.zero;
    private Vector2Int fromNode = new Vector2Int(1, -1);  // The initial position of the pacman
    private Vector2Int toNode = new Vector2Int(1, -1);
    private WalkingState walkingState = WalkingState.stopped;
    private int lerpValue = 100;
    private float startTime;
    private bool hitWallHappened = false;
    //Audios
    private SoundMgr soundMgr;
    public Text scoreText;
    public GameStateMachine gameStateMachine;


    void Start()
    {
        soundMgr = FindObjectOfType<SoundMgr>();
        gameStateMachine = FindObjectOfType<GameStateMachine>();
        EatNode(fromNode.x, fromNode.y, false);
        GameStatic.score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GetLastInput();

        // 1. if pacman is still walking from one node to next, then continue walking towards the toNode
        if (walkingState == WalkingState.walking)
        {
            Move();
            hitWallHappened = false;
            return;
        }

        // 2. if packman is arrived at next node or stopped
        // update the  toNode, according to neighbor node, current walking direction, last input
        
        UpdateToNode();

        // 2.1 if pacman is stopped and nowhere to go ,just keep it there, until toNode and fromNode is different
        if (fromNode == toNode)
        {
            walkingState = WalkingState.stopped;
        }
        else
        {
            walkingState = WalkingState.walking;
        }

    }

    void GetLastInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            lastInput = Vector2Int.left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            lastInput = Vector2Int.right;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            lastInput = Vector2Int.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            lastInput = Vector2Int.down;
        }

    }



    private void Move()
    {
        // for test only

        // 1. calculate next walking offset
        Vector2 lerp = new Vector2((float)(toNode.x - fromNode.x) / 20, (float)(toNode.y - fromNode.y) / 20f);

        pacStudent.transform.localPosition = new Vector2(pacStudent.transform.localPosition.x + lerp.x, pacStudent.transform.localPosition.y + lerp.y);
        // 2. if next walking offset is equal to toNode, change walking state to stop. 
        if (Math.Abs(pacStudent.transform.localPosition.x - (float)toNode.x) < 0.1 && Math.Abs(pacStudent.transform.localPosition.y - (float)toNode.y) < 0.1)
        {
            pacStudent.transform.localPosition = new Vector2(toNode.x, toNode.y);
            walkingState = WalkingState.stopped;
            fromNode = toNode;

            EatNode(fromNode.x, fromNode.y, levelMap[-fromNode.y, fromNode.x] == 6);
        }
    }


    void EatNode(int xx, int yy, bool isPower)
    {

        int x = -yy, y = xx;
        GameObject pellet = GameStatic.gameObjectsMap[x, y];
        if(pellet != null)
        {   if(pellet.active)
            {
                GameStatic.score += 10;
                scoreText.text = "" + GameStatic.score;
                soundMgr.PlayOnce(isPower ? "dotEatenBonus" : "dotEaten");
                if (isPower)
                {
                    gameStateMachine.changeToScaredState();
                } else
                {

                }
            } else
            {
                soundMgr.PlayOnce("dotMoving");
            }
            pellet.SetActive(false);
        }
    }

    // update the  toNode, according to neighbor node, current walking direction, last input 
    void UpdateToNode()
    {
        // do not update anything when walking towards target node
        if (walkingState == WalkingState.walking)
            return;

        int x = -fromNode.y, y = fromNode.x;
        //Check if fromNode is in tunnel end
        if (y == 0 && x == 14 && currentWalkingDirection == Vector2.left)
        {
            fromNode = new Vector2Int(27, -14);
            toNode = new Vector2Int(26, -14);
            pacStudent.transform.localPosition = new Vector3(27, -14, pacStudent.transform.localPosition.z);


            return;
        }
        if (y==27 && x == 14 && currentWalkingDirection == Vector2.right)
        {
            fromNode = new Vector2Int(0, -14);
            toNode = new Vector2Int(1, -14);
            pacStudent.transform.localPosition = new Vector3(0, -14, pacStudent.transform.localPosition.z);
            return;
        }

        bool left = isWalkable(levelMap[x, y - 1]),
            right = isWalkable(levelMap[x, y + 1]),
            up = isWalkable(levelMap[x - 1, y]),
            down = isWalkable(levelMap[x + 1, y]);
        if ((lastInput == Vector2.left && left)
            || lastInput == Vector2.right && right
            || lastInput == Vector2.up && up
            || lastInput == Vector2.down && down
            )
        {
            currentWalkingDirection = lastInput;
            toNode = new Vector2Int(fromNode.x + lastInput.x, fromNode.y + lastInput.y);
            changePackmanFace(lastInput);
        }
        else if ((currentWalkingDirection == Vector2.left && left)
          || currentWalkingDirection == Vector2.right && right
          || currentWalkingDirection == Vector2.up && up
          || currentWalkingDirection == Vector2.down && down
          )
        {
            toNode = new Vector2Int(fromNode.x + currentWalkingDirection.x, fromNode.y + currentWalkingDirection.y);
        }
        else
        {
            if (hitWallHappened == false)
            {
                Debug.Log("hit a wall!");
                hitWallEvent();
            }
            hitWallHappened = true;
            walkingState = WalkingState.stopped;
        }

        

        /** 
        Debug.Log("======= UpdateToNode=====");

        Debug.Log("FromNode: " + fromNode.x + "," + fromNode.y);
        Debug.Log("toNode: " + toNode.x + "," + toNode.y);
        Debug.Log("left:" + levelMap[x, y - 1] + left + ", right:" + levelMap[x, y + 1] + right + ", up:" + levelMap[x - 1, y] + up + ", down:" + levelMap[x + 1, y] + down);
        Debug.Log("walkingState:" + walkingState);
        Debug.Log("lastInput:" + lastInput);
        Debug.Log("currentWalkingDirection:" + currentWalkingDirection);
        */
    }


    bool isWalkable(int value)
    {
        if (value == 0 || value == 5 || value == 6)
            return true;
        return false;
    }

    void changePackmanFace(Vector2 direction)
    {
        if(direction == Vector2.left)
        {
            pacStudent.transform.localRotation = Quaternion.Euler(new Vector3(0,180f,0));
        } else if (direction == Vector2.right)
        {
            pacStudent.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else if (direction == Vector2.up)
        {
            pacStudent.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90f));
        }
        else if (direction == Vector2.down)
        {
            pacStudent.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -90f));
        }
    }

    public void hitWallEvent()
    {
        // 1. play sound
        soundMgr.PlayOnce("hitWall");
        // 2. change walking state to stop
        // 3. play particle hit
    }

}
