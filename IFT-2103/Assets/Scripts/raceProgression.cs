using UnityEngine;
using UnityEngine.UI;

public class raceProgression : MonoBehaviour
{

    public Slider slider;
    public GameObject finishLine;
    public GameObject playerCar;

    private Vector3 initialPlayerPosition;
    private Vector3 finishLinePosition;
    private float initialDistanceWithFinishLine;

    // Use this for initialization
    void Start()
    {
        initialPlayerPosition =  new Vector3(playerCar.transform.position.x, 0 , playerCar.transform.position.z);
        finishLinePosition = new Vector3(finishLine.transform.position.x, 0, finishLine.transform.position.z);
        initialDistanceWithFinishLine = Vector3.Distance(initialPlayerPosition, finishLinePosition);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPlayerPosition = new Vector3(playerCar.transform.position.x, 0, playerCar.transform.position.z);
        float distanceWithFinishLine = Vector3.Distance(currentPlayerPosition, finishLinePosition);
        float progressInPercentage = distanceWithFinishLine / initialDistanceWithFinishLine;
        slider.value = 1 - progressInPercentage;
    }
}
