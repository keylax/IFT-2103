using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class aiCarController : MonoBehaviour {

    public Vector3 startingPosition;
    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public WheelCollider rearLeftWheel;
    public WheelCollider rearRightWheel;

    private Transform path;
    private List<Transform> nodes;
    private int currectNode = 0;
    private float maxMotorTorque = 350f;
    private float maxSpeed = 30f;
    private float maxBrakeTorque = 600f;
    private float maxSteeringAngle = 45f;
    private float detectionRaycastsLength = 10;
    private float frontSideDetectorPos = 2f;
    private Vector3 frontDetectorPos = new Vector3(0f, 0.5f, 2f);
    private Vector3 initialPosition;
    private bool avoiding = false;
    private bool isBraking = false;
    private float targetSteerAngle = 0;
    private float turnSpeed = 5f;

    private void Start () {
        path = GameObject.Find("Path").transform;
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++) {
            if (pathTransforms[i] != path) {
                nodes.Add(pathTransforms[i]);
            }
        }

        initialPosition = transform.position;
    }

    public void reset()
    {


        frontLeftWheel.steerAngle = 0;
        frontRightWheel.steerAngle = 0;
        frontLeftWheel.motorTorque = 0;
        frontRightWheel.motorTorque = 0;
        rearLeftWheel.motorTorque = 0;
        rearLeftWheel.motorTorque = 0;

        transform.position = initialPosition;
        transform.eulerAngles = new Vector3(0, 90, 0);

        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private void FixedUpdate () {
        checkCollisions();
        steer();
        drive();
        checkCurrentWaypoint();
        braking();
        lerpToSteerAngle();
	}

    private void steer() {
        if (avoiding) return;
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currectNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteeringAngle;
        targetSteerAngle = newSteer;
    }

    private void drive() {
        float currentSpeed = 2 * Mathf.PI * frontLeftWheel.radius * frontLeftWheel.rpm * 60 / 1000;
        Debug.Log(currentSpeed);
        if (currentSpeed < maxSpeed)
        {
            frontLeftWheel.motorTorque = maxMotorTorque;
            frontRightWheel.motorTorque = maxMotorTorque;
            isBraking = false;
        }
        else
        {
            frontLeftWheel.motorTorque = 0;
            frontRightWheel.motorTorque = 0;
            isBraking = true;
        }
    }

    private void braking()
    {
        if (isBraking)
        {
            rearRightWheel.brakeTorque = maxBrakeTorque;
            rearLeftWheel.brakeTorque = maxBrakeTorque;
        } else
        {
            rearRightWheel.brakeTorque = 0;
            rearLeftWheel.brakeTorque = 0;
        }
    }

    private void checkCollisions()
    {
        RaycastHit hit;
        Vector3 detectorStartingPos = transform.position;
        detectorStartingPos += transform.forward * 2f;
        float avoidMultiplier = 0;
        avoiding = false;

        if (Physics.Raycast(detectorStartingPos, transform.forward, out hit, detectionRaycastsLength * 2))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                Debug.DrawLine(detectorStartingPos, hit.point);
                avoiding = true;
                if (hit.point.z < hit.collider.gameObject.transform.position.z)
                {
                    avoidMultiplier = 1;
                }
                else
                {
                    avoidMultiplier = -1;
                }
            } else if (hit.collider.CompareTag("Player"))
            {
                avoiding = true;
                if (hit.point.z < hit.collider.gameObject.transform.position.z)
                {
                    avoidMultiplier = 1;
                }
                else
                {

                    avoidMultiplier = -1;
                }
            }
        }
        else if (Physics.Raycast(detectorStartingPos, Quaternion.AngleAxis(15, transform.up) * transform.forward, out hit, detectionRaycastsLength))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                avoiding = true;
                avoidMultiplier -= 0.5f;
            } else if (hit.collider.CompareTag("Player"))
            {
                avoiding = true;
                avoidMultiplier += 0.5f;
            }
        }
        else if (Physics.Raycast(detectorStartingPos, Quaternion.AngleAxis(30, transform.up) * transform.forward, out hit, detectionRaycastsLength))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                avoiding = true;
                avoidMultiplier -= 0.5f;
            }  else if (hit.collider.CompareTag("Player"))
            {
                avoiding = true;
                avoidMultiplier += 0.5f;
            }
        }
        else if (Physics.Raycast(detectorStartingPos, Quaternion.AngleAxis(-15, transform.up) * transform.forward, out hit, detectionRaycastsLength))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                avoiding = true;
                avoidMultiplier += 0.5f;
            } else if (hit.collider.CompareTag("Player"))
            {
                avoiding = true;
                avoidMultiplier += 0.5f;
            }
        }
        else if (Physics.Raycast(detectorStartingPos, Quaternion.AngleAxis(-30, transform.up) * transform.forward, out hit, detectionRaycastsLength))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                avoiding = true;
                avoidMultiplier += 0.5f;
            } else if (hit.collider.CompareTag("Player"))
            {
                avoiding = true;
                avoidMultiplier += 0.5f;
            }
        }

        if (avoiding)
        {
            frontLeftWheel.steerAngle = maxSteeringAngle * avoidMultiplier;
            frontRightWheel.steerAngle = maxSteeringAngle * avoidMultiplier;
        }
    }

    private void checkCurrentWaypoint() {
        float distanceFromCarToStartingPosition = Vector3.Distance(transform.position, startingPosition);
        float distanceFromCurrentNodeToStartingPosition = Vector3.Distance(nodes[currectNode].position, startingPosition);
        if (distanceFromCarToStartingPosition > distanceFromCurrentNodeToStartingPosition)
        {
            if (currectNode < nodes.Count - 1)
            {
                currectNode++;
            }
        }
    }

    private void rotateWheelsMesh()
    {
        Transform visualLeftWheel = frontLeftWheel.transform.GetChild(0);
        Transform visualRightWheel = frontLeftWheel.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        frontLeftWheel.GetWorldPose(out position, out rotation);
        visualLeftWheel.transform.rotation = rotation;
        frontRightWheel.GetWorldPose(out position, out rotation);
        visualRightWheel.transform.rotation = rotation;
    }

    private void lerpToSteerAngle()
    {
        frontLeftWheel.steerAngle = Mathf.Lerp(frontLeftWheel.steerAngle, targetSteerAngle, Time.deltaTime * turnSpeed);
        frontRightWheel.steerAngle = Mathf.Lerp(frontRightWheel.steerAngle, targetSteerAngle, Time.deltaTime * turnSpeed);
    }
}
