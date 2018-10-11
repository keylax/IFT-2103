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
            // Add velocity to the bullet
            bulletInstance.GetComponent<Rigidbody>().velocity = bulletInstance.transform.forward * shootForce;

            // Destroy the bullet after 2 seconds
            Destroy(bulletInstance, 2.0f);
        }
    }
}
