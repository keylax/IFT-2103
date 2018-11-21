using UnityEngine;

public class raceManager : MonoBehaviour {

    public GameObject player;

    // Use this for initialization
    void Start () {
		
	}

    public void OnTriggerEnter(Collider trigger)
    {
        if (trigger.tag == "Player")
        {
            player.GetComponent<carController>().reset();
        }
    }
}
