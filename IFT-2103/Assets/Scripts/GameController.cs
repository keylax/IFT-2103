using System.Collections;
using System.Collections.Generic;
using Invector.CharacterController;
using UnityEngine;

public class GameController : MonoBehaviour {
    public Transform marioPrefab;
    public Transform playerCameraPrefab;
    public Transform coinPrefab;
    public Transform starPrefab;
    public Transform terrainPrefab;
    public Transform wallsPrefab;
    public Transform treePrefab;
    public Transform inGameMenu;
    public Vector2 terrainOffsets = new Vector2(100f,100f);
    public bool terrainRandomOffsets;
    public int numberOfCoins = 10;
    private Vector2 terrainSize;
    private int numberOfCoinsLeft;
    private PoissonDiscSampler poissonDiscSampler;
    private List<Vector2> treePositions = new List<Vector2>();

	// Use this for initialization
	void Start () {
        numberOfCoinsLeft = numberOfCoins;
        terrainSize = new Vector2(terrainPrefab.GetComponent<TerrainGeneration>().width, terrainPrefab.GetComponent<TerrainGeneration>().height);
        Transform terrain = instanciateTerrain();
        terrain.GetComponent<TerrainGeneration>().Start();
        float[,] heights = terrain.GetComponent<TerrainGeneration>().getHeights();
        poissonDiscSampler = new PoissonDiscSampler(terrainSize.x, terrainSize.y, 10);
        instanciateTrees();
        instanciatePlayer();
        instanciateCoins(numberOfCoins, heights);
	}
	
	// Update is called once per frame
	void Update () {
		if (numberOfCoinsLeft == 0)
        {

        }
	}

    private Transform instanciateTerrain()
    {
        terrainPrefab.GetComponent<TerrainGeneration>().offsetX = terrainOffsets.x;
        terrainPrefab.GetComponent<TerrainGeneration>().offsetY = terrainOffsets.y;
        terrainPrefab.GetComponent<TerrainGeneration>().randomOffsets = terrainRandomOffsets;
        Instantiate(wallsPrefab, wallsPrefab.position, wallsPrefab.rotation);
        return Instantiate(terrainPrefab, new Vector3(0,0,0), terrainPrefab.rotation);
    }

    private void instanciatePlayer()
    {
        marioPrefab.GetComponent<vThirdPersonInput>().InGameMenu = inGameMenu.gameObject;
        Transform playerTransform = Instantiate(marioPrefab, new Vector3(terrainSize.x / 2, 1, terrainSize.y / 2), marioPrefab.rotation);
        playerCameraPrefab.GetComponent<vThirdPersonCamera>().SetMainTarget(playerTransform);
        Transform cameraTransform = Instantiate(playerCameraPrefab, new Vector3(0, 0, 0), playerCameraPrefab.rotation);
    }

    private void instanciateCoins(int nbrOfCoins, float[,] mapHeights)
    {
        for (int i = 0; i < nbrOfCoins; i++)
        {
            float posX = Random.Range(0, terrainSize.x);
            float posZ = Random.Range(0, terrainSize.y);
            Vector2 possibleCoinPosition = new Vector2(posX, posZ);
            while (!isValidCoinPosition(possibleCoinPosition))
            {
                posX = Random.Range(0, terrainSize.x);
                posZ = Random.Range(0, terrainSize.y);
            }

            Instantiate(coinPrefab, new Vector3(posX, mapHeights[(int)posX, (int)posZ] + 1, posZ), coinPrefab.rotation);
        }
    }

    private bool isValidCoinPosition(Vector2 position)
    {
        foreach (Vector2 treePosition in treePositions)
        {
            if (Vector2.Distance(position, treePosition) < 1)
            {
                return false;
            }
        }
        return true;
    }

    private void instanciateTrees()
    {
        foreach (Vector2 sample in poissonDiscSampler.Samples()) {
            treePositions.Add(sample);
            Instantiate(treePrefab, new Vector3(sample.x, 0, sample.y), treePrefab.rotation);
        }
    }

    public void coinCollected()
    {
        numberOfCoinsLeft -= 1;
    }
}
