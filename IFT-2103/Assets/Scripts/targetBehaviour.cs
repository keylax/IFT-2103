using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetBehaviour : MonoBehaviour {
    private Vector3 from;
    private Vector3 to ;
    private Material hitmarkerMaterial;
    private float speed;
    private bool wasHit;

	// Use this for initialization
	void Start () {
        hitmarkerMaterial = transform.GetChild(0).gameObject.GetComponent<Renderer>().material;
        from = new Vector3(transform.position.x - 10, transform.position.y, transform. position.z);
        to = new Vector3(transform.position.x + 10, transform.position.y, transform.position.z);
        reset();
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(from, to, Mathf.PingPong(Time.time, 1));
    }

    public void reset()
    {
        wasHit = false;
        hitmarkerMaterial.color = Color.red;
    }

    public void onCollision()
    {
        hitmarkerMaterial.color = Color.green;
        wasHit = true;
    }

    public bool wasTargetHit()
    {
        return wasHit;
    }
}
