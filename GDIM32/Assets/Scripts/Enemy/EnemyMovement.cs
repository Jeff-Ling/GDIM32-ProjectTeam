using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float Speed = 12f;
    public float TurnSpeed = 180f;
    public float BehaviourBreak = 3f;

    private Rigidbody2D rb;
    private float MovementInputValue = 1f;
    private float TurnInputValue = 0f;
    private bool Moving = true;
    private float Behaviour_lastTime;
    private float Behaviour_currentTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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

    // Update is called once per frame
    void Update()
    {
        Behaviour_currentTime = Time.time;
        if (Behaviour_currentTime - Behaviour_lastTime >= BehaviourBreak)
        {
            getRandomInput();
        }
    }

    private void FixedUpdate()
    {
        if (Moving)
        {
            // Move and turn the enemy.
            Move();
            Turn();
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

    // Stop Enemy Any Behavior
    public void StopMove()
    {
        Moving = false;
    }

    // Make Enemy Start Move Again
    public void StartMove()
    {
        Moving = true;
    }
}
