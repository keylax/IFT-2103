using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class NetworkManager : MonoBehaviourPunCallbacks {

    string gameVersion = "1";
    [SerializeField]
    private byte maxPlayersPerRoom = 4;
    public Transform SpawnerHost;
    public Transform SpawnerClient;
    private string roomName = "room 1";
    private string VERSION = "v.0.0.1";
    public string player = "BasicCarRed";
    public GameObject Cars;
    private int players = 0;

    // Use this for initialization
    void Start () {
       Connect();
    }
	
	// Update is called once per frame
	void Update () {

	}

    void Awake() {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void Connect()
    {
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
        Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

        // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
        PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    public override void OnJoinedRoom()
    {
        GameObject playerCar;
        if (PhotonNetwork.IsMasterClient == true)
        {
            playerCar = PhotonNetwork.Instantiate(player, SpawnerHost.position, SpawnerHost.rotation, 0);
        }
        else
        {
            playerCar = PhotonNetwork.Instantiate(player, SpawnerClient.position, SpawnerClient.rotation, 0);
        }
        var camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<cameraFollow>();
        camera.target = playerCar.transform;
        playerCar.transform.parent = Cars.transform;
    }

}
