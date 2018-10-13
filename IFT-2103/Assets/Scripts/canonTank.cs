using UnityEngine;

public class canonTank : MonoBehaviour {

    [SerializeField]
    protected float m_Acceleration;
    [SerializeField]
    protected float m_Speed;
    [SerializeField]
    protected GameObject m_BulletPrefab;

    protected Vector3 m_ShotAcceleration;

    private Vector3 m_ShotVelocity;

    private void Start()
    {
        m_ShotVelocity = new Vector3(m_Speed, 0f, 0f);
        m_ShotAcceleration = new Vector3(m_Acceleration, 0f, 0f);
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
        bulletObject.shootBullet(m_ShotVelocity, m_ShotAcceleration, transform.position, transform.rotation);
    }
}
