using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Assets.Scripts.Controls;

public class NetworkManager : MonoBehaviourPunCallbacks {

    string gameVersion = "1";
    [SerializeField]
    private byte maxPlayersPerRoom = 4;
    public Transform SpawnerHost;
    public Transform SpawnerClient;
    private string roomName = "room 1";
    private string VERSION = "v.0.0.1";
    public string player = "BasicCarRed";
    public Component controls;
    public GameObject Cars;
    public GameObject playerPrefab;
    private int players = 0;
    private gameInitializer gameInitialize;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

	}

    void Awake() {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void Connect(gameInitializer gameInit)
    {
        gameInitialize = gameInit;
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log(PhotonNetwork.EnableLobbyStatistics);
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    #region MonoBehaviourPunCallbacks Callbacks


    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN");
        RoomOptions roomOption = new RoomOptions();
        roomOption.IsVisible = false;
        roomOption.MaxPlayers = maxPlayersPerRoom;
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOption, TypedLobby.Default);
    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
    }


    #endregion

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
        PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    public int getPlayersConnected()
    {
        return players;
    }

    public override void OnJoinedRoom()
    {
        GameObject player1Car;


        if (PhotonNetwork.IsMasterClient == true)
        {
            player1Car = PhotonNetwork.Instantiate(player, SpawnerHost.position, SpawnerHost.rotation, 0);
            //player1Car = gameInitialize.instanciatePlayer(playerPrefab.transform, SpawnerHost.position);
            player1Car.transform.SetParent(Cars.transform);
            player1Car.GetComponent<carController>().setControls(gameInitialize.getCurrentController());
            player1Car.GetComponent<carController>().setMainMenu(gameInitialize.mainMenu);
            gameInitialize.instanciateCamera(player1Car.transform);
        }
        else
        {
            player1Car = PhotonNetwork.Instantiate(player, SpawnerClient.position, SpawnerClient.rotation, 0);
            //player1Car = gameInitialize.instanciatePlayer(playerPrefab.transform, SpawnerClient.position);
            player1Car.GetComponent<carController>().setControls(gameInitialize.getCurrentController());
            player1Car.transform.SetParent(Cars.transform);
            player1Car.GetComponent<carController>().setMainMenu(gameInitialize.mainMenu);
            gameInitialize.instanciateCamera(player1Car.transform);
        }
        gameInitialize.instanciateFinishLine();

        /*   
         *   var camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<cameraFollow>();

        camera.target = playerCar.transform;
        playerCar.transform.parent = Cars.transform;
        playerCar.GetComponent<carController>().setControls(new ZQSDControls());
        playerCar.GetComponent<carController>().setMainMenu(GameObject.FindGameObjectWithTag("Menu"));
        */
    }

}
