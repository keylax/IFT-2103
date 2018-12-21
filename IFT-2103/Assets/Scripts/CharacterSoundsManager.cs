using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSoundsManager : MonoBehaviour {
    public AudioClip jump;
    private AudioClip currentAudioClip;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!GetComponent<AudioSource>().isPlaying && GetComponent<AudioSource>().clip != jump)
        {
            GetComponent<AudioSource>().clip = jump;
        }
	}

    public void setCurrentAudioClip(AudioClip clip)
    {
        currentAudioClip = clip;
    }
}
