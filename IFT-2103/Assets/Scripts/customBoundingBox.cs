using UnityEngine;

public class customBoundingBox : MonoBehaviour
{
    private Vector3 min;
    private Vector3 max;
    private Vector3[] pts = new Vector3[8];

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        min = GetComponent<Renderer>().bounds.min;
        max = GetComponent<Renderer>().bounds.max;
        pts[0] = min;
        pts[1] = max;
        pts[2] = new Vector3(pts[0].x, pts[0].y, pts[1].z);
        pts[3] = new Vector3(pts[0].x, pts[1].y, pts[0].z);
        pts[4] = new Vector3(pts[1].x, pts[0].y, pts[0].z);
        pts[5] = new Vector3(pts[0].x, pts[1].y, pts[1].z);
        pts[6] = new Vector3(pts[1].x, pts[0].y, pts[1].z);
        pts[7] = new Vector3(pts[1].x, pts[1].y, pts[0].z);
    }

    public Vector3[] getPts()
    {
        return pts;
    }

    public Vector3 getMin()
    {
        return min;
    }

    public Vector3 getMax()
    {
        return max;
    }

    public bool intersects(customBoundingBox box)
    {
        return (min.x <= box.getMax().x) && (max.x >= box.getMin().x) &&
                (min.y <= box.getMax().y) && (max.y >= box.getMin().y) &&
                (min.z <= box.getMax().z) && (max.z >= box.getMin().z);
    }

    public Vector3 getIntersectPt(customBoundingBox box)
    {
        return new Vector3(Mathf.Max(min.x, box.getMin().x), Mathf.Max(min.y, box.getMin().y), Mathf.Max(min.z, box.getMin().z));
    }
}

