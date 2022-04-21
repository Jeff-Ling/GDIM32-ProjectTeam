using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float existTime = 3f;
    
    void Update()
    {
        if(existTime > 0)
        {
            existTime -= Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
