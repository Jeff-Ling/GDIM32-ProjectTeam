using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

// Author: Jiefu Ling (jieful2)
// This script is used to calculate player and enemies' health status.

public class PlayerHealth : MonoBehaviour
{
    public float m_StartingHealth = 100f;
    public Slider m_Slider;
    public Image m_FillImage;
    public Color m_FullHealthColor = Color.green;
    public Color m_ZeroHealthColor = Color.red;

    public float m_CurrentHealth;
    private bool m_Dead;

    private void OnEnable()
    {
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;
        
        if (this.tag != "Enemy")
        {
            SetHealthUI();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        m_CurrentHealth -= damage;

        // Set the UI;
        if (this.tag != "Enemy")
        {
            SetHealthUI();
        }

        // When health is under 0 and m_Dead is false
        if (m_CurrentHealth <= 0f && !m_Dead)
        {
            m_Dead = true;

            Destroy(this.gameObject);
        }
    }

    private void SetHealthUI()
    {
        // Adjust the value and colour of the slider.
        // Set the slider's value appropriately.
        m_Slider.value = m_CurrentHealth;

        // Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
        m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / m_StartingHealth);
    }
}
