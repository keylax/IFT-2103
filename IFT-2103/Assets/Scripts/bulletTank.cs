using UnityEngine;

public class bulletTank : MonoBehaviour
{
    protected Vector3 m_BulletAcceleration;

    private Vector3 m_BulletVelocity;
    private GameObject[] obstacles;

    private void Start()
    {
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
    }

    void Update()
    {
        transform.position += m_BulletVelocity + 0.5f * m_BulletAcceleration * Time.deltaTime * Time.deltaTime;
        m_BulletVelocity += m_BulletAcceleration * Time.deltaTime;
        checkCollisionsWithObstacles();
    }

    public void shootBullet(Vector3 p_BulletVelocity, Vector3 p_BulletAcceleration, Vector3 p_StartPosition, Quaternion p_StartRotation)
    {
        m_BulletVelocity = p_BulletVelocity;
        m_BulletAcceleration = p_BulletAcceleration;
        transform.position = p_StartPosition;
    }

    private void checkCollisionsWithObstacles()
    {
        foreach (GameObject gameObj in obstacles)
        {
            checkCollisionWith(gameObj);
        }
    }

    private void checkCollisionWith(GameObject _gameObject)
    {
        Vector3 closestPoint = closestPointToBullet(_gameObject);

        float distanceToClosestPoint = Vector3.Distance(transform.position, closestPoint);
        float bulletRadius = GetComponent<Renderer>().bounds.extents.magnitude;

        if (distanceToClosestPoint < bulletRadius)
            bulletHitAWall(_gameObject);        
    }

    private void bulletHitAWall(GameObject _wallHit)
    {
        //Code qui gère la colision entre un balle et un mur / un autre gameObject physique.
        //TODO faire rebondir la balle
        Destroy(gameObject);
    }

    private Vector3 closestPointToBullet(GameObject _gameObject)
    {
        Vector3 closestPoint;
        Vector3 gameObjCenter = _gameObject.transform.position;
        Vector3 wallUpOrientation = _gameObject.transform.up;
        Vector3 wallForwardOrientation = _gameObject.transform.forward;
        Vector3 wallRightOrientation = _gameObject.transform.right;

        float halfX = _gameObject.transform.localScale.x / 2;
        float halfY = _gameObject.transform.localScale.y / 2;
        float halfZ = _gameObject.transform.localScale.z / 2;
        Vector3 distanceBetweenBulletAndWall = transform.position - gameObjCenter;

        //Start result at center of box and make steps from there
        closestPoint = gameObjCenter;

        //Along the axis of d from the center
        float dist = Vector3.Dot(distanceBetweenBulletAndWall, wallUpOrientation);
        //If distance farther than the box extents, clamp to the box
        if (dist > halfY) dist = halfY;
        if (dist < -halfY) dist = -halfY;

        closestPoint += dist * wallUpOrientation;

        //Repeat for other axis
        dist = Vector3.Dot(distanceBetweenBulletAndWall, wallForwardOrientation);
        if (dist > halfZ) dist = halfZ;
        if (dist < -halfZ) dist = -halfZ;

        closestPoint += dist * wallForwardOrientation;

        dist = Vector3.Dot(distanceBetweenBulletAndWall, wallRightOrientation);
        if (dist > halfX) dist = halfX;
        if (dist < -halfX) dist = -halfX;

        closestPoint += dist * wallRightOrientation;

        return closestPoint;
    }
}