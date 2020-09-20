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
        
    }
}