using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfAnim : MonoBehaviour
{
    public enum Anim_Dir   // enum how many directions in the game
    {
        Right = 0,
        Left,
        Up,
        Down
    }
    public int totalFrame;  // define how many frames in 1 animation, in this case, it's 3 frame in 1
    public float AnimSpeed;
    public Sprite[] spriteArr;
    private SpriteRenderer spriteRender;
    private int currFrame = 0;
    private int startFrame;
    private int endFrame;
    private Anim_Dir currDir; // when the user hold the direction key, the animation will paused.

    // Start is called before the first frame update
    void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        ChangeDir(Anim_Dir.Right);
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

    public void ChangeDir(Anim_Dir _dir)   // direction control
    {
        if(currDir == _dir)  // to avoid holding dir key that animation paused
        {
            return;
        }
        else
        {
            currDir = _dir;
            //encapsulate actions.
            startFrame = currFrame = (int)_dir * totalFrame;
            endFrame = startFrame + totalFrame;
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
