using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour {
    public float rotationSpeed = 1.5f;
    public float volumeModifier = 1.0f;
    public AudioClip pickupClip;

	// Use this for initialization
	void Start () {
		volumeModifier = gameParameters.getVolume();
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, rotationSpeed, 0));
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<CharacterSoundsManager>().playSFX(pickupClip);
            Destroy(gameObject);
        }
    }
}
