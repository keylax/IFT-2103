using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks {

    private string gameVersion = "1";
    [SerializeField]
    private byte maxPlayersPerRoom = 2;
    public Transform SpawnerHost;
    public Transform SpawnerClient;
    private string roomName = "room 1";
    public string player = "PlayerCar";
    public GameObject Cars;
    private gameInitializer gameInitialize;
    public GameObject WaitingForPlayersMenu;

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

    public override void OnConnectedToMaster()
    {
        RoomOptions roomOption = new RoomOptions();
        roomOption.IsVisible = false;
        roomOption.MaxPlayers = maxPlayersPerRoom;
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOption, TypedLobby.Default);
    }


    public override void OnDisconnected(DisconnectCause cause)
    {
    }


    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
        PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    public override void OnJoinedRoom()
    {
        GameObject player1Car;

        if (PhotonNetwork.IsMasterClient == true)
        {
            player1Car = PhotonNetwork.Instantiate(player, SpawnerHost.position, SpawnerHost.rotation, 0);
        }
        else
        {
            player1Car = PhotonNetwork.Instantiate(player, SpawnerClient.position, SpawnerClient.rotation, 0);
        }
        player1Car.transform.SetParent(Cars.transform);
        player1Car.GetComponent<carController>().setControls(gameInitialize.getCurrentController());
        player1Car.GetComponent<carController>().setMainMenu(gameInitialize.mainMenu);
        player1Car.GetComponent<carController>().setEnableCommand(false);
        gameInitialize.instanciateCamera(player1Car.transform);
        gameInitialize.instanciateFinishLine();
        WaitingForPlayersMenu.GetComponentInChildren<Image>().color = new Color(0f, 0f, 0f, 0.5f);
        
        if (PhotonNetwork.PlayerList.Length == maxPlayersPerRoom)
        {
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("StartGame", RpcTarget.Others);
            WaitingForPlayersMenu.SetActive(false);
            GameObject HUDCanvas = GameObject.Find("HUDCanvas");
            player1Car.GetComponent<carController>().setEnableCommand(true);
            HUDCanvas.GetComponentInChildren<DelayedStart>().StartRace();
            player1Car.GetComponent<carController>().SetCountDown();
        }

    }

    [PunRPC]
    void StartGame()
    {
        WaitingForPlayersMenu.SetActive(false);
        var controller = Cars.GetComponentsInChildren<carController>();
        for (int i = 0; i < controller.Length; i++)
        {
            controller[i].setEnableCommand(true);
        }
        GameObject HUDCanvas = GameObject.Find("HUDCanvas");
        HUDCanvas.GetComponentInChildren<DelayedStart>().StartRace();
        for (int i = 0; i < controller.Length; i++)
        {
            controller[i].GetComponent<carController>().SetCountDown();
        }
    }

}
