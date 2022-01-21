using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float m_StartingHealth = 100f;
    public Slider m_Slider;
    public Image m_FillImage;
    public Color m_FullHealthColor = Color.green;
    public Color m_ZeroHealthColor = Color.red;

    private float m_CurrentHealth;
    private bool m_Dead;

    private void OnEnable()
    {
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        m_CurrentHealth -= damage;

        // When health is under 0 and m_Dead is false
        if (m_CurrentHealth <= 0f && !m_Dead)
        {
            m_Dead = true;

            Destroy(this.gameObject);
        }
    }
}
