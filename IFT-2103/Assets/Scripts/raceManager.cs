using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Photon.Pun;

public class raceManager : MonoBehaviour
{

    public GameObject allCars;

    public Slider progressSlider;
    public Text positionText;

    public GameObject finishLine;
    public GameObject winRaceCanvas;
    public GameObject loseRaceCanvas;
    public GameObject endOfGameMenuOfflineMP;

    private Vector3 initialRacePosition;
    private Vector3 finishLinePosition;
    private float initialDistanceWithFinishLine;

    private List<KeyValuePair<int, float>> listOfDistances;
    private int playerPositionIndex;

    void Start()
    {
        initialRacePosition = new Vector3(allCars.transform.GetChild(0).transform.position.x, 0, allCars.transform.GetChild(0).transform.position.z);
        finishLinePosition = new Vector3(finishLine.transform.position.x, 0, finishLine.transform.position.z);
        initialDistanceWithFinishLine = Vector3.Distance(initialRacePosition, finishLinePosition);

        listOfDistances = new List<KeyValuePair<int, float>>();

        for (int carsIndex = 0; carsIndex < allCars.transform.childCount; carsIndex++)
        {
            listOfDistances.Insert(carsIndex, new KeyValuePair<int, float>(carsIndex, float.MaxValue));
        }
    }

    void Update()
    {
        for (int carsIndex = 0; carsIndex < allCars.transform.childCount; carsIndex++)
        {
            Transform currentCarTransform = allCars.transform.GetChild(carsIndex);

            Vector3 currentPosition = new Vector3(currentCarTransform.transform.position.x, 0, currentCarTransform.transform.position.z);
            float distanceWithFinishLine = Vector3.Distance(currentPosition, finishLinePosition);

            if (currentCarTransform.tag == "Player")
            {
                playerPositionIndex = carsIndex;
                float progressInPercentage = distanceWithFinishLine / initialDistanceWithFinishLine;
                progressSlider.value = 1 - progressInPercentage;
            }

            listOfDistances[carsIndex] = new KeyValuePair<int, float>(carsIndex, distanceWithFinishLine);
        }

        listOfDistances.Sort((x, y) => x.Value.CompareTo(y.Value));

        for (int posIndex = 0; posIndex < listOfDistances.Count; posIndex++)
        {
            if (listOfDistances[posIndex].Key == playerPositionIndex)
            {
                positionText.text = "POSITION:" + (posIndex + 1).ToString();
            }
        }
    }

    public void OnTriggerEnter(Collider trigger)
    {
        if (GameParameters.getGameMode() == GameMode.VERSUS_AI || GameParameters.getGameMode() == GameMode.ONLINE_MP)
        {
                if (trigger.CompareTag("Player"))
                {
                    if (PhotonNetwork.IsConnected && PhotonNetwork.LocalPlayer.IsLocal)
                    {
                        PhotonView photonView = PhotonView.Get(this);
                        photonView.RPC("EndGame", RpcTarget.Others);
                    }
                    GameObject HUDCanvas = GameObject.Find("HUDCanvas");
                    HUDCanvas.SetActive(false);
                    winRaceCanvas.SetActive(true);
                }
                else
                {
                    GameObject HUDCanvas = GameObject.Find("HUDCanvas");
                    HUDCanvas.SetActive(false);
                    loseRaceCanvas.SetActive(true);
                }
        } else
        {
            GameObject HUDCanvas = GameObject.Find("HUDCanvas");
            HUDCanvas.SetActive(false);
            endOfGameMenuOfflineMP.SetActive(true);
            GameObject winMsg = GameObject.Find("winMsg");
            if (trigger.transform == allCars.transform.GetChild(0).GetChild(0))
            {
                winMsg.GetComponent<Text>().text = "Player 1 wins";
            } else
            {
                winMsg.GetComponent<Text>().text = "Player 2 wins";
            }
        }
        
    }
    
    [PunRPC]
    void EndGame()
    {
        GameObject HUDCanvas = GameObject.Find("HUDCanvas");
        HUDCanvas.SetActive(false);
        loseRaceCanvas.SetActive(true);
    }
}