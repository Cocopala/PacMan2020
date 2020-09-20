using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfAnim : MonoBehaviour
{
    public float AnimSpeed;
    public Sprite[] spriteArr;
    private SpriteRenderer spriteRender;
    private int currFrame = 0;

    // Start is called before the first frame update
    void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        StartCoroutine(PlayAnim());
    }

    IEnumerable PlayAnim()
    {
        while(true)
        {
            yield return new WaitForSeconds(AnimSpeed);
            spriteRender.sprite = spriteArr[currFrame];
            currFrame++;
            if(currFrame >= spriteArr.Length)
            {
                currFrame = 0;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
