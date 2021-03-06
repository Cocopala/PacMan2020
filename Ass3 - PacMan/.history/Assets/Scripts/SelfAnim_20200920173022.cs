﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfAnim : MonoBehaviour
{
    public enum Anim_Dir
    {
        Right = 0,
        Left,
        Up,
        Down
    }
    public int totalFrame;  // define how many frames in 1 animation
    public float AnimSpeed;
    public Sprite[] spriteArr;
    private SpriteRenderer spriteRender;
    private int currFrame = 0;
    private int startFrame;
    private int endFrame;

    // Start is called before the first frame update
    void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        currFrame = 0;
        startFrame = 0;
        endFrame = 3;
        StartCoroutine(PlayAnim());
    }

    IEnumerator PlayAnim()
    {
        while(true)
        {
            yield return new WaitForSeconds(AnimSpeed);
            spriteRender.sprite = spriteArr[currFrame];
            currFrame++;
            if(currFrame >= endFrame)
            {
                currFrame = startFrame;
            }
        }
    }

    public void ChangeDir(Anim_Dir _dir)
    {
        //encapsulate actions.
        startFrame = currFrame = (int)_dir * totalFrame;
        endFrame = startFrame + totalFrame;
        // switch(_dir)
        // {
        //     case Anim_Dir.Right:
        //         currFrame = 0;
        //         startFrame = 0;
        //         endFrame = 3;
        //         break;
        //     case Anim_Dir.Left:
        //         currFrame = 3;
        //         startFrame = 3;
        //         endFrame = 6;
        //         break;
        //     case Anim_Dir.Up:
        //         currFrame = 6;
        //         startFrame = 6;
        //         endFrame = 9;
        //         break;
        //     case Anim_Dir.Down:
        //         currFrame = 9;
        //         startFrame = 9;
        //         endFrame = 11;
        //         break;
        // }
    }
    // Update is called once per frame
    void Update()
    {
        // Direction control by user
        if(Input.GetKeyDown(KeyCode.RightArrow)) // right
        {
            ChangeDir(Anim_Dir.Right);
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow)) // left
        {
            ChangeDir(Anim_Dir.Left);
        }
        if(Input.GetKeyDown(KeyCode.UpArrow)) // up
        {
            ChangeDir(Anim_Dir.Up);
        }
        if(Input.GetKeyDown(KeyCode.DownArrow)) // down
        {
            ChangeDir(Anim_Dir.Down);
        }
    }
}
