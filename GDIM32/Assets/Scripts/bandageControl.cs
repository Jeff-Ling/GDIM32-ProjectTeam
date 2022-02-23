using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////////////////////////////////
//Created by Y. Song at 2022.02.23//
////////////////////////////////////

public class bandageControl : MonoBehaviour
{
    private void OnTriggerEnter2D (Collider2D collison)
    {
        if (collison.tag == "Player1" || collison.tag == "Player2")
        {
            // if (collison.tag == "Player1")
            collison.GetComponent<PlayerHealth>().m_CurrentHealth += 30f;

            if (collison.GetComponent<PlayerHealth>().m_CurrentHealth >= 100f)
            {
                collison.GetComponent<PlayerHealth>().m_CurrentHealth = 100f;
            }

            Destroy(gameObject);
        }
    }
}
