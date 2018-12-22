using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour {
    public float rotationSpeed = 1.5f;
    public AudioClip pickupClip;
    private Transform gameController;
    private Vector3 startpos;
    private const float BOBBING_AMOUNT = 0.5f;

    // Use this for initialization
    void Start () {
        startpos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, rotationSpeed, 0));
        if (transform.CompareTag("star"))
        {
            transform.position = new Vector3(transform.position.x, startpos.y + (BOBBING_AMOUNT * Mathf.Sin(Time.time * BOBBING_AMOUNT)), transform.position.z);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<CharacterSoundsManager>().playSFX(pickupClip);
            if (transform.CompareTag("coin"))
            {
                gameController.GetComponent<GameController>().coinCollected();
            } else if (transform.CompareTag("star"))
            {
                gameController.GetComponent<GameController>().starCollected();
            }
            Destroy(gameObject);
        }
    }

    public void setObserver(Transform controller)
    {
        gameController = controller;
    }
}
