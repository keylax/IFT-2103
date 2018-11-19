using UnityEngine;

public class raceManager : MonoBehaviour {

    public GameObject allCars;

    // Use this for initialization
    void Start () {
		
	}

    public void OnTriggerEnter(Collider trigger)
    {
        if(trigger.tag == "Player")
        {
            for (int i = 0; i < allCars.transform.childCount; i++)
            {
                if (allCars.transform.GetChild(i).GetComponent<aiCarController>())
                {
                    allCars.transform.GetChild(i).GetComponent<aiCarController>().reset();
                }
                else if (allCars.transform.GetChild(i).GetComponent<carController>())
                {
                    allCars.transform.GetChild(i).GetComponent<carController>().reset();
                }
            }
        }
    }
}
