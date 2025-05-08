using UnityEngine;

public class StructurePlacer : MonoBehaviour
{
    [Header("Prefabs & Settings")]
    public GameObject structurePrefab;
    public GameObject ghostPrefab;
    public float gridSize = 5f;
    public float flattenRadius = 5f;

    public MaterialManager MaterialManager;

    private GameObject ghostInstance;

    private bool isBuilding = false;

    void Start()
    {
        if (ghostPrefab != null)
        {
            ghostInstance = Instantiate(ghostPrefab);
            SetLayerRecursively(ghostInstance, LayerMask.NameToLayer("Ignore Raycast")); // optional: prevent ghost from blocking raycasts
        }

        MaterialManager = FindAnyObjectByType<MaterialManager>();
    }

    void Update()
    {
        if (!isBuilding || Camera.main == null || ghostInstance == null)
            return;

        if (Input.GetMouseButtonDown(1))
        {
            CancelBuilding();
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.collider.GetComponent<Terrain>())
        {
            Vector3 snappedPos = GetSnappedPosition(hit.point, gridSize);
            ghostInstance.transform.position = snappedPos;

            if (Input.GetMouseButtonDown(0)) // Left-click
            {
                CancelBuilding();
                PlaceStructure(snappedPos);
                MaterialManager.BuildHouse();

                Debug.Log("Called Build House");
            }
        }
    }



    public void StartBuilding() {
        isBuilding = true;
        if (ghostInstance != null) {
            ghostInstance.SetActive(true);
        }
    }

    public void CancelBuilding()
{
    isBuilding = false;
    if (ghostInstance != null)
    {
        ghostInstance.SetActive(false);
    }
}

    Vector3 GetSnappedPosition(Vector3 position, float gridSize)
    {
        float x = Mathf.Round(position.x / gridSize) * gridSize;
        float z = Mathf.Round(position.z / gridSize) * gridSize;

        float y = Terrain.activeTerrain.SampleHeight(new Vector3(x, 0f, z)) + Terrain.activeTerrain.transform.position.y;
        return new Vector3(x, y, z);
    }

    void PlaceStructure(Vector3 position)
    {
        float flattenHeight = position.y;
        FlattenTerrain(position, flattenRadius, flattenHeight);

        Vector3 heightOffset = new Vector3(0, 2, 0);
        position += heightOffset;
        Instantiate(structurePrefab, position, Quaternion.identity);
    }

    void FlattenTerrain(Vector3 centerWorldPos, float radius, float flattenHeight)
    {
        Terrain terrain = Terrain.activeTerrain;
        TerrainData terrainData = terrain.terrainData;

        Vector3 terrainPos = centerWorldPos - terrain.transform.position;

        int hmWidth = terrainData.heightmapResolution;
        int hmHeight = terrainData.heightmapResolution;

        float[,] heights = terrainData.GetHeights(0, 0, hmWidth, hmHeight);

        float normX = terrainPos.x / terrainData.size.x;
        float normZ = terrainPos.z / terrainData.size.z;

        int centerX = Mathf.RoundToInt(normX * hmWidth);
        int centerZ = Mathf.RoundToInt(normZ * hmHeight);
        int radiusInHeights = Mathf.RoundToInt((radius / terrainData.size.x) * hmWidth);

        for (int z = -radiusInHeights; z <= radiusInHeights; z++)
        {
            for (int x = -radiusInHeights; x <= radiusInHeights; x++)
            {
                int tx = centerX + x;
                int tz = centerZ + z;

                if (tx >= 0 && tx < hmWidth && tz >= 0 && tz < hmHeight)
                {
                    float dist = Mathf.Sqrt(x * x + z * z);
                    if (dist <= radiusInHeights)
                    {
                        heights[tz, tx] = flattenHeight / terrainData.size.y;
                    }
                }
            }
        }

        terrainData.SetHeights(0, 0, heights);
    }

    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
}
