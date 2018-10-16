﻿using UnityEngine;
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
        walls = GameObject.FindGameObjectsWithTag("Target");
        obstacles = new List<GameObject>();
        obstacles.Add(plane);
        foreach (GameObject wall in walls)
            obstacles.Add(wall);
    }

    void Update()
    {
        checkCollisionsAPriori();
        m_BulletVelocity = m_BulletVelocity + 0.5f * m_BulletAcceleration * Time.deltaTime;
        transform.position += m_BulletVelocity;
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
        Vector3 futurePosition = transform.position + (transform.forward * (m_BulletVelocity.magnitude * Time.deltaTime));
        foreach (GameObject gameObj in obstacles)
        {
            checkCollisionWith(gameObj, futurePosition);
        }
    }

    private void checkCollisionWith(GameObject p_gameObject, Vector3 p_position)
    {
        Vector3 closestPoint = closestPointToBullet(p_gameObject, p_position);

        if (p_gameObject.tag == "Plane")
            closestPoint.y = 0;

        float distanceToClosestPoint = Vector3.Distance(p_position, closestPoint);
        float bulletRadius = GetComponent<Renderer>().bounds.extents.magnitude / 2;

        if (distanceToClosestPoint < bulletRadius)
        {
            if (p_gameObject.tag == "Plane")
            {
                m_BulletVelocity = m_BulletVelocity * 0;
                m_BulletAcceleration = m_BulletAcceleration * 0;
                Destroy(gameObject, 1.0f);
            } else if (p_gameObject.tag == "Target" && !collided)
            {
                collided = true;
                bulletHitAWall(p_gameObject, closestPoint);
                p_gameObject.GetComponent<targetBehaviour>().onCollision();
            }
        }
    }

    private void bulletHitAWall(GameObject p_wallHit, Vector3 p_hit)
    {
        RaycastHit hit;
        Ray ray = new Ray(tankPosition, p_hit);
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 newVelocity;
            Vector3 surfaceNormal = hit.normal;
            float NdotV = Vector3.Dot(surfaceNormal, m_BulletVelocity);
            float twoNdotV = 2 * NdotV;
            Vector3 twoNdotVN = twoNdotV * surfaceNormal;
            Vector3 velocityminustwodotVN = m_BulletVelocity - twoNdotVN;

            newVelocity = 0.5f * (velocityminustwodotVN);
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

        if (isPointInObstacle(p_bulletPosition, p_gameObject)) return p_bulletPosition;

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

    private bool isPointInObstacle(Vector3 p_point, GameObject p_obstacle)
    {
        float distance = Vector3.Distance(p_point, p_obstacle.transform.position);

        float halfX = (p_obstacle.GetComponent<Renderer>().bounds.size.x * 0.5f);
        float halfY = (p_obstacle.GetComponent<Renderer>().bounds.size.y * 0.5f);
        float halfZ = (p_obstacle.GetComponent<Renderer>().bounds.size.z * 0.5f);
        if (distance < halfX && distance < halfY && distance < halfZ)
            return true;
        else
            return false;
    }
}