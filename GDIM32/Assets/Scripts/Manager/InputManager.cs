using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    [SerializeField] private KeyCode m_Move_Up_Key = KeyCode.W;
    [SerializeField] private KeyCode m_Move_Down_Key = KeyCode.S;
    [SerializeField] private KeyCode m_Move_Left_Key = KeyCode.A;
    [SerializeField] private KeyCode m_Move_Right_Key = KeyCode.D;

    #region Public interface for player manager
    public Vector3 MoveInput()
    {
        float hori = 0;
        float vert = 0;
        if (Input.GetKey(m_Move_Right_Key))
        {
            hori += 1;
        }
        if (Input.GetKey(m_Move_Left_Key))
        {
            hori -= 1;
        }
        if (Input.GetKey(m_Move_Up_Key))
        {
            vert += 1f;
        }
        if (Input.GetKey(m_Move_Down_Key))
        {
            vert -= 1f;
        }
        if(hori != 0 && vert != 0)
        {
            hori *= 0.7f;
            vert *= 0.7f;
        }
        return new Vector3(hori, vert, 0);
    }
    #endregion

    #region public interface for UI system
    public KeyCode Move_Up_Key
    {
        get { return m_Move_Up_Key; }
        set { m_Move_Up_Key = value; }
    }
    public KeyCode Move_Down_Key
    {
        get { return m_Move_Down_Key; }
        set { m_Move_Down_Key = value; }
    }
    public KeyCode Move_Left_Key
    {
        get { return m_Move_Left_Key; }
        set { m_Move_Left_Key = value; }
    }
    public KeyCode Move_Right_Key
    {
        get { return m_Move_Right_Key; }
        set { m_Move_Right_Key = value; }
    }
    #endregion

}
