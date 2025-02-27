using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject eggPrefab;
    public Terrain terrain;
    TerrainData terrainData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        terrainData = terrain.terrainData;
    }

    void CreateEgg()
    {
        int x = (int)Random.Range(0, terrainData.heightmapResolution);
    }
}
