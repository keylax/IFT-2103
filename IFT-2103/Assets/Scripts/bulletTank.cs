using UnityEngine;
using System.Collections.Generic;

public class bulletTank : MonoBehaviour
{
    protected Vector3 m_BulletAcceleration;

    private Vector3 m_BulletVelocity;
    private List<GameObject> obstacles;
    private GameObject[] walls;
    private GameObject plane;
    private bool colliding;

    private void Start()
    {
        colliding = false;
        plane = GameObject.FindGameObjectWithTag("Plane");
        walls = GameObject.FindGameObjectsWithTag("Obstacle");
        obstacles = new List<GameObject>();
        obstacles.Add(plane);
        foreach (GameObject wall in walls)
            obstacles.Add(wall);
    }

    void Update()
    {
        if (!colliding)
            checkCollisionsAPriori();
        transform.position += m_BulletVelocity + 0.5f * m_BulletAcceleration * Time.deltaTime * Time.deltaTime;
        m_BulletVelocity += m_BulletAcceleration * Time.deltaTime;
        if (!colliding)
            checkCollisionsAPosteriori();
    }

    public void shootBullet(Vector3 p_BulletVelocity, Vector3 p_BulletAcceleration, Vector3 p_StartPosition, Quaternion p_StartRotation)
    {
        m_BulletVelocity = p_BulletVelocity;
        m_BulletAcceleration = p_BulletAcceleration;
        transform.position = p_StartPosition;
    }

    private void checkCollisionsAPosteriori()
    {
        foreach (GameObject gameObj in obstacles)
        {
            checkCollisionWith(gameObj);
        }
    }

    private void checkCollisionsAPriori()
    {
        //TODO collision a priori
    }

    private void checkCollisionWith(GameObject _gameObject)
    {
        Vector3 closestPoint = closestPointToBullet(_gameObject);
        float distanceToClosestPoint = Vector3.Distance(transform.position, closestPoint);
        float bulletRadius = GetComponent<Renderer>().bounds.extents.magnitude / 2;
        Debug.DrawLine(transform.position, closestPoint, Color.blue);
        if (distanceToClosestPoint < bulletRadius || distanceToClosestPoint < 0)
        {
            colliding = true;
            bulletHitAWall(_gameObject, closestPoint);
        } else if (distanceToClosestPoint > 1)
        {
            colliding = false;
        }
    }

    private void bulletHitAWall(GameObject _wallHit, Vector3 _hit)
    {
        //TODO faire rebondir la balle
        RaycastHit hit;
        Ray ray = new Ray(transform.position, _hit);

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 newVelocity;
            Vector3 surfaceNormal = hit.normal;
            float NdotV = Vector3.Dot(surfaceNormal, m_BulletVelocity);
            float twoNdotV = 2 * NdotV;
            Vector3 twoNdotVN = twoNdotV * surfaceNormal;
            Vector3 velocityminustwodotVN = m_BulletVelocity - twoNdotVN;

            newVelocity = 0.5f * (velocityminustwodotVN);
            m_BulletAcceleration = m_BulletAcceleration * 0.5f;
            m_BulletVelocity = newVelocity;
        }
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