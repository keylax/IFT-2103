using UnityEngine;

public class rotateTank : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("z"))
        {
            transform.Rotate(0, -5, 0);
        }
        else if (Input.GetKey("c"))
        {
            transform.Rotate(0, 5, 0);
        }
    }
}
