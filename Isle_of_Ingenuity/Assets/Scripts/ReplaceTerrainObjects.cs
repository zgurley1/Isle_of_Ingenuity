using UnityEngine;

public class ReplaceTerrainObjects : MonoBehaviour
{
    public Terrain terrain; // Assign your terrain in the Inspector
    public GameObject[] treePrefabs; // Assign prefabs in order (apple tree, pear tree, rocks, etc.)

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
            newObject.transform.localScale = Vector3.one * tree.widthScale;

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
