using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSoundsManager : MonoBehaviour {
    public AudioClip jump;
    private AudioSource audioSource;
    private string playingSFX = "";
    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!audioSource.isPlaying)
        {
            playingSFX = "";
        }
	}

    public void playJumpSFX()
    {
        if ((audioSource.isPlaying && playingSFX != jump.name) || !audioSource.isPlaying)
        {
            playSFX(jump);
        } 
    }

    public void playSFX(AudioClip clip)
    {
        audioSource.volume = gameParameters.getVolume();
        audioSource.PlayOneShot(clip);
        playingSFX = clip.name;
    }
}
