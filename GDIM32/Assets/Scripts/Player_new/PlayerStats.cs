using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public enum PlayerType
    {
        Gun,
        Sheild
    }
    [SerializeField] private float m_MoveSpeed;
    [SerializeField] private float m_MaxHP;
    [SerializeField] private PlayerType m_Type;
    private float m_CurrentHP;

    #region Public interface
    public PlayerType Type
    {
        get { return m_Type; }
    }
    public float MoveSpeed
    {
        get { return m_MoveSpeed; }
        set { m_MoveSpeed = value; }
    }
    public float MaxHP
    {
        get { return m_MaxHP; }
        set { m_MaxHP = value; }
    }
    public float CurrentHP
    {
        get { return m_CurrentHP; }
        set { m_CurrentHP = value; }
    }
    public bool IsDead
    {
        get { return m_CurrentHP <= 0; }
    }
    #endregion

    private void Start()
    {
        m_CurrentHP = m_MaxHP;
    }
}
