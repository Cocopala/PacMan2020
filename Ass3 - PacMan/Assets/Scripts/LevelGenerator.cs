using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject empty;
    public GameObject outsideCorner;
    public GameObject outsideWall;
    public GameObject insideCorner;
    public GameObject insideWall;
    public GameObject standardPellet;
    public GameObject powerPellet;
    public GameObject tJunction;
    private int[,] quadrant =
  {
 {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
 {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
 {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
 {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
 {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
 {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
 {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
 {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
 {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
 {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
 {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
 {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
 {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
 {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
 {0,0,0,0,0,0,5,0,0,0,4,0,0,0},
 };
    private int[,] levelMap;

    // Start is called before the first frame update
    void Start()
    {
        initMartix(quadrant);

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void initMartix(int[,] arr2d)
    {
        int quadrant_r = arr2d.GetLength(0);
        int quadrant_c = arr2d.GetLength(1);
        int levelMap_r = quadrant_r * 2 - 1;
        int levelMap_c = quadrant_c * 2;
        levelMap = new int[levelMap_r,levelMap_c];
        for (int r=0; r<levelMap_r; r++)
        {
            for (int c=0; c<levelMap_c; c++)
            {
                if (r <= quadrant_r && c <= quadrant_c )
                {
                    levelMap[r,c] = arr2d[r,c];
                    initGrid(levelMap[r, c], r, c);
                    
                }
                if (r <= quadrant_r && c >= quadrant_c)
                {
                    levelMap[r,c] = arr2d[r, quadrant_c - c];
                    initGrid(levelMap[r, c], r, c);
                }
                if (r > quadrant_r && c <= quadrant_c)
                {
                    levelMap[r,c] = arr2d[r - quadrant_r - 1, c];
                    initGrid(levelMap[r, c], r, c);
                }
                if (r > quadrant_r && c > quadrant_c)
                {
                    levelMap[r, c] = arr2d[r - quadrant_r - 1, quadrant_c - c];
                    initGrid(levelMap[r, c], r, c);
                }

            }
        }
    }

    public void initGrid(int value,int x, int y)
    {
        if (value == 0)
        {
            Instantiate(empty, new Vector2(x, y), Quaternion.identity);
        }
        if (value == 1)
        {
            Instantiate(outsideCorner, new Vector2(x, y), Quaternion.identity);
        }
        if (value == 2)
        {
            Instantiate(outsideWall, new Vector2(x, y), Quaternion.identity);
        }
        if (value == 3)
        {
            Instantiate(insideCorner, new Vector2(x, y), Quaternion.identity);
        }
        if (value == 4)
        {
            Instantiate(insideWall, new Vector2(x, y), Quaternion.identity);
        }
        if (value == 5)
        {
            Instantiate(standardPellet, new Vector2(x, y), Quaternion.identity);
        }
        if (value == 6)
        {
            Instantiate(powerPellet, new Vector2(x, y), Quaternion.identity);
        }
        if (value == 7)
        {
            Instantiate(tJunction, new Vector2(x, y), Quaternion.identity);
        }

    }
}
