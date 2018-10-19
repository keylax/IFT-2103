using UnityEngine;

public class shootTank : MonoBehaviour {

    public float shootForce;
    public Transform shootPosition;
    public GameObject bullet;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            GameObject bulletInstance = (GameObject)Instantiate(bullet, transform.position, shootPosition.rotation);
            bulletInstance.GetComponent<Rigidbody>().velocity = bulletInstance.transform.forward * shootForce;
        }
    }
}
