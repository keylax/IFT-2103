using UnityEngine;

public class musicVolumeManager : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<AudioSource>().volume = gameParameters.getMusicVolume();
	}
}
