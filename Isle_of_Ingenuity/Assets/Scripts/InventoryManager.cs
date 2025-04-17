using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public static InventoryManager instance;
    public int maxStackedItems = 64;
    public InventorySlot [] inventorySlots;
    public GameObject inventoryItemPrefab;
    public Item[] startItems;

    int selectedSlot = -1;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this);
        }
    }

    private void Start() {
        ChangeSelectedSlot(0);
        foreach (var item in startItems) {
            AddItem(item);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            ChangeSelectedSlot(0);
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            ChangeSelectedSlot(1);
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            ChangeSelectedSlot(2);
        } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            ChangeSelectedSlot(3);
        } else if (Input.GetKeyDown(KeyCode.Alpha5)) {
            ChangeSelectedSlot(4);
        } else if (Input.GetKeyDown(KeyCode.Alpha6)) {
            ChangeSelectedSlot(5);
        } else if (Input.GetKeyDown(KeyCode.Alpha7)) {
            ChangeSelectedSlot(6);
        } else if (Input.GetKeyDown(KeyCode.Alpha8)) {
            ChangeSelectedSlot(7);
        }
    }

    void ChangeSelectedSlot(int newValue) {
        if (selectedSlot >= 0) {
            inventorySlots[selectedSlot].Deselect();
        }
        
        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }
    public bool AddItem(Item item) {

        for (int i = 0; i < inventorySlots.Length; i++) {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxStackedItems && itemInSlot.item.stackable == true) {
                itemInSlot.count++;
                itemInSlot.RefereshCount();
                return true;
            }
        }

        for (int i = 0; i < inventorySlots.Length; i++) {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null) {
                SpawnNewItem(item, slot);
                return true;
            }
        }
        return false;
    }

    void SpawnNewItem(Item item, InventorySlot slot) {
        GameObject NewItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = NewItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);
    }

    public Item GetSelectedItem(bool use) {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null) {
            Item item = itemInSlot.item;
            if (use == true) {
                if (item.stackable == true) {
                    itemInSlot.count--;
                }
                
                if (itemInSlot.count <= 0) {
                    Destroy(itemInSlot.gameObject);
                } else {
                    itemInSlot.RefereshCount();
                }
            }

            return item;
        }

        return null;
    }


    
}
