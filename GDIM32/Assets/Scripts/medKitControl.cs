using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////////////////////////////////
//Created by Y. Song at 2022.02.22//
////////////////////////////////////

public class medKitControl : MonoBehaviour
{

    public float heal = 60f;

    public AudioSource AS;
    public AudioClip PickUpThing;

    private void OnTriggerEnter2D (Collider2D collison)
    {
        if (collison.name == "Player1" || collison.name == "Player2")
        {

            collison.gameObject.GetComponent<PlayerHealth>().GetHeal(heal);

            AS.clip = PickUpThing;
            AS.Play();

            Destroy(gameObject);
        }
    }
}
