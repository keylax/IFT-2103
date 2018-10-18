using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tankCollisions : MonoBehaviour {
    GameObject[] walls;
    GameObject tankMovement;

    // Use this for initialization
    void Start () {
        tankMovement = transform.parent.gameObject;
        walls = GameObject.FindGameObjectsWithTag("Obstacle");
    }

    // Update is called once per frame
    void Update()
    {
        customBoundingBox myBox = GetComponent<customBoundingBox>();
        Vector3 intersectPt;
        bool isInCollision = false;
        for (int i = 0; i < walls.Length; i++)
        {
            customBoundingBox wallBox = walls[i].GetComponent<customBoundingBox>();
            if (myBox.intersects(wallBox))
            {
                isInCollision = true;
                intersectPt = myBox.getIntersectPt(wallBox);

                if (isInFront(intersectPt))
                {
                    tankMovement.GetComponent<moveTank>().collisionInFront();
                } else
                {
                    tankMovement.GetComponent<moveTank>().collisionInBack();
                }
            }
        }

        if (!isInCollision)
        {
            tankMovement.GetComponent<moveTank>().resetCollision();
        }
    }

    private bool isInFront(Vector3 _pt)
    {
        return Vector3.Dot(transform.forward, _pt) > 0;
    }
}
