using UnityEngine;


public class TreeEntity : MonoBehaviour
{
    public Vector3 position;
    public int prefabIndex;
    private TreeSpawner spawner;

    public void Initialize(Vector3 pos, int index, TreeSpawner treeSpawner)
    {
        position = pos;
        prefabIndex = index;
        spawner = treeSpawner;
    }

    private void OnDestroy()
    {
        if (spawner != null)
        {
            spawner.RemoveTree(position, prefabIndex);
        }
    }
}




public class Harvestable : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public Item dropItem;
    public ToolType requiredTool;

    //public Item logItem;
    public Item axeItem;
    public Item pickaxeItem;
    //public Item stoneItem;

    public enum ToolType {
        Axe,
        Pickaxe
    }

    void Start() {
        currentHealth = maxHealth;
    }

    public void Harvest(Item usedTool) {
        if (!IsCorrectTool(usedTool)) {
            Debug.Log("Wrong tool for this resource.");
            return;
        }

        currentHealth--;
        InventoryManager.instance.AddItem(dropItem);
        Debug.Log($"Collected {dropItem.name}! Remaining health: {currentHealth}");

        if (currentHealth <= 0) {
            Destroy(gameObject);
            Debug.Log($"{gameObject.name} destroyed!");
        }
    }

    private bool IsCorrectTool(Item tool) {
        if (requiredTool == ToolType.Axe && tool == InventoryManager.instance.GetSelectedItem(true) == axeItem)
            return true;
        if (requiredTool == ToolType.Pickaxe && tool == InventoryManager.instance.GetSelectedItem(true) == pickaxeItem)
            return true;
        return false;
    }
}
