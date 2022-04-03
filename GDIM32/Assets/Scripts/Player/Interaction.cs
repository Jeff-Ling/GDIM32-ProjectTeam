using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Author: Jiefu Ling (jieful2)
// This script is used to control player's interaction. 
// Player 1 (F) to pick up
// Player 2 ([3]) to pick up

// Future Feature: Add health


public class Interaction : MonoBehaviour
{

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Interactiable")
        {
            if (this.tag == "Player1")
            {
                if (Input.GetKey(KeyCode.F))
                {
                    // disable interaction sprite
                }
            }
            else if (this.tag == "Player2")
            {
                if (Input.GetKey(KeyCode.Alpha3))
                {
                    // disable interaction sprite
                }
            }
        }
    }

}
