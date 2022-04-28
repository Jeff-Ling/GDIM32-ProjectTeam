using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// Author: Jiefu Ling (jieful2); Yurui Leng(yuruil)
// This script is used to define bullet behaviour
// Bullet should cross the gameobject that generate it. Should cause damage to enemy and friend
// Bullet should destroy when it hit something else.

public class Bullet : MonoBehaviour
{
    public float Damage = 30f;

    private AudioSource AS;
    public AudioClip HitOnWall;

    // Start is called before the first frame update
    void Start()
    {
        AS = GameObject.Find("SoundSystem").GetComponent<AudioSource>();
        if (AS == null)
        {
            Debug.Log("G");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name == "Shield Collider")
            PhotonNetwork.Destroy(this.gameObject);

        if (collision.gameObject.tag != this.tag)
        {
            if (collision.gameObject.name == "Player1" || collision.gameObject.name == "Player2" || collision.gameObject.name.StartsWith("Enemy"))
            {                

                PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

                playerHealth.TakeDamage(Damage);
            }

            if (collision.gameObject.name != "FOV" && !collision.gameObject.name.StartsWith("Bullet"))
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }

        if (this.gameObject.tag != "Enemy")
        {
            if (collision.gameObject.tag == "Map")
            {
                AS.clip = HitOnWall;
                AS.Play();
            }
        }

    }
}
