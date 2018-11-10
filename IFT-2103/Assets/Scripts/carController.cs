using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class CarInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
}

public class carController : MonoBehaviour
{
    public List<CarInfo> CarInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;

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
                else if (Input.GetAxisRaw("Vertical") > 0)
                {
                    float motor = maxMotorTorque * Input.GetAxisRaw("Vertical");
                    carInfo.leftWheel.brakeTorque = 0;
                    carInfo.rightWheel.brakeTorque = 0;
                    carInfo.leftWheel.motorTorque = motor;
                    carInfo.rightWheel.motorTorque = motor;
                }
                else if (Input.GetAxisRaw("Vertical") < 0)
                {
                    float brakeTorque = maxMotorTorque * Input.GetAxisRaw("Vertical") * 4;
                    carInfo.leftWheel.brakeTorque = brakeTorque;
                    carInfo.rightWheel.brakeTorque = brakeTorque;
                }
            }
            applyColliderRotationToWheelObject(carInfo.leftWheel);
            applyColliderRotationToWheelObject(carInfo.rightWheel);
        }
    }
}