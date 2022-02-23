using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////////////////////////////////
//Created by Y. Song at 2022.02.22//
////////////////////////////////////

public class medKitControl : MonoBehaviour
{
    private void OnTriggerEnter2D (Collider2D collison)
    {
        if (collison.tag == "Player1" || collison.tag == "Player2")
        {
            Destroy(gameObject);
            // Player health ++
        }
    }
}
