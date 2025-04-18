using UnityEngine;

public class ReplaceTerrainObjects : MonoBehaviour
{
    public Terrain terrain;
    public GameObject[] treePrefabs;

    void Start()
    {
        ReplaceTerrainTreesWithPrefabs();
    }

    void ReplaceTerrainTreesWithPrefabs()
    {
        if (terrain == null || treePrefabs.Length == 0)
        {
            Debug.LogError("Terrain or tree prefabs not assigned!");
            return;
        }

        TerrainData terrainData = terrain.terrainData;
        TreeInstance[] treeInstances = terrainData.treeInstances;

        for (int i = 0; i < treeInstances.Length; i++)
        {
            TreeInstance tree = treeInstances[i];

            // Get corresponding prefab based on tree prototype index
            int prototypeIndex = tree.prototypeIndex;
            if (prototypeIndex >= treePrefabs.Length)
            {
                Debug.LogWarning($"No prefab assigned for prototype index {prototypeIndex}");
                continue;
            }

            GameObject prefab = treePrefabs[prototypeIndex];

            // Convert terrain tree position to world position
            Vector3 worldPos = Vector3.Scale(tree.position, terrainData.size) + terrain.transform.position;

            // Instantiate the prefab
            GameObject newObject = Instantiate(prefab, worldPos, Quaternion.identity);
            // Choose scale range based on prefab name
            float minScale = 15.0f;
            float maxScale = 20.0f;

            string prefabName = prefab.name.ToLower();
            // if (prefabName.Contains("rock"))
            // {
            //     minScale = 5.0f;
            //     maxScale = 15.0f;
            // }
            // else if (prefabName.Contains("tree"))
            // {
            //     minScale = 1.5f;
            //     maxScale = 4.0f;
            // }

            float randomScale = Random.Range(minScale, maxScale);
            newObject.transform.localScale = Vector3.one * randomScale;

            // Ensure it has a collider
            if (newObject.GetComponent<Collider>() == null)
            {
                newObject.AddComponent<CapsuleCollider>();
            }
        }

        // Clear terrain trees after replacement
        terrainData.treeInstances = new TreeInstance[0];

        Debug.Log("Replaced terrain objects with prefabs.");
    }
}
