using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemController : MonoBehaviour {
    private Transform transformToFollow;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (!transformToFollow)
        {
            transformToFollow = gameObject.GetComponentInParent<Transform>();
        }
        transform.position = transformToFollow.position;
	}

    public void setPosition(Transform position)
    {
        transformToFollow = position;
    }
}
