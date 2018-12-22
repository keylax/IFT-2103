using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour {
    public Transform starParticle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void moveStarParticleToObject(Transform starObject)
    {
        starParticle.GetComponent<ParticleSystemController>().setPosition(starObject);
    }
}
