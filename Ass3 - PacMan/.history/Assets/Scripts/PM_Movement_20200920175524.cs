using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PM_Movement : MonoBehaviour
{
    public float moveSpeed;  // the moving speed of Pacman
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // get keyboard input
        // Because the character can move in one direction in the same time,
        // so change the other directions as 'else'
        if(Input.GetKey(KeyCode.RightArrow)) // right
        {
            Vector2 dest = rb.position + Vector2.right * moveSpeed;
            rb.MovePosition(dest);
            //ChangeDir(Anim_Dir.Right);
        }
        else if(Input.GetKey(KeyCode.LeftArrow)) // left
        {
            Vector2 dest = rb.position + Vector2.left * moveSpeed;
            rb.MovePosition(dest);
            //ChangeDir(Anim_Dir.Left);
        }
        else if(Input.GetKey(KeyCode.UpArrow)) // up
        {
            Vector2 dest = rb.position + Vector2.up * moveSpeed;
            rb.MovePosition(dest);
            //ChangeDir(Anim_Dir.Up);
        }
        else if(Input.GetKey(KeyCode.DownArrow)) // down
        {
            Vector2 dest = rb.position + Vector2.down * moveSpeed;
            rb.MovePosition(dest);
            //ChangeDir(Anim_Dir.Down);
        }
    }
}
