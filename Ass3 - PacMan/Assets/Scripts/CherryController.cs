using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
enum CherryStatus
{
    appear,
    disappear

};
enum CherryWalkingStatus
{
    walking,
    stopped
}
public class CherryController : MonoBehaviour
{

    private Vector2Int currentWalkingDirection = Vector2Int.left;

    private Vector2Int fromNode = new Vector2Int(26, -27);
    private Vector2Int homeNode = new Vector2Int(26, -27);
    public Vector2Int toNode = new Vector2Int(26, -27);
    private CherryStatus cherryState = CherryStatus.appear;
    private CherryWalkingStatus walkingState = CherryWalkingStatus.stopped;
    private SoundMgr soundMgr;
    public Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        //#walkingState = CherryWalkingStatus.walking;
        GameStatic.score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (cherryState == CherryStatus.disappear)
            return;
        if (walkingState == CherryWalkingStatus.walking)
        {
            Move();
            return;
        }

        UpdateToNode();
        walkingState = CherryWalkingStatus.walking;
    }

    void Spawn()
    { 
        gameObject.SetActive(true);
        transform.localPosition = new Vector3(homeNode.x, homeNode.y, transform.localPosition.z);
        walkingState = CherryWalkingStatus.walking;
        cherryState = CherryStatus.appear;
        currentWalkingDirection = Vector2Int.left;

    }

    void unSpawn()
    {
        gameObject.SetActive(false);
        walkingState = CherryWalkingStatus.stopped;
        cherryState = CherryStatus.disappear;
        int reProduceTime = UnityEngine.Random.Range(10,25);
        Invoke("Spawn", reProduceTime);
    }

    void Eaten()
    {
        unSpawn();
        GameStatic.score += 200;
        scoreText.text = "" + GameStatic.score;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // check if triggered by pacman
        //GameStatic.score += 200;
        // set to unactive
        if (collision.gameObject.tag == "pacman")
        {
            Debug.Log("eating cherry");
            Eaten();
        }
        Debug.Log("Something else touches my cherry");
    }

    void Move()
    {
        // 1. calculate next walking offset
        Vector2 lerp = new Vector2((float)(toNode.x - fromNode.x) / 20, (float)(toNode.y - fromNode.y) / 20f);

        transform.localPosition = new Vector2(transform.localPosition.x + lerp.x, transform.localPosition.y + lerp.y);
        // 2. if next walking offset is equal to toNode, change walking state to stop. 
        if (Math.Abs(transform.localPosition.x - (float)toNode.x) < 0.1 && Math.Abs(transform.localPosition.y - (float)toNode.y) < 0.1)
        {
            transform.localPosition = new Vector2(toNode.x, toNode.y);
            walkingState = CherryWalkingStatus.stopped;
            fromNode = new Vector2Int(toNode.x, toNode.y);
        }
    }

    void UpdateToNode()
    {
        if (walkingState == CherryWalkingStatus.walking)
            return;

        int x = -fromNode.y, y = fromNode.x;
        if(fromNode.x == 26 && fromNode.y == -27 && currentWalkingDirection == Vector2Int.down)
        {
            unSpawn();
            currentWalkingDirection = Vector2Int.left;
        }
        if (fromNode.x == 26 && fromNode.y == -1)
        {
            currentWalkingDirection = Vector2Int.down;
        }
        if (fromNode.x == 1 && fromNode.y == -1)
        {
            currentWalkingDirection = Vector2Int.right;
        }

        if (fromNode.x == 1 && fromNode.y == -27)
        {
            currentWalkingDirection = Vector2Int.up;
        }
        toNode = new Vector2Int(fromNode.x + currentWalkingDirection.x, fromNode.y + currentWalkingDirection.y);
        //Debug.Log("======= UpdateToNode=====");

        //Debug.Log("FromNode: " + fromNode.x + "," + fromNode.y);
        //Debug.Log("toNode: " + toNode.x + "," + toNode.y);
    }
}

/*
Create a script called “CherryController.cs” to implement the spawning and
movement of the bonus cherry sprite created in the previous assessment.
o The bonus cherry should spawn once every 30 seconds.
o It should be instantiated just outside of the camera view.
o It should move in a straight line, via linear lerping, from one side of the
screen to the other, passing through the center point of the level and
ignoring collisions with walls, ghosts, and pellets.
o If the cherry reaches the other side of the level, outside of camera
view, destroy it.
o See below for what to do if PacStudent collides with the cherry. 
 */