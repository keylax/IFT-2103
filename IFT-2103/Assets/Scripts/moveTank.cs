using UnityEngine;

public class moveTank : MonoBehaviour {

    float forwardRate = 15;
    float turnRate = 3;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {

        //tank forward speed in action
        float forwardMoveAmount = Input.GetAxis("Vertical") * forwardRate;

        //force of the tank's turn
        float turnForce = Input.GetAxis("Horizontal") * turnRate;

        //rotate tank in action
        transform.Rotate(0, turnForce, 0);

        //Move tank
        transform.position += transform.forward * forwardMoveAmount * Time.deltaTime;
	}

    public void reset()
    {
        transform.position = new Vector3(0, 1.5f, 0);
        transform.eulerAngles = new Vector3(0, 0, 0);
    }
}