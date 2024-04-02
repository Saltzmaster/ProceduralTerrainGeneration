using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralTerrainGenerator : MonoBehaviour
{
    public Terrain terrain;
    public float treeDensity = 0.00002f; // Adjust density to control the number of trees
    public float rockDensity = 0.00001f; // Adjust density to control the number of rocks

    public GameObject oakTreePrefab;
    public GameObject firTreePrefab;
    public GameObject rocksPrefab1;
    public GameObject rocksPrefab2;
    public GameObject rocksPrefab3;

    void Start()
    {
        if (terrain == null)
        {
            terrain = Terrain.activeTerrain;
            if (terrain == null)
            {
                Debug.LogError("No Terrain object found!");
                return;
            }
        }

        PlaceTrees();
        PlaceRocks();
    }

    void PlaceTrees()
    {
        TerrainData terrainData = terrain.terrainData;
        float terrainWidth = terrainData.size.x;
        float terrainHeight = terrainData.size.z;

        for (float x = 0; x < terrainWidth; x++)
        {
            for (float y = 0; y < terrainHeight; y++)
            {
                if (Random.value < treeDensity)
                {
                    Vector3 position = new Vector3(x, 0, y);
                    position.y = terrain.SampleHeight(position);

                    GameObject treePrefab = Random.value < 0.5f ? oakTreePrefab : firTreePrefab;
                    GameObject tree = Instantiate(treePrefab, position, Quaternion.identity);
                    tree.transform.localScale *= Random.Range(0.8f, 1.2f);
                }
            }
        }
    }

    void PlaceRocks()
    {
        TerrainData terrainData = terrain.terrainData;
        float terrainWidth = terrainData.size.x;
        float terrainHeight = terrainData.size.z;

        for (float x = 0; x < terrainWidth; x++)
        {
            for (float y = 0; y < terrainHeight; y++)
            {
                if (Random.value < rockDensity)
                {
                    Vector3 position = new Vector3(x, 0, y);
                    position.y = terrain.SampleHeight(position);

                    GameObject rockPrefab;
                    float randomValue = Random.value;
                    if (randomValue < 0.33f)
                        rockPrefab = rocksPrefab1;
                    else if (randomValue < 0.66f)
                        rockPrefab = rocksPrefab2;
                    else
                        rockPrefab = rocksPrefab3;
                    Instantiate(rockPrefab, position, Quaternion.identity);
                }
            }
        }
    }
}