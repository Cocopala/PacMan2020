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
    public GameObject whosYourDaddy;

    Dictionary<int, GameObject> valueSpriteMap = new Dictionary<int, GameObject>();

    private int[,] quadrant = GameStatic.quadrant;
    private int[,] rotation;
    public int[,] levelMap = GameStatic.levelMap;

    // Start is called before the first frame update
    void Start()
    {

        valueSpriteMap.Add(0, empty);
        valueSpriteMap.Add(1, outsideCorner);
        valueSpriteMap.Add(2, outsideWall);
        valueSpriteMap.Add(3, insideCorner);
        valueSpriteMap.Add(4, insideWall);
        valueSpriteMap.Add(5, standardPellet);
        valueSpriteMap.Add(6, powerPellet);
        valueSpriteMap.Add(7, tJunction);
        initMartix(quadrant);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void initMartix(int[,] arr2d)
    {
        for (int r = 0; r < levelMap.GetLength(0); r++)
        {
            for (int c = 0; c < levelMap.GetLength(1); c++)
            {
                Vector3 rotation = calcRotation(levelMap[r, c], r, c); 
                initGrid(levelMap[r, c], r, c, rotation);
            } 
        }
    }

    public void initGrid(int value,int x, int y,Vector3 rotation)
    {
        GameObject piece = Instantiate(valueSpriteMap[value], new Vector2(y, -x), Quaternion.Euler(rotation));
        if (whosYourDaddy)
            piece.transform.SetParent(whosYourDaddy.transform, false);

        GameStatic.gameObjectsMap[x, y] = piece;
    }

    public Vector3 calcRotation(int value, int x, int y)
    {
        int rotateZ = 0;
        int up = 0, down = 0, left = 0, right = 0;
        if (x > 0) up = levelMap[x - 1, y];
        if (x < levelMap.GetLength(0) - 2) down = levelMap[x + 1, y];
        if (y > 0) left = levelMap[x, y - 1];
        if (y < levelMap.GetLength(1) - 2) right = levelMap[x, y + 1];
        switch (value) {
            case 1:
                if (right == 2 && down == 2) rotateZ = 0;
                if (left == 2 && down == 2) rotateZ = 270;
                if (up == 2 && left == 2) rotateZ = 180;
                if (up == 2 && right == 2) rotateZ = 90;

                break;
            case 2:
                if (right == 2 || left == 2 ) rotateZ = 90;
                break;
            case 3:
                //if (right == 4 && down == 4 || (right == 3 && down == 3)) rotateZ = 0;
                //if (left == 4 && down == 4 || (left == 3 && down == 3)) rotateZ = 270;
                //if (up == 4 && left == 4 || (up == 3 && left == 3)) rotateZ = 180;
                //if (up == 4 && right == 4 || (up == 3 && right == 3)) rotateZ = 90;
                if ((right != 0 && right != 5 && right != 6) && (down != 0 && down != 5 && down != 6)) rotateZ = 0;
                if ((left != 0 && left != 5 && left != 6) && (down != 0 && down != 5 && down != 6)) rotateZ = 270;
                if ((up != 0 && up != 5 && up != 6) && (left != 0 && left != 5 && left != 6)) rotateZ = 180;
                if ((up != 0 && up != 5 && up != 6) && (right != 0 && right != 5 && right != 6)) rotateZ = 90;
                break;
            case 4:
                if ((left != 0 && left != 5 && left != 6) && (right != 0 && right != 5 && right != 6)) rotateZ = 90;

                    break;
            case 7:
                break;
            default:
                break;

        }
        // special cases
        int rotateY = 0;
        if (x == 0 && y == 14)
            rotateY = 180;
        if (x == 7 && y == 13)
            rotateZ = 270;
        if (x == 7 && y == 14)
            rotateZ = 0;
        if (x == 10 && y == 8)
            rotateZ = 0;
        if (x == 9 && y == 19)
            rotateZ = 180;
        if (x == 10 && y == 19)
            rotateZ = 270;
        if (x == 12 && y == 12)
            rotateZ = 90;
        if (x == 21 && y == 13)
            rotateZ = 180;
        if (x == 19 && y == 8)
            rotateZ = 0;
        if (x == 18 && y == 19)
            rotateZ = 180;
        if (x == 19 && y == 19)
            rotateZ = 270;

            if (x == 12 && y == 12 ||
            x == 12 && y == 15
            || x == 16 && y == 12
            || x == 16 && y == 15)
            rotateZ = 90;

        if (x == 28 && y == 13) {
            rotateZ = 180;
            rotateY = 180;
        }
        if (x == 28 && y == 14)
        {
            rotateZ = 180;
        }

        Vector3 rotation = new Vector3(0, rotateY, rotateZ);
        return rotation;
    }


    public int[,] getLevelMap()
    {
        return levelMap;
    }
}
