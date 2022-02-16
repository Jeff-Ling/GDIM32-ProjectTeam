using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Jiefu Ling (jieful2)
// This script is used to control player's movement.

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private FOV Fov;
    public int m_PlayerNumber = 1;
    public float m_Speed = 12f;
    public float m_TurnSpeed = 180f;
    public Transform currentFacing;      // Current Direction that Player is facing (used for FOV)

    private string m_MovementAxisName;
    private string m_TurnAxisName;
    private Rigidbody2D m_Rigidbody;
    private float m_MovementInputValue;
    private float m_TurnInputValue;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        m_Rigidbody.isKinematic = false;
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
    }

    private void OnDisable()
    {
        m_Rigidbody.isKinematic = true;
    }

    private void Start()
    {
        m_TurnAxisName = "Rotation" + m_PlayerNumber;

        Vector3 targetPosition = currentFacing.position;
        Vector3 aimDir = (targetPosition - transform.position).normalized;
        Fov.SetAimDirection(aimDir);
        Fov.SetOrigin(transform.position);
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 targetPosition = currentFacing.position;
        Vector3 aimDir = (targetPosition - transform.position).normalized;
        Fov.SetAimDirection(aimDir);
        Fov.SetOrigin(transform.position);

        getPlayerInput();
    }

    private void FixedUpdate()
    {
        // Move and turn the player.
        Move();
        Turn();
    }

    private void getPlayerInput()
    {
        // Store the player's input
        m_TurnInputValue = Input.GetAxisRaw(m_TurnAxisName);
    }

    private void Move()
    {
        // Player 1 Movement
        Vector3 moveDir = new Vector3(0, 0);
        if (m_PlayerNumber == 1)
        {
            if (Input.GetKey(KeyCode.W))
            {
                moveDir.y = +1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                moveDir.y = -1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                moveDir.x = -1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                moveDir.x = +1;
            }
            moveDir.Normalize();
        }
        // Player 2 Movement
        else
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                moveDir.y = +1;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                moveDir.y = -1;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                moveDir.x = -1;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                moveDir.x = +1;
            }
            moveDir.Normalize();
        }


        m_Rigidbody.velocity = moveDir * m_Speed * Time.deltaTime;
    }

    private void Turn()
    {
        float turn = - (m_TurnInputValue * m_TurnSpeed * Time.deltaTime);

        transform.Rotate(Vector3.forward * turn);
    }
}
