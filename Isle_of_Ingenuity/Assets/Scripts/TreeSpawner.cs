using UnityEngine;
using System.Collections.Generic;

public class TreeSpawner : MonoBehaviour
{
    public Terrain terrain; // Assign the terrain
    public GameObject[] treePrefabs; // Assign tree prefabs
    public int maxTrees = 100; // Maximum trees allowed
    private List<Vector3> savedTreePositions = new List<Vector3>();

    void Start()
    {
        LoadTreePositions();
        EnsureMaxTreeCount();
        SpawnSavedTrees();
    }

    void LoadTreePositions()
    {
        savedTreePositions.Clear();

        for (int i = 0; i < maxTrees; i++)
        {
            float x = PlayerPrefs.GetFloat($"TreeX_{i}", float.MinValue);
            float z = PlayerPrefs.GetFloat($"TreeZ_{i}", float.MinValue);

            if (x != float.MinValue && z != float.MinValue)
            {
                float y = terrain.SampleHeight(new Vector3(x, 0, z)) + terrain.transform.position.y;
                savedTreePositions.Add(new Vector3(x, y, z));
            }
        }
    }

    void SaveTreePositions()
    {
        for (int i = 0; i < savedTreePositions.Count; i++)
        {
            PlayerPrefs.SetFloat($"TreeX_{i}", savedTreePositions[i].x);
            PlayerPrefs.SetFloat($"TreeZ_{i}", savedTreePositions[i].z);
        }
        PlayerPrefs.Save();
    }

    void EnsureMaxTreeCount()
    {
        while (savedTreePositions.Count > maxTrees)
        {
            savedTreePositions.RemoveAt(savedTreePositions.Count - 1);
        }

        while (savedTreePositions.Count < maxTrees)
        {
            Vector3 newTreePos = GetValidSpawnPosition();
            if (newTreePos != Vector3.zero) savedTreePositions.Add(newTreePos);
        }

        SaveTreePositions();
    }

    void SpawnSavedTrees()
    {
        foreach (Vector3 position in savedTreePositions)
        {
            GameObject treePrefab = treePrefabs[Random.Range(0, treePrefabs.Length)];
            GameObject newTree = Instantiate(treePrefab, position, Quaternion.identity);
            newTree.transform.localScale = Vector3.one * Random.Range(0.8f, 1.2f);

            if (newTree.GetComponent<Collider>() == null)
                newTree.AddComponent<CapsuleCollider>();
        }
    }

    Vector3 GetValidSpawnPosition()
    {
        TerrainData terrainData = terrain.terrainData;
        int maxAttempts = 100;

        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            float randomX = Random.Range(0, terrainData.size.x);
            float randomZ = Random.Range(0, terrainData.size.z);
            float terrainY = terrain.SampleHeight(new Vector3(randomX, 0, randomZ)) + terrain.transform.position.y;

            Vector3 worldPos = new Vector3(randomX + terrain.transform.position.x, terrainY, randomZ + terrain.transform.position.z);

            // Check texture at the spawn location
            if (IsGrassArea(worldPos))
                return worldPos;
        }

        return Vector3.zero; // Return zero if no valid position was found
    }

    bool IsGrassArea(Vector3 worldPos)
    {
        TerrainData terrainData = terrain.terrainData;
        Vector3 terrainPos = worldPos - terrain.transform.position;
        int mapX = Mathf.FloorToInt((terrainPos.x / terrainData.size.x) * terrainData.alphamapWidth);
        int mapZ = Mathf.FloorToInt((terrainPos.z / terrainData.size.z) * terrainData.alphamapHeight);

        float[,,] alphaMaps = terrainData.GetAlphamaps(mapX, mapZ, 1, 1);
        int grassTextureIndex = 0; // Change if grass texture is not at index 0

        return alphaMaps[0, 0, grassTextureIndex] > 0.5f;
    }
}
