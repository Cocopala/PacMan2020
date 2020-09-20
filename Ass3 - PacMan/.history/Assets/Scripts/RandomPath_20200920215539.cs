using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPath : MonoBehaviour
{
    public static PathMgr Instance;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;

    }

    public Vector3[] GetPath()
    {
        int randomIndex = Random.Range(0, transform.childCount);
        Transform child = transform.GetChild(randomIndex);

        Vector3[] result = new Vector3[child.childCount];
        for (int i = 0; i < pathParent.childCount; i++)
        {
            result[i] = pathParent.GetChild(i).position;
        }
        return result;
    }
}