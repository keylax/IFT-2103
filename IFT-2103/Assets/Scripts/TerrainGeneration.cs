using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour {
    public int width = 65;
    public int height = 65;
    public int depth = 1;
    public float scale = 12;
    public float offsetX = 100f;
    public float offsetY = 100f;
    public bool randomOffsets;
    float[,] mapHeights;

    // Use this for initialization
    public void Start () {
        if (randomOffsets) {
            offsetX = Random.Range(0f, 9999f);
            offsetY = Random.Range(0f, 9999f);
        }
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }
	
	// Update is called once per frame
	void Update () {
    }

    private TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);
        mapHeights = GenerateHeights();
        terrainData.SetHeights(0, 0, mapHeights);

        return terrainData;
    }

    private float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                heights[x, y] = CalculateHeight(x, y);
            }
        }

        return heights;
    }

    private float CalculateHeight(int x, int y)
    {
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }

    public float[,] getHeights()
    {
        return mapHeights;
    }
}
