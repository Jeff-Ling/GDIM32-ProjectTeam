using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
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

    public float bulletSpeed = 10.0f;

    public Slider m_Slider;
    public Image m_FillImage;
    public Color m_FullHealthColor = Color.green;
    public Color m_ZeroHealthColor = Color.red;

    private bool m_Dead;

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

    // Audio Component
    public AudioSource AS;
    public AudioClip[] Death_AudioClip;

    private void OnEnable()
    {
        m_CurrentHP = m_MaxHP;
        m_Dead = false;

        this.GetComponent<PhotonView>().RPC("SetHealthUI", RpcTarget.All);
    }

    [PunRPC]
    public void TakeDamage(float damage)
    {
        m_CurrentHP -= damage;
        this.GetComponent<PhotonView>().RPC("SetHealthUI", RpcTarget.All);
        // When health is under 0 and m_Dead is false
        if (m_CurrentHP <= 0f && !m_Dead)
        {
            this.GetComponent<PhotonView>().RPC("Dead", RpcTarget.All);
        }
    }

    [PunRPC]
    public void GetHeal(float heal)
    {
        m_CurrentHP += heal;        
        if (m_CurrentHP > 100f)
        {
            m_CurrentHP = 100f;
        }
        this.GetComponent<PhotonView>().RPC("SetHealthUI", RpcTarget.All);
    }

    [PunRPC]
    private void SetHealthUI()
    {
        // Adjust the value and colour of the slider.
        // Set the slider's value appropriately.
        m_Slider.value = m_CurrentHP;

        // Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
        m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHP / m_MaxHP);
    }

    [PunRPC]
    private void Dead()
    {
        m_Dead = true;

        // Play the clip
        int random_clip = Random.Range(0, Death_AudioClip.Length);
        AS.clip = Death_AudioClip[random_clip];
        AS.Play();
        Destroy(this.gameObject);
    }
}
