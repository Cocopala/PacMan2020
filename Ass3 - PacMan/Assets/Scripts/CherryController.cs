using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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