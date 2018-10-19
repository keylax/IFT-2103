using UnityEngine;

public class tankCollisions : MonoBehaviour {
    GameObject[] walls;
    GameObject tankMovement;

    // Use this for initialization
    void Start () {
        tankMovement = transform.gameObject;
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
                } else if (isInBack(intersectPt))
                {
                    tankMovement.GetComponent<moveTank>().collisionInBack();
                } else if (isAtLeft(intersectPt))
                {
                    tankMovement.GetComponent<moveTank>().collisionAtLeft();
                } else if (isAtRight(intersectPt))
                {
                    tankMovement.GetComponent<moveTank>().collisionAtRight();
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

    private bool isInBack(Vector3 _pt)
    {
        return Vector3.Dot(transform.forward * -1, _pt) > 0;
    }

    private bool isAtLeft(Vector3 _pt)
    {
        return Vector3.Dot(transform.right * -1, _pt) > 0;
    }

    private bool isAtRight(Vector3 _pt)
    {
        return Vector3.Dot(transform.right, _pt) > 0;
    }
}
