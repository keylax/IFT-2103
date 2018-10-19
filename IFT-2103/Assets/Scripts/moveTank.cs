using UnityEngine;

public class moveTank : MonoBehaviour {

    float forwardRate = 10;
    float turnRate = 1;
    bool collidingFront = false;
    bool collidingBack = false;
    bool collidingLeft = false;
    bool collidingRight = false;
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
        if ((collidingFront && forwardMoveAmount < 0) || (collidingBack && forwardMoveAmount > 0) || (!collidingBack && !collidingFront && !collidingLeft && !collidingRight)){
            transform.position += transform.forward * forwardMoveAmount * Time.deltaTime;
        }
	}

    public void reset()
    {
        transform.position = new Vector3(17, 0.7f, -10);
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

    public void collisionAtLeft()
    {
        collidingLeft = true;
    }

    public void collisionAtRight()
    {
        collidingRight = true;
    }
}