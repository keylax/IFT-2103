using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetBehaviour : MonoBehaviour {

    private Material hitmarkerMaterial;

	// Use this for initialization
	void Start () {
        hitmarkerMaterial = transform.GetChild(0).gameObject.GetComponent<Renderer>().material;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void reset()
    {
        hitmarkerMaterial.color = Color.red;
    }

    public void onCollision()
    {
        hitmarkerMaterial.color = Color.green;
    }
}
