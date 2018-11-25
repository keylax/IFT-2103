using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using System;

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
    public GameObject mainMenu;

    public List<CarInfo> CarInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;

    private Vector3 initialPosition;
    private ControlScheme controls;
    private float stuckTimer = 0;
    private float timeToResetRotation = 2.0f;

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
        float steering = maxSteeringAngle * controls.getHorizontalAxis();

        foreach (CarInfo carInfo in CarInfos)

        {
            if (carInfo.steering)
            {
                carInfo.leftWheel.steerAngle = steering;
                carInfo.rightWheel.steerAngle = steering;
            }
            if (carInfo.motor)
            {
                if (controls.getVerticalAxis() == 0)
                {
                    carInfo.leftWheel.motorTorque = 0;
                    carInfo.rightWheel.motorTorque = 0;
                }
                else
                {
                    float motor = maxMotorTorque * controls.getVerticalAxis();

                    carInfo.leftWheel.motorTorque = motor;
                    carInfo.rightWheel.motorTorque = motor;
                }
            }
            applyColliderRotationToWheelObject(carInfo.leftWheel);
            applyColliderRotationToWheelObject(carInfo.rightWheel);
        }
        float carZRotation = transform.eulerAngles.z;
        if ((carZRotation < -140 && carZRotation > -220) || (carZRotation < 220 && carZRotation > 140))
        {
            stuckTimer += Time.deltaTime;

            if (stuckTimer > timeToResetRotation)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
            }
        } else
        {
            stuckTimer = 0;
        }
    }

    public void Update()
    {
        if (controls.getMenuButtonDown())
        {
            mainMenu.SetActive(true);
        }
    }

    public void setControls(ControlScheme controlsScheme)
    {
        controls = controlsScheme;
    }
}