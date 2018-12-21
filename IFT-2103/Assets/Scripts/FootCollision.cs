using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootCollision : MonoBehaviour {

    public float baseFootAudioVolume = 1.0f;
    public float soundEffectPitchRandomness = 0.05f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {                                                             
        GroundCollisionParticleEffect collisionParticleEffect = other.GetComponent<GroundCollisionParticleEffect>();      

        if (collisionParticleEffect)                                                                       
        {
            Instantiate(collisionParticleEffect.effect, transform.position, transform.rotation);     
        }

        GroundCollisionSoundEffect collisionSoundEffect = other.GetComponent<GroundCollisionSoundEffect>();          

        if (collisionSoundEffect)                                                                         
        {                               
            GetComponent<AudioSource>().volume = collisionSoundEffect.volumeModifier * baseFootAudioVolume;                           
            GetComponent<AudioSource>().pitch = Random.Range(1.0f - soundEffectPitchRandomness, 1.0f + soundEffectPitchRandomness);   
            GetComponent<AudioSource>().PlayOneShot(collisionSoundEffect.audioClip);                                                                                     
        }
    }

    void Reset()
    {
        GetComponent<Rigidbody>().isKinematic = true;                                                                            
        GetComponent<Collider>().isTrigger = true;
    }
}