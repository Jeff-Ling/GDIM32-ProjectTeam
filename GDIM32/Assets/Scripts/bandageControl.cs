using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////////////////////////////////
//Created by Y. Song at 2022.02.23//
////////////////////////////////////

public class bandageControl : MonoBehaviour
{

    public float heal = 30f;

    private void OnTriggerEnter2D (Collider2D collison)
    {
        if (collison.name == "Player1" || collison.name == "Player2")
        {

            collison.gameObject.GetComponent<PlayerHealth>().GetHeal(heal);

            Destroy(gameObject);
        }
    }
}
