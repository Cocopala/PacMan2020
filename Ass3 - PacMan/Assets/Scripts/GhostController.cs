using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
enum GhostState
{
    normal,
    scared,
    recovering,
    dead,

};
enum GhostWalkingState
{
    walking,
    makeDecision
}

public class GhostController : MonoBehaviour
{
    public int[,] levelMap = GameStatic.levelMap;
    private Vector2Int currentWalkingDirection = Vector2Int.up;
    public Vector2Int homeNode = new Vector2Int(10, -14);  // The initial position of the pacman
    private Vector2Int fromNode;
    public Vector2Int toNode = new Vector2Int(10, -15);
    private GhostState mentalState = GhostState.normal;
    private int lerpValue = 100;
    private float startTime;
    //Audios
    public Text scoreText;
    public GameStateMachine gameStateMachine;
    private GhostWalkingState walkingState = GhostWalkingState.makeDecision;
    public int ghostNumber = 1;
    public GameObject pacMan;
    private SelfAnim animator;

    // Start is called before the first frame update
    void Start()
    {
        gameStateMachine = FindObjectOfType<GameStateMachine>();
        fromNode = new Vector2Int(homeNode.x, homeNode.y);

        animator = GetComponent<SelfAnim>();
        walkingState = GhostWalkingState.walking;
        animator.ChangeDir(SelfAnim.Anim_Dir.Up);

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagement.Instance.isGameStart)
        {
            if (walkingState == GhostWalkingState.walking)
            {
                Move();
                return;
            }

            // if ghost arrived at a node, make next node decision
            UpdateToNode();
            walkingState = GhostWalkingState.walking;
        }

    }

    private void Move()
    {
        // 1. calculate next walking offset
        Vector2 lerp = new Vector2((float)(toNode.x - fromNode.x) / 20, (float)(toNode.y - fromNode.y) / 20f);

        transform.localPosition = new Vector2(transform.localPosition.x + lerp.x, transform.localPosition.y + lerp.y);
        // 2. if next walking offset is equal to toNode, change walking state to stop. 
        if (Math.Abs(transform.localPosition.x - (float)toNode.x) < 0.1 && Math.Abs(transform.localPosition.y - (float)toNode.y) < 0.1)
        {
            transform.localPosition = new Vector2(toNode.x, toNode.y);
            walkingState = GhostWalkingState.makeDecision;
            fromNode = new Vector2Int(toNode.x, toNode.y);
        }
    }

    // update the  toNode, according to neighbor node, current walking direction, last input 
    void UpdateToNode()
    {
        // do not update anything when walking towards target node
        if (walkingState == GhostWalkingState.walking)
            return;

        int x = -fromNode.y, y = fromNode.x;
        //Check if fromNode is in tunnel end
        if (y == 0 && x == 14 && currentWalkingDirection == Vector2.left)
        {
            fromNode = new Vector2Int(0, -14);
            toNode = new Vector2Int(1, -14);
            currentWalkingDirection = Vector2Int.right;
            //#transform.localPosition = new Vector3(27, -14, transform.localPosition.z);
            return;
        }
        if (y == 27 && x == 14 && currentWalkingDirection == Vector2Int.right)
        {
            fromNode = new Vector2Int(27, -14);
            toNode = new Vector2Int(26, -14);
            currentWalkingDirection = Vector2Int.left;
            return;
        }

        bool left = isWalkable(levelMap[x, y - 1]),
            right = isWalkable(levelMap[x, y + 1]),
            up = isWalkable(levelMap[x - 1, y]),
            down = isWalkable(levelMap[x + 1, y]);
        //if ((lastInput == Vector2.left && left)
        //    || lastInput == Vector2.right && right
        //    || lastInput == Vector2.up && up
        //    || lastInput == Vector2.down && down
        //    )
        //{
        //    currentWalkingDirection = lastInput;
        //    toNode = new Vector2Int(fromNode.x + lastInput.x, fromNode.y + lastInput.y);
        //    changePackmanFace(lastInput);
        //}
        if ((currentWalkingDirection == Vector2.left && left && !up && !down)
          || currentWalkingDirection == Vector2.right && right && !up && !down
          || currentWalkingDirection == Vector2.up && up && !left && !right
          || currentWalkingDirection == Vector2.down && down && !left && !right
          )
        {
            toNode = new Vector2Int(fromNode.x + currentWalkingDirection.x, fromNode.y + currentWalkingDirection.y);
        }
        else
        {
            Vector2Int newDirection = calcRandomDirection(currentWalkingDirection, up, down, left, right, ghostNumber);
            currentWalkingDirection = newDirection;
            toNode = new Vector2Int(fromNode.x + newDirection.x, fromNode.y + newDirection.y);
            changeGhostAnim(newDirection);
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

    Vector2Int calcRandomDirection(Vector2Int currentWalkingDirection, bool up, bool down, bool left, bool right, int ghostNumber)
    {
        Dictionary<int, Vector2Int> randomLocation = new Dictionary<int, Vector2Int>();
        int index = 1;
        if (up && currentWalkingDirection != Vector2Int.down)
            randomLocation.Add(index++, Vector2Int.up);
        if (down && currentWalkingDirection != Vector2Int.up)
            randomLocation.Add(index++, Vector2Int.down);
        if (left && currentWalkingDirection != Vector2Int.right)
            randomLocation.Add(index++, Vector2Int.left);
        if (right && currentWalkingDirection != Vector2Int.left)
            randomLocation.Add(index++, Vector2Int.right);
        System.Random rand = new System.Random();
        return randomLocation[rand.Next(1, index)];
    }

    bool isWalkable(int value)
    {
        if (value == 0 || value == 5 || value == 6)
            return true;
        return false;
    }

    // Set animation of ghost
    void changeGhostAnim(Vector2 direction)
    {

        if (direction == Vector2.left)
        {
            animator.ChangeDir(SelfAnim.Anim_Dir.Left);
        }
        else if (direction == Vector2.right)
        {
            animator.ChangeDir(SelfAnim.Anim_Dir.Right);
        }
        else if (direction == Vector2.up)
        {
            animator.ChangeDir(SelfAnim.Anim_Dir.Up);
        }
        else if (direction == Vector2.down)
        {
            animator.ChangeDir(SelfAnim.Anim_Dir.Down);
        }
    }

}
