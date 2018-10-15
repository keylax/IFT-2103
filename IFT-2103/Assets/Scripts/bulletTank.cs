using UnityEngine;
using System.Collections.Generic;

public class bulletTank : MonoBehaviour
{
    protected Vector3 m_BulletAcceleration;

    private Vector3 m_BulletVelocity;
    private Vector3 tankPosition;
    private List<GameObject> obstacles;
    private GameObject[] walls;
    private GameObject plane;
    private bool collided;

    private void Start()
    {
        collided = false;
        plane = GameObject.FindGameObjectWithTag("Plane");
        walls = GameObject.FindGameObjectsWithTag("Obstacle");
        obstacles = new List<GameObject>();
        obstacles.Add(plane);
        foreach (GameObject wall in walls)
            obstacles.Add(wall);
    }

    void Update()
    {
        //checkCollisionsAPriori();
        transform.position += m_BulletVelocity + 0.5f * m_BulletAcceleration * Time.deltaTime;
        m_BulletVelocity += m_BulletAcceleration * Time.deltaTime;
        checkCollisionsAPosteriori();
    }

    public void shootBullet(Vector3 p_BulletVelocity, Vector3 p_BulletAcceleration, Vector3 p_StartPosition, Quaternion p_StartRotation)
    {
        m_BulletVelocity = p_BulletVelocity;
        m_BulletAcceleration = p_BulletAcceleration;
        transform.position = p_StartPosition;
        tankPosition = p_StartPosition;
    }

    private void checkCollisionsAPosteriori()
    {
        foreach (GameObject gameObj in obstacles)
        {
            checkCollisionWith(gameObj, transform.position);
        }
    }

    private void checkCollisionsAPriori()
    {
        //TODO collision a priori
        Vector3 futurePosition = transform.position + (transform.forward * (m_BulletVelocity.magnitude * Time.deltaTime));
        foreach (GameObject gameObj in obstacles)
        {
            checkCollisionWith(gameObj, futurePosition);
        }
    }

    private void checkCollisionWith(GameObject p_gameObject, Vector3 p_position)
    {
        Vector3 closestPoint = closestPointToBullet(p_gameObject, p_position);
        float distanceToClosestPoint = Vector3.Distance(p_position, closestPoint);
        float bulletRadius = GetComponent<Renderer>().bounds.extents.magnitude / 2;

        if (p_gameObject.tag == "Plane")
            closestPoint.y = 0;
        Debug.DrawLine(transform.position, closestPoint, Color.blue);
        Debug.Log(closestPoint + " is closestpoint on object: " + p_gameObject + " and is at distance: " + distanceToClosestPoint);
        if (distanceToClosestPoint < bulletRadius)
        {
            if (p_gameObject.tag == "Plane")
            {
                m_BulletVelocity = m_BulletVelocity * 0;
                m_BulletAcceleration = m_BulletAcceleration * 0;
                Destroy(gameObject, 1.0f);
            } else if (p_gameObject.tag == "Obstacle" && !collided)
            {
                collided = true;
                bulletHitAWall(p_gameObject, closestPoint);
            }
        }
    }

    private void bulletHitAWall(GameObject p_wallHit, Vector3 p_hit)
    {
        //TODO faire rebondir la balle
        RaycastHit hit;
        Ray ray = new Ray(tankPosition, transform.position);
        //Debug.DrawLine(tankPosition, transform.position, Color.green);
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 newVelocity;
            Vector3 surfaceNormal = hit.normal;
            Debug.DrawLine(p_hit, hit.normal, Color.red);
            float NdotV = Vector3.Dot(surfaceNormal, m_BulletVelocity);
            float twoNdotV = 2 * NdotV;
            Vector3 twoNdotVN = twoNdotV * surfaceNormal;
            Vector3 velocityminustwodotVN = m_BulletVelocity - twoNdotVN;

            newVelocity = 0.5f * (velocityminustwodotVN);
            m_BulletAcceleration = m_BulletAcceleration * 0.7f;
            m_BulletVelocity = newVelocity;
        }
    }

    private Vector3 closestPointToBullet(GameObject p_gameObject, Vector3 p_bulletPosition)
    {
        Vector3 closestPoint;
        Vector3 gameObjCenter = p_gameObject.transform.position;
        Vector3 wallUpOrientation = p_gameObject.transform.up;
        Vector3 wallForwardOrientation = p_gameObject.transform.forward;
        Vector3 wallRightOrientation = p_gameObject.transform.right;

        float halfX = p_gameObject.transform.localScale.x /2;
        float halfY = p_gameObject.transform.localScale.y /2;
        float halfZ = p_gameObject.transform.localScale.z /  2;
        Vector3 distanceBetweenBulletAndTarget = p_bulletPosition - gameObjCenter;

        //Start result at center of box and make steps from there
        closestPoint = gameObjCenter;

        //Along the axis of d from the center
        float dist = Vector3.Dot(distanceBetweenBulletAndTarget, wallUpOrientation);
        //If distance farther than the box extents, clamp to the box
        if (dist > halfY) dist = halfY;
        if (dist < -halfY) dist = -halfY;

        closestPoint += dist * wallUpOrientation;

        //Repeat for other axis
        dist = Vector3.Dot(distanceBetweenBulletAndTarget, wallForwardOrientation);
        if (dist > halfZ) dist = halfZ;
        if (dist < -halfZ) dist = -halfZ;

        closestPoint += dist * wallForwardOrientation;

        dist = Vector3.Dot(distanceBetweenBulletAndTarget, wallRightOrientation);
        if (dist > halfX) dist = halfX;
        if (dist < -halfX) dist = -halfX;

        closestPoint += dist * wallRightOrientation;

        return closestPoint;
    }
}