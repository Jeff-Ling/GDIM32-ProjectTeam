using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Jiefu Ling (jieful2)
// This script is used to control enemy movement.
// Currently this is just a basic AI (Random move)

// We use Finite State Machine to build our AI.
// State: IDLE, PATROL, SHOOTING, CHASE

public class EnemyMovement : MonoBehaviour
{
    public float Speed = 12f;
    public float TurnSpeed = 180f;
    public float BehaviourBreak = 3f;

    private Rigidbody2D rb;
    private Transform tf;
    private float MovementInputValue = 1f;
    private float TurnInputValue = 0f;

    private float Behaviour_lastTime;
    private float Behaviour_currentTime;

    // Fire Element
    public float fire_break = 3.0f;
    public Rigidbody2D bullet;
    public Transform FireTransform;
    public float bulletSpeed = 10.0f;
    private float fire_lastTime;
    private float fire_curTime;
    private Vector3 targ;

    // Chase Element
    public float chase_time = 5f;

    private string state = "IDLE";

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
    }

    private void OnEnable()
    {
        rb.isKinematic = false;
        MovementInputValue = 0f;
        TurnInputValue = 0f;
    }

    private void OnDisable()
    {
        rb.isKinematic = true;
    }

    private void Start()
    {
        state = "PATROL";
    }

    private void FixedUpdate()
    {

        if (state == "PATROL")
        {
            PatrolUpdate();
        }
        else if (state == "SHOOTING")
        {
            ShootingUpdate();
        }
        else if (state == "CHASE")
        {
            ChaseUpdate();
        }
    }

    private void PatrolUpdate()
    {
        Behaviour_currentTime = Time.time;
        if (Behaviour_currentTime - Behaviour_lastTime >= BehaviourBreak)
        {
            getRandomInput();
        }
        Move();
        Turn();
    }

    private void ShootingUpdate()
    {
        // Make enemy stop moving
        MovementInputValue = 0f;
        Move();

        // Look At the player and Fire
        targ.z = 0f;

        Vector3 objectPos = tf.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        tf.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // record the current time
        fire_curTime = Time.time;

        // If player press m_fireButton and the time exceed the break: Fire
        if (fire_curTime - fire_lastTime >= fire_break)
        {
            Fire();
        }

        Debug.Log(targ);

    }

    private void ChaseUpdate()
    {
        MovementInputValue = 5f;
        Move();

        Debug.Log(targ);


        if (Vector3.Distance(tf.position, targ) <= 2f)
        {
            Debug.Log("state change to partol");
            ChangeState("PATROL");
        }
    }

    private void getRandomInput()
    {
        // Store the player's input
        MovementInputValue = 1f;
        TurnInputValue = Random.Range(-1, 2);
        Behaviour_lastTime = Time.time;
    }

    private void Move()
    {
        rb.velocity = transform.right * MovementInputValue * Speed * Time.deltaTime;
    }

    private void Turn()
    {
        float turn = -(TurnInputValue * TurnSpeed * Time.deltaTime);

        transform.Rotate(Vector3.forward * turn);
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

    public void GetTarget(Vector3 target)
    {
        targ = target;
    }

    public void ChangeState(string s)
    {
        state = s;
    }

}
