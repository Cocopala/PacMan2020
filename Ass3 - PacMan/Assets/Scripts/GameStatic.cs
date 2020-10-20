using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatic : MonoBehaviour
{
    public static int[,] quadrant =
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
    

    public static int[,] levelMap = GameStatic.initLevelMap(quadrant);
    public static GameObject[,] gameObjectsMap =  new GameObject[quadrant.GetLength(0) * 2 - 1, quadrant.GetLength(1) * 2];
    public static int[,] initLevelMap(int[,] arr2d)
    {
        int quadrant_r = arr2d.GetLength(0);
        int quadrant_c = arr2d.GetLength(1);
        int levelMap_r = quadrant_r * 2 - 1;    //29
        int levelMap_c = quadrant_c * 2;        //28
        Debug.Log(levelMap_c);
        Debug.Log(levelMap_r);
        int[,] levelMap = new int[levelMap_r, levelMap_c];
        for (int r = 0; r < levelMap_r; r++)
        {
            for (int c = 0; c < levelMap_c; c++)
            {

                levelMap[r, c] = arr2d[
                                    r < quadrant_r ? r : 2 * quadrant_r - r - 2,
                                    c < quadrant_c ? c : 2 * quadrant_c - c - 1];

            }
        }

        return levelMap;
    }

    public static int score = 0;
}
