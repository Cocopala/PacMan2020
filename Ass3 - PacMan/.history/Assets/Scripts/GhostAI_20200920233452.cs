using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAI : MonoBehaviour
{
    public float moveSpeed;
    private Vector3[] allPoints;
    private int currPoint;
    private SelfAnim animator;
    private int childPathIndex;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<SelfAnim>();
        InitialOnePath();
    }

    private void InitialOnePath()  // initial a path
    {
        allPoints = RandomPath.Instance.GetPath(out childPathIndex);
        allPoints[0].x = transform.position.x;   // the ghost starting follow the x dir. vertical up
        currPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManagement.Instance.isGameStart)
        {
            transform.position = Vector3.MoveTowards(transform.position, allPoints[currPoint], Time.deltaTime * moveSpeed);
            if(transform.position == allPoints[currPoint])
            {
            Vector3 prev = allPoints[currPoint];
                currPoint++;
                if(currPoint >= allPoints.Length)  // a path route end
                {
                    RandomPath.Instance.RecoverPath(childPathIndex); // when the path finished, back value to randompath
                    InitialOnePath(); 
                }
            Vector3 next = allPoints[currPoint];
            CalculateDir(prev, next);
            }
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player");
        GameManagement.Instance.GameOver(false);
    }
}
