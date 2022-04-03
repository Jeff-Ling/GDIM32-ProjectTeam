using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Jiefu Ling (jieful2), Xiao Jing (xjing2), Liujiahao Xie(liujiahx), Yurui Leng(yuruil), Yuhao Song (yuhaos5)
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

    // Audio Component
    public AudioSource AS;
    public AudioClip Walk_AudioClip;

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
        Vector3 origin = transform.position;
        Fov.SetAimDirection(aimDir);
        Fov.SetOrigin(origin);

        AS.clip = Walk_AudioClip;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 targetPosition = currentFacing.position;
        Vector3 aimDir = (targetPosition - transform.position).normalized;
        Vector3 origin = transform.position;
        Fov.SetAimDirection(aimDir);
        Fov.SetOrigin(origin);

        getPlayerInput();

        WalkAudio();
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

    private  void WalkAudio()
    {
        // If there is no input:
        if (Mathf.Abs(m_MovementInputValue) < 0.1f && Mathf.Abs(m_TurnInputValue) < 0.1f)
        {
            if (AS.isPlaying)
            {
                AS.Stop();
            }
        }
        else
        {
            if (!AS.isPlaying)
            {
                AS.Play();
            }
        }
    }
}
