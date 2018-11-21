using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

[System.Serializable]
public class CarInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
}

public class carController : Photon.Pun.MonoBehaviourPun
{
    public List<CarInfo> CarInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;

    private Vector3 initialPosition;
    

    public void Start()
    {
        initialPosition = transform.position;
    }

    public void reset()
    {
        foreach (CarInfo carInfo in CarInfos)
        {
            if (carInfo.steering)
            {
                carInfo.leftWheel.steerAngle = 0;
                carInfo.rightWheel.steerAngle = 0;
            }
            if (carInfo.motor)
            {
                carInfo.leftWheel.motorTorque = 0;
                carInfo.rightWheel.motorTorque = 0;
            }
        }

        transform.position = initialPosition;
        transform.eulerAngles = new Vector3(0, 90, 0);

        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public void applyColliderRotationToWheelObject(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.rotation = rotation;
    }

    public void FixedUpdate()
    {
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        if (photonView.IsMine == false && Photon.Pun.PhotonNetwork.IsConnected == true)
        {
            return;
        }
        foreach (CarInfo carInfo in CarInfos)

        {
            if (carInfo.steering)
            {
                carInfo.leftWheel.steerAngle = steering;
                carInfo.rightWheel.steerAngle = steering;
            }
            if (carInfo.motor)
            {
                if (Input.GetAxisRaw("Vertical") == 0)
                {
                    carInfo.leftWheel.motorTorque = 0;
                    carInfo.rightWheel.motorTorque = 0;
                }
                else
                {
                    float motor = maxMotorTorque * Input.GetAxisRaw("Vertical");
                    carInfo.leftWheel.motorTorque = motor;
                    carInfo.rightWheel.motorTorque = motor;
                }
            }
            applyColliderRotationToWheelObject(carInfo.leftWheel);
            applyColliderRotationToWheelObject(carInfo.rightWheel);
        }
    }
}