using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAI : MonoBehaviour
{
    public float moveSpeed;
    private Vector3[] allPoints;
    public Transform pathParent;
    private int currPoint;
    // Start is called before the first frame update
    void Start()
    {
        allPoints = new Vector3[pathParent.childCount];

        for (int i = 0; i < pathParent.childCount; i++)
        {
            allPoints[i] = pathParent.GetChild(i).position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, allPoints[currPoint], Time.deltaTime * moveSpeed);
        if(transform.position == allPoints[currPoint])
            currPoint++;
            if(currPoint >= allPoints.Length)
            {
                currPoint = 0;
            }
    }
}
