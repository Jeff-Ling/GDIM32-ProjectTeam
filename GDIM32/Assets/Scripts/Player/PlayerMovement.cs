using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private FOV Fov;
    public int m_PlayerNumber = 1;
    public float m_Speed = 12f;
    public float m_TurnSpeed = 180f;
    public Transform currentFacing;      // Current Direction that Player is facing

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
        m_MovementAxisName = "Vertical" + m_PlayerNumber;
        m_TurnAxisName = "Horizontal" + m_PlayerNumber;

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
        m_MovementInputValue = Input.GetAxisRaw(m_MovementAxisName);
        m_TurnInputValue = Input.GetAxisRaw(m_TurnAxisName);
    }

    private void Move()
    {
        m_Rigidbody.velocity = transform.right * m_MovementInputValue * m_Speed * Time.deltaTime;
    }

    private void Turn()
    {
        float turn = - (m_TurnInputValue * m_TurnSpeed * Time.deltaTime);

        transform.Rotate(Vector3.forward * turn);
    }
}
