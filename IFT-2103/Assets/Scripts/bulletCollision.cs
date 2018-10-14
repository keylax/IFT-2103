using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletCollision : MonoBehaviour {
    private GameObject[] allObjects;
    // Use this for initialization
    void Start () {
        allObjects = GameObject.FindGameObjectsWithTag("Obstacle");
    }
	
	// Update is called once per frame
	void Update () {
        foreach (GameObject gameObj in allObjects)
        {
            checkCollision(gameObj);
        }
    }

    void checkCollision(GameObject _gameObject) {
        Vector3 closestPoint = closestPointToBullet(_gameObject);
        float distanceToClosestPoint = Vector3.Distance(transform.position, closestPoint);
        float bulletRadius = GetComponent<Renderer>().bounds.extents.magnitude;
        if (distanceToClosestPoint < bulletRadius)
            Destroy(gameObject);
        /*Debug.Log("forward: " + _gameObject.transform.forward);
        Debug.Log("up: " + _gameObject.transform.up);
        Debug.Log("right: " + _gameObject.transform.right);
        Debug.Log(closestPoint);*/
        Debug.DrawLine(transform.position, closestPoint, Color.green);
    }

    Vector3 closestPointToBullet(GameObject _gameObject) {
        Vector3 closestPoint;
         Vector3 gameObjCenter = _gameObject.transform.position;
         Vector3 wallUpOrientation = _gameObject.transform.up;
         Vector3 wallForwardOrientation = _gameObject.transform.forward;
         Vector3 wallRightOrientation = _gameObject.transform.right;

         float halfX = _gameObject.transform.localScale.x;
         float halfY = _gameObject.transform.localScale.y;
         float halfZ = _gameObject.transform.localScale.z;
         Vector3 distanceBetweenTankAndWall = transform.position - gameObjCenter;

         //Start result at center of box and make steps from there
         closestPoint = gameObjCenter;

         //Along the axis of d from the center
         float dist = Vector3.Dot(distanceBetweenTankAndWall, wallUpOrientation);
         //If distance farther than the box extents, clamp to the box
         if (dist > halfY) dist = halfY;
         if (dist < -halfY) dist = -halfY;

         closestPoint += dist * wallUpOrientation;

         //Repeat for other axis
         dist = Vector3.Dot(distanceBetweenTankAndWall, wallForwardOrientation);
         if (dist > halfZ) dist = halfZ;
         if (dist < -halfZ) dist = -halfZ;

         closestPoint += dist * wallForwardOrientation;

         dist = Vector3.Dot(distanceBetweenTankAndWall, wallRightOrientation);
         if (dist > halfX) dist = halfX;
         if (dist < -halfX) dist = -halfX;

         closestPoint += dist * wallRightOrientation;

         return closestPoint;

        //return _gameObject.GetComponent<BoxCollider>().ClosestPoint(transform.position);

    }
}
