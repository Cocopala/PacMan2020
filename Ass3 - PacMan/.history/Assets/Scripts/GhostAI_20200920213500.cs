using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAI : MonoBehaviour
{
    public float moveSpeed;
    private Vector3[] allPoints;
    public Transform pathParent;
    private int currPoint;
    private SelfAnim animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<SelfAnim>();
        allPoints = new Vector3[pathParent.childCount];
        for (int i = 0; i < pathParent.childCount; i++)
        {
            allPoints[i] = pathParent.GetChild(i).position;
        }
        currPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, allPoints[currPoint], Time.deltaTime * moveSpeed);
        if(transform.position == allPoints[currPoint])
        Vector3 prev = allPoints[currPoint];
            currPoint++;
            if(currPoint >= allPoints.Length)
            {
                currPoint = 0;
            }
        Vector3 next = allPoints[currPoint];
        CalculateDir(prev, next);
    }

    private void CalculateDir(Vector3 prev, Vector3 next)  // the ghost move direction
    {
        float xx = next.x - prev.x;
        float yy = next.y - prev.y;
        if(Mathf.Abs(yy) > Mathf.Abs(xx))
        {
            animator.ChangeDir(yy > 0 ? SelfAnim.Anim_Dir.Up : SelfAnim.Anim_Dir.Down);
        }
        else
        {
            animator.ChangeDir(xx > 0 ? SelfAnim.Anim_Dir.Right : SelfAnim.Anim_Dir.Left);
        }
    }
}
