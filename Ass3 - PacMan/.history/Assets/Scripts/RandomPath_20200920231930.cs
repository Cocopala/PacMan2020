using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPath : MonoBehaviour
{
    public static RandomPath Instance;
    private List<int> canBeUsedPath;

    void Awake()
    {
        Instance = this;
        canBeUsedPath = new List<int>();  // if some ghost picked 1 path, delete it from the list
        for (int i = 0; i < transform.childCount; i++)
        {
            canBeUsedPath.Add(i);   // when initiation setup, put all path into the list
        }

    }

    public Vector3[] GetPath(out int childPathIndex)  // get a random path
    {
        int randomIndex = Random.Range(0, canBeUsedPath.Count);
        childPathIndex = canBeUsedPath[randomIndex];  // the index of the paths (can pick)
        canBeUsedPath.Remove(childPathIndex);  // remove the used path

        Transform child = transform.GetChild(childPathIndex);

        Vector3[] result = new Vector3[child.childCount];
        for (int i = 0; i < child.childCount; i++)
        {
            result[i] = child.GetChild(i).position;
        }
        return result;
    }
    public void RecoverPath(int childPathIndex)   // put the used paths back to list
    {
        canBeUsedPath.Add(childPathIndex);
    }
}