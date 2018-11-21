﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameInitializer : MonoBehaviour {
    //Pour ne pas etre obligé de passer par le menu à chaque fois que je veux tester la scene. À ENLEVER
    //########################################################################################
    private GameMode debugGameMode = GameMode.OFFLINE_MP;
    //########################################################################################

    public Transform allCars;
    public GameObject mainMenu;

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
        }
        instanciateFinishLine();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void initializeVSAI()
    {
        Transform player1Car = instanciatePlayer(playerPrefab, spawnPoint1.position);
        Transform player1Camera = instanciateCamera(player1Car);

        Transform aiCar = instanciatePlayer(aiPrefab, spawnPoint2.position);
    }
    
    private void initializeOfflineMP()
    {
        Transform player1Car = instanciatePlayer(playerPrefab, spawnPoint1.position);
        Transform player1Camera = instanciateCamera(player1Car);

        Transform player2Car = instanciatePlayer(playerPrefab, spawnPoint2.position);
        Transform player2Camera = instanciateCamera(player2Car);

        splitScreen(player1Camera, player2Camera);
    }

    private void instanciateFinishLine()
    {
        finishLinePrefab.GetComponent<raceManager>().allCars = allCars.gameObject;
        finishLinePrefab.GetComponent<raceManager>().progressSlider = progressSlider;
        finishLinePrefab.GetComponent<raceManager>().positionText = positionText;
        finishLinePrefab.GetComponent<raceManager>().winRaceCanvas = winRaceCanvas;
        Transform finishLine = Instantiate(finishLinePrefab, finishLinePrefab.position, finishLinePrefab.rotation);
    }

    private void splitScreen(Transform player1Camera, Transform player2Camera) 
    {
        player1Camera.GetComponent<Camera>().rect = new Rect(0, 0.5f, 1, 0.5f);
        player2Camera.GetComponent<Camera>().rect = new Rect(0, 0, 1, 0.5f);
    }

    private Transform instanciatePlayer(Transform carPrefab, Vector3 spawnPoint)
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

    private Transform instanciateCamera(Transform targetPlayer)
    {
        Transform playerCamera = Instantiate(cameraPrefab, cameraPrefab.position, cameraPrefab.rotation);
        playerCamera.GetComponent<cameraFollow>().target = targetPlayer;

        return playerCamera;
    }
}