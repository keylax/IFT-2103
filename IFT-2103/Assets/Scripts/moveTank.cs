using UnityEngine;

public class moveTank : MonoBehaviour {

    float forwardRate = 15;
    float turnRate = 3;
    bool collidingFront = false;
    bool collidingBack = false;
    float forwardMoveAmount;
    float turnForce;

    // Use this for initialization
    void Start () {}
	
	// Update is called once per frame
	void Update () {

        //tank forward speed in action
        forwardMoveAmount = Input.GetAxis("Vertical") * forwardRate;
 
        //force of the tank's turn
        turnForce = Input.GetAxis("Horizontal") * turnRate;

        //rotate tank in action
        transform.Rotate(0, turnForce, 0);

        //Move tank
        if ((collidingFront && forwardMoveAmount < 0) || (collidingBack && forwardMoveAmount > 0) || (!collidingBack && !collidingFront)){
            transform.position += transform.forward * forwardMoveAmount * Time.deltaTime;
        }
	}

    public void reset()
    {
        transform.position = new Vector3(0, 1.5f, 0);
        transform.eulerAngles = new Vector3(0, 0, 0);
    }

    public void resetCollision()
    {
        collidingFront = false;
        collidingBack = false;
    }

    public void collisionInFront()
    {
        collidingFront = true;
    }

    public void collisionInBack()
    {
        collidingBack = true;
    }
}