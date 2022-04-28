using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
// Author: Jiefu Ling (jieful2); Yuhao Song (yuhaos5)
// This script is used to calculate player and enemies' health status.

public class PlayerHealth : MonoBehaviourPun
{
    public float m_StartingHealth = 100f;
    public Slider m_Slider;
    public Image m_FillImage;
    public Color m_FullHealthColor = Color.green;
    public Color m_ZeroHealthColor = Color.red;

    private float m_CurrentHealth;
    private bool m_Dead = false;

    // Audio Component
    public AudioSource AS;
    public AudioClip[] Death_AudioClip;

    private void OnEnable()
    {
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;

        this.GetComponent<PhotonView>().RPC("SetHealthUI", RpcTarget.All);
    }

    [PunRPC]
    public void TakeDamage(float damage)
    {
        m_CurrentHealth -= damage;
        this.GetComponent<PhotonView>().RPC("SetHealthUI", RpcTarget.All);
        // When health is under 0 and m_Dead is false
        if (m_CurrentHealth <= 0f && !m_Dead)
        {
            this.GetComponent<PhotonView>().RPC("Dead", RpcTarget.All);
        }
    }

    [PunRPC]
    public void GetHeal(float heal)
    {
        m_CurrentHealth += heal;
        if (m_CurrentHealth > 100f)
        {
            m_CurrentHealth = 100f;
        }
        this.GetComponent<PhotonView>().RPC("SetHealthUI", RpcTarget.All);
    }

    [PunRPC]
    private void SetHealthUI()
    {
        // Adjust the value and colour of the slider.
        // Set the slider's value appropriately.
        m_Slider.value = m_CurrentHealth;

        // Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
        m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / m_StartingHealth);
    }
    [PunRPC]
    private void Dead()
    {
        m_Dead = true;
        int random_clip = Random.Range(0, Death_AudioClip.Length);
        AS.clip = Death_AudioClip[random_clip];
        AS.Play();
        Destroy(this.gameObject);
    }

}
