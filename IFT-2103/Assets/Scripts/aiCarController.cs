using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class aiCarController : MonoBehaviour {

    public Transform path;
    public Vector3 startingPosition;
    public float maxSteeringAngle;
    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public WheelCollider rearLeftWheel;
    public WheelCollider rearRightWheel;
    public float maxMotorTorque = 300f;
    public float maxBrakeTorque = 600f;
    public bool isBraking = false;

    private List<Transform> nodes;
    private int currectNode = 0;

    [Header("CollisionDetectors")]
    public float detectionRaycastsLength = 25;
    public Vector3 frontDetectorPos = new Vector3(0f, 0.5f, 2f);
    public float frontSideDetectorPos = 2f;
    private bool avoiding = false;

    private void Start () {
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++) {
            if (pathTransforms[i] != path.transform) {
                nodes.Add(pathTransforms[i]);
            }
        }
    }
	
	private void FixedUpdate () {
        checkCollisions();
        steer();
        drive();
        checkCurrentWaypoint();
        braking();
	}

    private void steer() {
        if (avoiding) return;
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currectNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteeringAngle;
        frontLeftWheel.steerAngle = newSteer;
        frontRightWheel.steerAngle = newSteer;
        rotateWheelsMesh();
    }

    private void drive() {
        if (!isBraking)
        {
            frontLeftWheel.motorTorque = maxMotorTorque;
            frontRightWheel.motorTorque = maxMotorTorque;
        }
        else
        {
            frontLeftWheel.motorTorque = 0;
            frontRightWheel.motorTorque = 0;
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

        if (Physics.Raycast(detectorStartingPos, transform.forward, out hit, detectionRaycastsLength))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                Debug.DrawLine(detectorStartingPos, hit.point);
                avoiding = true;
                avoidMultiplier -= 1f;
            }
        }
        else if (Physics.Raycast(detectorStartingPos, Quaternion.AngleAxis(15, transform.up) * transform.forward, out hit, detectionRaycastsLength))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                Debug.DrawLine(detectorStartingPos, hit.point);
                avoiding = true;
                avoidMultiplier -= 0.5f;
            }
        }
        else if (Physics.Raycast(detectorStartingPos, Quaternion.AngleAxis(30, transform.up) * transform.forward, out hit, detectionRaycastsLength))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                Debug.DrawLine(detectorStartingPos, hit.point);
                avoiding = true;
                avoidMultiplier -= 0.5f;
            }
        }

        if (Physics.Raycast(detectorStartingPos, transform.forward, out hit, detectionRaycastsLength))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                Debug.DrawLine(detectorStartingPos, hit.point);
                avoiding = true;
                avoidMultiplier += 1f;
            }
        }
        else if (Physics.Raycast(detectorStartingPos, Quaternion.AngleAxis(-15, transform.up) * transform.forward, out hit, detectionRaycastsLength))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                Debug.DrawLine(detectorStartingPos, hit.point);
                avoiding = true;
                avoidMultiplier += 0.5f;
            }
        }
        else if (Physics.Raycast(detectorStartingPos, Quaternion.AngleAxis(-30, transform.up) * transform.forward, out hit, detectionRaycastsLength))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                Debug.DrawLine(detectorStartingPos, hit.point);
                avoiding = true;
                avoidMultiplier += 0.5f;
            }
        }

        if (avoidMultiplier == 0)
        {
            if (Physics.Raycast(detectorStartingPos, transform.forward, out hit, detectionRaycastsLength))
            {
                if (hit.collider.CompareTag("Obstacle"))
                {
                    Debug.DrawLine(detectorStartingPos, hit.point);
                    avoiding = true;
                    if (hit.normal.x < 0)
                    {
                        avoidMultiplier = -1;
                    }
                    else
                    {
                        avoidMultiplier = 1;
                    }
                }
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
}
