using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.CharacterController;

public class CharacterAnimationsManager : MonoBehaviour {
    public AnimationClip aniIdle;
    public AnimationClip aniRun;
    public AnimationClip aniJump;

    private bool jumped;
    private vThirdPersonController characterController;

	// Use this for initialization
	void Start () {
        characterController = gameObject.GetComponent<vThirdPersonController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (characterController.isGrounded)
        {
            if (characterController.isRunning)
            {
                GetComponent<Animation>().CrossFade(aniRun.name);
            } else
            {
                GetComponent<Animation>().CrossFade(aniIdle.name);
            }
        }

        if (characterController.isJumping)
        {
            playJumpSound();
            GetComponent<Animation>().CrossFade(aniJump.name);
        }
	}

    private void playJumpSound()
    {
        if (!GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().Play();
        }
    }
}
