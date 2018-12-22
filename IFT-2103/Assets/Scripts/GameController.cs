using System.Collections;
using System.Collections.Generic;
using Invector.CharacterController;
using UnityEngine.SceneManagement;
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
    public Transform particlePoolPrefab;
    public AudioClip starAppearsClip;
    public Vector2 terrainOffsets = new Vector2(100f,100f);
    public bool terrainRandomOffsets;
    public int numberOfCoins = 10;
    private Transform particlePool;
    private Transform player;
    private Vector2 terrainSize;
    private int numberOfCoinsLeft;
    private PoissonDiscSampler poissonDiscSampler;
    private List<Vector2> treePositions = new List<Vector2>();
    private float[,] mapHeights;

    // Use this for initialization
    void Start () {
        numberOfCoinsLeft = numberOfCoins;
        particlePool = Instantiate(particlePoolPrefab, new Vector3(100, 100, 100), particlePoolPrefab.rotation);
        terrainSize = new Vector2(terrainPrefab.GetComponent<TerrainGeneration>().width, terrainPrefab.GetComponent<TerrainGeneration>().height);
        Transform terrain = instanciateTerrain();
        terrain.GetComponent<TerrainGeneration>().Start();
        mapHeights = terrain.GetComponent<TerrainGeneration>().getHeights();
        poissonDiscSampler = new PoissonDiscSampler(terrainSize.x, terrainSize.y, 10);
        instanciateTrees();
        instanciatePlayer();
        instanciateCoins(numberOfCoins);
	}
	
	// Update is called once per frame
	void Update () {
		if (numberOfCoinsLeft == 0)
        {
            player.GetComponent<CharacterSoundsManager>().playSFX(starAppearsClip);
            instanciateStar();
            numberOfCoinsLeft = 1;
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
        player = Instantiate(marioPrefab, new Vector3(terrainSize.x / 2, 1, terrainSize.y / 2), marioPrefab.rotation);
        playerCameraPrefab.GetComponent<vThirdPersonCamera>().SetMainTarget(player);
        Transform cameraTransform = Instantiate(playerCameraPrefab, new Vector3(0, 0, 0), playerCameraPrefab.rotation);
    }

    private void instanciateCoins(int nbrOfCoins)
    {
        for (int i = 0; i < nbrOfCoins; i++)
        {
            float posX = Random.Range(1, terrainSize.x - 1);
            float posZ = Random.Range(1, terrainSize.y - 1);
            Vector2 possibleCoinPosition = new Vector2(posX, posZ);
            while (!isValidCoinPosition(possibleCoinPosition))
            {
                posX = Random.Range(1, terrainSize.x - 1);
                posZ = Random.Range(1, terrainSize.y - 1);
                possibleCoinPosition = new Vector2(posX, posZ);
            }
          
            Transform newCoin = Instantiate(coinPrefab, new Vector3(posX, mapHeights[(int)posX, (int)posZ] + 1, posZ), coinPrefab.rotation);
            newCoin.GetComponent<PickupItem>().setObserver(transform);
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

    private void instanciateStar()
    {
        float posX = terrainSize.x / 2;
        float posZ = terrainSize.y / 2;
        Transform newStar = Instantiate(starPrefab, new Vector3(posX, mapHeights[(int)posX, (int)posZ] + 1, posZ), starPrefab.rotation);
        particlePool.GetComponent<ParticlePool>().moveStarParticleToObject(newStar);
        newStar.GetComponent<PickupItem>().setObserver(transform);
    }

    public void coinCollected()
    {
        numberOfCoinsLeft -= 1;
    }

    public void starCollected()
    {
        StartCoroutine(endGame());
    }

    private IEnumerator endGame()
    {
        yield return new WaitForSeconds(3);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);
    }
}
