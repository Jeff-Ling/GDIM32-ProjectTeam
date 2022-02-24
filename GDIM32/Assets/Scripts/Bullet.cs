using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Jiefu Ling (jieful2)
// This script is used to define bullet behaviour
// Bullet should cross the gameobject that generate it. Should cause damage to enemy and friend
// Bullet should destroy when it hit something else.

public class Bullet : MonoBehaviour
{
    public float LifeTime = 2f;
    public float Damage = 30f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, LifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != this.tag)
        {
            if (collision.gameObject.name == "Player1" || collision.gameObject.name == "Player2" || collision.gameObject.name == "Enemy")
            {
                PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

                playerHealth.TakeDamage(Damage);
            }

            if (collision.name != "Enemy FOV" && collision.name != "Bullet")
            {
                Destroy(gameObject);
            }
        }
    }
}
