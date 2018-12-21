using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public Transform marioPrefab;
    public Transform playerCameraPrefab;
    public Transform coinPrefab;
    public Transform starPrefab;
    public Transform terrainPrefab;
    public Vector2 terrainOffsets = new Vector2(100f,100f);
    public Vector2 terrainSize;
    public bool terrainRandomOffsets;
    public int numberOfCoins;

	// Use this for initialization
	void Start () {
        terrainSize = new Vector2(terrainPrefab.GetComponent<TerrainGeneration>().width, terrainPrefab.GetComponent<TerrainGeneration>().height);
        Transform terrain = instanciateTerrain();
        terrain.GetComponent<TerrainGeneration>().Start();
        float[,] heights = terrain.GetComponent<TerrainGeneration>().getHeights();
        instanciatePlayer();
        instanciateCoins(numberOfCoins, heights);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private Transform instanciateTerrain()
    {
        terrainPrefab.GetComponent<TerrainGeneration>().offsetX = terrainOffsets.x;
        terrainPrefab.GetComponent<TerrainGeneration>().offsetY = terrainOffsets.y;
        terrainPrefab.GetComponent<TerrainGeneration>().randomOffsets = terrainRandomOffsets;
        return Instantiate(terrainPrefab, new Vector3(0,0,0), terrainPrefab.rotation);
    }

    private void instanciatePlayer()
    {
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

            Instantiate(coinPrefab, new Vector3(posX, mapHeights[(int)posX, (int)posZ] + 1, posZ), coinPrefab.rotation);
        }
    }
}
