using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDstroy : MonoBehaviour
{
    void Awake()
    {
        var obj = FindObjectsOfType<Player>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
