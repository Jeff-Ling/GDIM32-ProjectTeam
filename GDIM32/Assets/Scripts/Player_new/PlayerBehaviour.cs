using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private PlayerStats stats;

    #region Var: Combat
    [Header("Combat System")]
    public Rigidbody2D m_bullet;
    public Transform m_FireTransform;
    public float fire_break = 3.0f;

    private float fire_lastTime;
    private float fire_curTime;
    #endregion

    #region Var: Movement
    [Header("Movement System")]
    [SerializeField] private FOV Fov;
    public Transform currentFacing;      // Current Direction that Player is facing (used for FOV)

    private Rigidbody2D m_Rigidbody;
    private float m_MovementInputVertiValue;
    private float m_MovementInputHoriValue;
    #endregion

    #region Audio Component
    [Header("Audio")]
    public AudioSource ShootAS;
    public AudioClip Fire_AudioClip;
    public AudioSource MoveAS;
    public AudioClip Walk_AudioClip;
    #endregion

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        m_Rigidbody.isKinematic = false;
        m_MovementInputVertiValue = 0f;
        m_MovementInputHoriValue = 0f;
    }

    private void OnDisable()
    {
        m_Rigidbody.isKinematic = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector3 targetPosition = currentFacing.position;
        Vector3 aimDir = (targetPosition - transform.position).normalized;
        Vector3 origin = transform.position;
        Fov.SetAimDirection(aimDir);
        Fov.SetOrigin(origin);

        MoveAS.clip = Walk_AudioClip;
    }

    // Update is called once per frame
    void Update()
    {
        // record the current time
        fire_curTime = Time.time;

        Vector3 targetPosition = currentFacing.position;
        Vector3 aimDir = (targetPosition - transform.position).normalized;
        Vector3 origin = transform.position;
        Fov.SetAimDirection(aimDir);
        Fov.SetOrigin(origin);

        WalkAudio();
    }

    public void Fire(bool isFire)
    {
        if (!isFire) { return; }
        if (fire_curTime - fire_lastTime < fire_break) { return; }
        // m_Fired = true;

        Rigidbody2D shellInstance =
            Instantiate(m_bullet, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody2D;

        // set the velocity
        shellInstance.velocity = stats.bulletSpeed * m_FireTransform.right;

        // set the tag
        shellInstance.tag = this.tag;

        // Play the clip
        ShootAS.clip = Fire_AudioClip;
        ShootAS.Play();

        // Record the time
        fire_lastTime = Time.time;
    }

    public void Move(Vector3 inputVec)
    {
        m_Rigidbody.velocity = inputVec * stats.m_Speed * Time.deltaTime;
    }

    public void Turn()
    {
        //float turn = - (m_TurnInputValue * m_TurnSpeed * Time.deltaTime);
        //transform.Rotate(Vector3.forward * turn);

        Vector3 mousePoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        Vector3 dir = (mousePoint - transform.position);
        dir = new Vector3(dir.x, dir.y, 0f).normalized;
        float theta = Mathf.Atan(dir.y / dir.x) * Mathf.Rad2Deg;
        theta = dir.x < 0f ? 180f + theta : theta;
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, theta);

    }

    private void WalkAudio()
    {
        // If there is no input:
        if (Mathf.Abs(m_MovementInputVertiValue) < 0.1f && Mathf.Abs(m_MovementInputHoriValue) < 0.1f)
        {
            if (MoveAS.isPlaying)
            {
                MoveAS.Stop();
            }
        }
        else
        {
            if (!MoveAS.isPlaying)
            {
                MoveAS.Play();
            }
        }
    }
}
