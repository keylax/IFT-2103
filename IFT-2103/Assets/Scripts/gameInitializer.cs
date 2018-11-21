using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameInitializer : MonoBehaviour {
    //Pour ne pas etre obligé de passer par le menu à chaque fois que je veux tester la scene. À ENLEVER
    //########################################################################################
    private GameMode debugGameMode = GameMode.VERSUS_AI;
    //########################################################################################

    public Transform allCars;

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
        switch (debugGameMode)
        {
            case GameMode.VERSUS_AI:
                instanciatePlayerVSAI();
                break;
        }

        instanciateFinishLine();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void instanciatePlayerVSAI()
    {
        Transform player1Car = Instantiate(playerPrefab, spawnPoint1.position, playerPrefab.rotation);
        player1Car.SetParent(allCars);
        player1Car.localScale = new Vector3(1, 1, 1);

        Transform player1Camera = Instantiate(cameraPrefab, cameraPrefab.position, cameraPrefab.rotation);
        player1Camera.GetComponent<cameraFollow>().target = player1Car;

        Transform aiCar = Instantiate(aiPrefab, spawnPoint2.position, aiPrefab.rotation);
        aiCar.SetParent(allCars);
        aiCar.localScale = new Vector3(1, 1, 1);
    }

    private void instanciateFinishLine()
    {
        Transform finishLine = Instantiate(finishLinePrefab, finishLinePrefab.position, finishLinePrefab.rotation);
        finishLine.GetComponent<raceManager>().allCars = allCars.gameObject;
        finishLine.GetComponent<raceManager>().progressSlider = progressSlider;
        finishLine.GetComponent<raceManager>().positionText = positionText;
        finishLine.GetComponent<raceManager>().winRaceCanvas = winRaceCanvas;
    }
}
