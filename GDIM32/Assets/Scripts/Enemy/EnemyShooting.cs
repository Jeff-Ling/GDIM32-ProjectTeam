using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Jiefu Ling (jieful2); Xiao Jing (xjing2)
// This script is used to control enemy's firing. 
// When player1 or 2 enter the collider, we change the state to SHOOTING. 
// When they leave, we back to PATROL.

public class EnemyShooting : MonoBehaviour
{
    private EnemyMovement enemyMovement;
    private float damping = 2f;

    private void Start()
    {
        enemyMovement = transform.parent.GetComponent<EnemyMovement>(); 
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
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            // Send target information
            enemyMovement.GetTarget(collision.transform.position);
            enemyMovement.ChangeState(EnemyMovement.Status.SHOOTING);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            enemyMovement.GetTarget(collision.transform.position);
            enemyMovement.ChangeState(EnemyMovement.Status.CHASE);
            enemyMovement.SetChaseLastTime(Time.time);
        }
    }
}
