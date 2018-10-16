using UnityEngine;

public class canonTank : MonoBehaviour {

    [SerializeField]
    protected float m_Acceleration;
    [SerializeField]
    protected float m_Speed;
    [SerializeField]
    protected float m_Gravity;
    [SerializeField]
    protected GameObject m_BulletPrefab;

    protected Vector3 m_StartPosition;
    protected Vector3 m_CanonOffset;
    protected Vector3 m_ShotAcceleration;
    protected Vector3 m_ShotGravity;
    protected Vector3 m_ShotVelocity;

    private void Start()
    {
        m_ShotGravity = new Vector3(0f, -m_Gravity, 0f);
    }

    void Update () {
        if (Input.GetButtonDown("Jump"))
        {
            Fire();    
        }
    }

    public void Fire()
    {
        bulletTank bulletObject = Instantiate(m_BulletPrefab).GetComponent<bulletTank>();
        bulletObject.transform.SetParent(null);
        bulletObject.name = m_BulletPrefab.name;
        m_ShotVelocity = transform.forward * m_Speed;
        m_ShotAcceleration = transform.forward * m_Acceleration;
        m_ShotAcceleration += m_ShotGravity;
        m_CanonOffset = transform.forward * 2.5f;
        m_StartPosition = transform.position + m_CanonOffset;
        bulletObject.shootBullet(m_ShotVelocity, m_ShotAcceleration, m_StartPosition, transform.rotation);
    }
}
