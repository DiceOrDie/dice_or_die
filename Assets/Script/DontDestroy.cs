using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DontDestroy : MonoBehaviour
{
    static public List<GameObject> dont_destroy = new List<GameObject>();
    void Awake()
    {
        dont_destroy.Add(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }
}
