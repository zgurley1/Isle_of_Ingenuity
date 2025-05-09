using UnityEngine;

public class CheatCodeManager : MonoBehaviour
{
    private MaterialManager materialManager;
    private InventoryManager inventoryManager;

    [SerializeField] private GameObject canvas;

    public bool canvasActive = true;
    
    void Start() {
        materialManager = FindAnyObjectByType<MaterialManager>();
        inventoryManager = FindAnyObjectByType<InventoryManager>();

        canvasActive = canvas.activeSelf;
    }
    
    
    public void GiveMats() {
        for (int i = 0; i < 3; i++) {
            inventoryManager.AddItem(inventoryManager.wood);
        }
        for (int i = 0; i < 3; i++) {
            inventoryManager.AddItem(inventoryManager.stone);
        }
        for (int i = 0; i < 3; i++) {
            inventoryManager.AddItem(inventoryManager.plank);
        }
        for (int i = 0; i < 3; i++) {
            inventoryManager.AddItem(inventoryManager.brick);
        }
    }

    public void SwitchUI() {
        canvasActive = !canvasActive;
        canvas.SetActive(canvasActive);
    }
}
