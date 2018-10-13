using UnityEngine;

public class bulletTank : MonoBehaviour
{
    protected Vector3 m_BulletAcceleration;

    private Vector3 m_BulletVelocity;

    void Update()
    {
        //New position
        transform.position += m_BulletVelocity + 0.5f * m_BulletAcceleration * Time.deltaTime * Time.deltaTime;
        //New velocity
        m_BulletVelocity += m_BulletAcceleration * Time.deltaTime;
    }

    public void shootBullet(Vector3 p_BulletVelocity, Vector3 p_BulletAcceleration, Vector3 p_StartPosition, Quaternion p_StartRotation)
    {
        m_BulletVelocity = p_BulletVelocity;
        m_BulletAcceleration = p_BulletAcceleration;
        transform.position = p_StartPosition;
        //transform.rotation = p_StartRotation;
    }

    private void bulletHitAWall()
    {
        //Code qui gère la colision entre un balle et un mur / un autre gameObject physique.
    }
}