using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class TreeData
{
    public Vector3 position;
    public int prefabIndex;
}


public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array, bool prettyPrint = false)
    {
        Wrapper<T> wrapper = new Wrapper<T> { Items = array };
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}

public class TreeSpawner : MonoBehaviour
{
    public Terrain terrain;
    public GameObject[] treePrefabs;
    public int maxTrees = 100;

    private List<TreeData> savedTrees = new List<TreeData>();

    void Start()
    {
        LoadTreeData();
        EnsureMaxTreeCount();
        SpawnTrees();
    }

    void LoadTreeData()
    {
        savedTrees.Clear();
        string json = PlayerPrefs.GetString("TreeData", "");

        if (!string.IsNullOrEmpty(json))
        {
            TreeData[] loadedTrees = JsonHelper.FromJson<TreeData>(json);
            savedTrees.AddRange(loadedTrees);
        }
    }

    void SaveTreeData()
    {
        TreeData[] array = savedTrees.ToArray();
        string json = JsonHelper.ToJson(array, true);
        PlayerPrefs.SetString("TreeData", json);
        PlayerPrefs.Save();
    }

    void EnsureMaxTreeCount()
    {
        while (savedTrees.Count > maxTrees)
        {
            savedTrees.RemoveAt(savedTrees.Count - 1);
        }

        while (savedTrees.Count < maxTrees)
        {
            Vector3? newTreePos = GetValidSpawnPosition();
            if (newTreePos.HasValue)
            {
                int prefabIndex = Random.Range(0, treePrefabs.Length);
                savedTrees.Add(new TreeData { position = newTreePos.Value, prefabIndex = prefabIndex });
            }
        }

        SaveTreeData();
    }

    void SpawnTrees()
    {
        foreach (TreeData tree in savedTrees)
        {
            GameObject prefab = treePrefabs[tree.prefabIndex];
            GameObject newTree = Instantiate(prefab, tree.position, Quaternion.identity);
            newTree.transform.localScale = Vector3.one * Random.Range(0.8f, 1.2f);

            // Add TreeInstance script and initialize
            TreeEntity instance = newTree.AddComponent<TreeEntity>();
            instance.Initialize(tree.position, tree.prefabIndex, this);

            if (newTree.GetComponent<Collider>() == null)
                newTree.AddComponent<CapsuleCollider>();
        }
    }

    Vector3? GetValidSpawnPosition()
    {
        TerrainData terrainData = terrain.terrainData;
        int maxAttempts = 100;

        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            float randomX = Random.Range(0, terrainData.size.x);
            float randomZ = Random.Range(0, terrainData.size.z);
            float terrainY = terrain.SampleHeight(new Vector3(randomX, 0, randomZ)) + terrain.transform.position.y;

            Vector3 worldPos = new Vector3(randomX + terrain.transform.position.x, terrainY, randomZ + terrain.transform.position.z);

            if (IsGrassArea(worldPos))
                return worldPos;
        }

        return null;
    }

    bool IsGrassArea(Vector3 worldPos)
    {
        TerrainData terrainData = terrain.terrainData;
        Vector3 terrainPos = worldPos - terrain.transform.position;
        int mapX = Mathf.FloorToInt((terrainPos.x / terrainData.size.x) * terrainData.alphamapWidth);
        int mapZ = Mathf.FloorToInt((terrainPos.z / terrainData.size.z) * terrainData.alphamapHeight);

        float[,,] alphaMaps = terrainData.GetAlphamaps(mapX, mapZ, 1, 1);
        int grassTextureIndex = 0;

        return alphaMaps[0, 0, grassTextureIndex] > 0.5f;
    }

    public void RemoveTree(Vector3 position, int prefabIndex)
    {
        for (int i = 0; i < savedTrees.Count; i++)
        {
            if (Vector3.Distance(savedTrees[i].position, position) < 0.1f && savedTrees[i].prefabIndex == prefabIndex)
            {
                savedTrees.RemoveAt(i);
                SaveTreeData();
                break;
            }
        }
    }
}
