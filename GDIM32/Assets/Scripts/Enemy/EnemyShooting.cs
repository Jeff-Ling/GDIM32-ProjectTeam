using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Jiefu Ling (jieful2)
// This script is used to control enemy's firing. 

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
    private float damping = 2f;

    private void Start()
    {
        tf = transform.parent.GetComponent<Transform>();
        enemyMovement = transform.parent.GetComponent<EnemyMovement>(); 
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
        shellInstance.velocity = bulletSpeed * FireTransform.right;

        // set the tag
        shellInstance.tag = this.tag;

        // Record the time
        fire_lastTime = Time.time;
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {

        // Enemy see Players
        if (collision.gameObject.name == "Player1" || collision.gameObject.name == "Player2")
        {
            // Look At the player and Fire
            var lookPos = collision.transform.position - tf.position;
            lookPos.y = 0;
            Debug.Log(lookPos);
            var rotation = Quaternion.LookRotation(lookPos);
            Debug.Log(rotation);
            tf.rotation = Quaternion.Slerp(tf.rotation, rotation, Time.deltaTime * damping);

            AbleToFire = true;

            // Stop Moving
            enemyMovement.StopMove();
        }
    }
    */
    private void OnTriggerStay2D(Collider2D collision)
    {
        // Enemy see Players
        if (collision.gameObject.name == "Player1" || collision.gameObject.name == "Player2")
        {
            // Look At the player and Fire
            Vector3 targ = collision.transform.position;
            targ.z = 0f;

            Vector3 objectPos = tf.position;
            targ.x = targ.x - objectPos.x;
            targ.y = targ.y - objectPos.y;

            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
            tf.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

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
