using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;
using Assets.Scripts.Controls;
using Photon.Pun;
using Photon.Realtime;

public class gameInitializer : MonoBehaviour {
    //Pour ne pas etre obligé d'avoir tous le menu d'implémenter pour tester la scene. À ENLEVER
    //########################################################################################
    private GameMode debugGameMode = GameMode.ONLINE_MP;
    //Les controls vont venir de gameparameters après avoir été set à partir du menu
    private ControlScheme player1ControlScheme = new ZQSDControls();
    private ControlScheme player2ControlScheme = new ArrowsControls();
    //########################################################################################

    public Transform allCars;
    public GameObject mainMenu;
    public GameObject WaitingForPlayersMenu;

    [HeaderAttribute("Spawn Points")]
    public Transform spawnPoint1;
    public Transform spawnPoint2;

    [HeaderAttribute("Prefabs")]
    public Transform playerPrefab;
    public Transform aiPrefab;
    public Transform cameraPrefab;

    [HeaderAttribute("Finish line")]
    public Transform finishLinePrefab;
    public Slider progressSlider;
    public Text positionText;
    public GameObject winRaceCanvas;
    public GameObject loseRaceCanvas;


    // Use this for initialization
    void Start () {
        //À changer éventuellement pour GameParameters.getGameMode()
        switch (GameParameters.getGameMode())
        {
            case GameMode.VERSUS_AI:
                initializeVSAI();
                break;
            case GameMode.OFFLINE_MP:
                initializeOfflineMP();
                break;
            case GameMode.ONLINE_MP:
                initializeOnlineMP();
                break;
        }
     //   instanciateFinishLine();
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void initializeVSAI()
    {
        Transform player1Car = instanciatePlayer(playerPrefab, spawnPoint1.position);
        player1Car.GetComponent<carController>().SetControls(player1ControlScheme);
        Transform player1Camera = instanciateCamera(player1Car);

        Transform aiCar = instanciatePlayer(aiPrefab, spawnPoint2.position);
        instanciateFinishLine();
    }
    
    private void initializeOfflineMP()
    {
        Transform player1Car = instanciatePlayer(playerPrefab, spawnPoint1.position);
        player1Car.GetComponent<carController>().SetControls(player1ControlScheme);
        Transform player1Camera = instanciateCamera(player1Car);

        Transform player2Car = instanciatePlayer(playerPrefab, spawnPoint2.position);
        player2Car.GetComponent<carController>().SetControls(player2ControlScheme);
        Transform player2Camera = instanciateCamera(player2Car);

        splitScreen(player1Camera, player2Camera);
        instanciateFinishLine();
    }

    private void initializeOnlineMP()
    {
        WaitingForPlayersMenu.SetActive(true);
        GameObject.FindGameObjectWithTag("Network").GetComponent<NetworkManager>().Connect(this);
    }

    public void instanciateFinishLine()
    {
        finishLinePrefab.GetComponent<raceManager>().allCars = allCars.gameObject;
        finishLinePrefab.GetComponent<raceManager>().progressSlider = progressSlider;
        finishLinePrefab.GetComponent<raceManager>().positionText = positionText;
        finishLinePrefab.GetComponent<raceManager>().winRaceCanvas = winRaceCanvas;
        finishLinePrefab.GetComponent<raceManager>().loseRaceCanvas = loseRaceCanvas;
        Transform finishLine = Instantiate(finishLinePrefab, finishLinePrefab.position, finishLinePrefab.rotation);
        if (!PhotonNetwork.IsConnected)
        {
            GameObject HUDCanvas = GameObject.Find("HUDCanvas");
            HUDCanvas.GetComponentInChildren<DelayedStart>().StartRace();
        }
    }

    private void splitScreen(Transform player1Camera, Transform player2Camera) 
    {
        player1Camera.GetComponent<Camera>().rect = new Rect(0, 0.5f, 1, 0.5f);
        player2Camera.GetComponent<Camera>().rect = new Rect(0, 0, 1, 0.5f);
    }

    public Transform instanciatePlayer(Transform carPrefab, Vector3 spawnPoint)
    {
        Transform playerTransform = playerTransform = Instantiate(carPrefab, spawnPoint, playerPrefab.rotation);

        playerTransform.SetParent(allCars);
        playerTransform.localScale = new Vector3(1, 1, 1);

        if (carPrefab.tag == "Player")
        {
            playerTransform.GetComponent<carController>().mainMenu = mainMenu;
        }

        return playerTransform;
    }

    public Transform instanciateCamera(Transform targetPlayer)
    {
        Transform playerCamera = Instantiate(cameraPrefab, cameraPrefab.position, cameraPrefab.rotation);
        playerCamera.GetComponent<cameraFollow>().target = targetPlayer;

        return playerCamera;
    }

    public ControlScheme getCurrentController()
    {
        return player1ControlScheme;
    }
}
