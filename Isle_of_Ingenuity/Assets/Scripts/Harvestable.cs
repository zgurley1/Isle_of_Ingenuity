using UnityEngine;


public class TreeEntity : MonoBehaviour
{
    public Vector3 position;
    public int prefabIndex;
    private TreeSpawner spawner;
    public static int maxHealth;

    public void Initialize(Vector3 pos, int index, int health, TreeSpawner treeSpawner)
    {
        position = pos;
        prefabIndex = index;
        spawner = treeSpawner;
        maxHealth = health;
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
        maxHealth = TreeEntity.maxHealth;
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

    // private bool IsCorrectTool(Item tool) {
    //     if (requiredTool == ToolType.Axe && tool == InventoryManager.instance.GetSelectedItem(false) == axeItem) {
    //         Debug.Log(requiredTool);
    //         Debug.Log(tool);
    //         Debug.Log("Tool is axe");
    //         return true;
    //     }
    //     if (requiredTool == ToolType.Pickaxe && tool == InventoryManager.instance.GetSelectedItem(false) == pickaxeItem) {
    //         Debug.Log("Tool is pickaxe");
    //         return true;
    //     }
    //     return false;
    // }

    private bool IsCorrectTool(Item tool) {
    if (requiredTool == ToolType.Axe && tool == axeItem) {
        return true;
    } if (requiredTool == ToolType.Pickaxe && tool == pickaxeItem) {
        return true;
    }
    return false;
}
}
