using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public float fire_break = 3.0f;
    public Rigidbody2D bullet;
    public Transform FireTransform;
    public float bulletSpeed = 10.0f;

    private EnemyMovement enemyMovement;
    private Transform tf;
    private bool AbleToFire;
    private float fire_lastTime;
    private float fire_curTime;

    private void Start()
    {
        tf = gameObject.GetComponentInParent<Transform>();
        enemyMovement = gameObject.GetComponentInParent<EnemyMovement>();
    }

    private void Update()
    {
        // record the current time
        fire_curTime = Time.time;

        // If player press m_fireButton and the time exceed the break: Fire
        if (AbleToFire && fire_curTime - fire_lastTime >= fire_break)
        {
            Fire();
        }
    }

    private void Fire()
    {
        Rigidbody2D shellInstance =
            Instantiate(bullet, FireTransform.position, FireTransform.rotation) as Rigidbody2D;

        // set the velocity
        shellInstance.velocity = bulletSpeed * FireTransform.forward;

        // set the tag
        shellInstance.tag = this.tag;

        // Record the time
        fire_lastTime = Time.time;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        // Enemy see Players
        if (collision.gameObject.name == "Player1" || collision.gameObject.name == "Player2")
        {
            // Look At the player and Fire
            tf.LookAt(collision.transform);
            AbleToFire = true;

            // Stop Moving
            enemyMovement.StopMove();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        AbleToFire = false;

        // Start Moving
        enemyMovement.StartMove();
    }
}
