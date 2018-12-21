using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollisionSoundEffect : MonoBehaviour {

    public AudioClip audioClip;
    public float volumeModifier;

    // Use this for initialization
    void Start () {
        volumeModifier = gameParameters.getVolume();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
