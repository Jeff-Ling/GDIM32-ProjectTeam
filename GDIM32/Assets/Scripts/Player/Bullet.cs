using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2" || collision.gameObject.tag == "Enemy")
            {
                PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

                playerHealth.TakeDamage(Damage);
            }
        }
    }
}
