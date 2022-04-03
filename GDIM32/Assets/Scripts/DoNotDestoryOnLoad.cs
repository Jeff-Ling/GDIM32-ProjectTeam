using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestoryOnLoad : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
