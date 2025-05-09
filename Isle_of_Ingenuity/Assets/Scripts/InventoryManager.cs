using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryManager : MonoBehaviour
{

    public static InventoryManager instance;
    public int maxStackedItems = 64;
    public InventorySlot [] inventorySlots;
    public GameObject inventoryItemPrefab;
    public Item[] startItems;

    int selectedSlot = -1;


    [Header("Materials")]
    public Item wood;
    public Item stone;
    public Item plank;
    public Item brick;



    [Header("Tools")]
    public Item axe;
    public Item pickaxe;

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

        // Check the inventory for an available stack of the item
        for (int i = 0; i < inventorySlots.Length; i++) {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxStackedItems && itemInSlot.item.stackable == true) {
                itemInSlot.count++;
                itemInSlot.RefereshCount();
                return true;
            }
        }


        // Fill the first avaliable slot
        int first = GetFirstEmptySlot();
        if (first != -1) {
            SpawnNewItem(item, inventorySlots[first]);
            return true;
        } else {
            return false;
        }
    }


    public bool RemoveItem(int index, int amount) {
        
        InventoryItem itemInSlot = inventorySlots[index].GetComponentInChildren<InventoryItem>();
        if (itemInSlot.count < amount) {
            return false;
        }
        itemInSlot.count -= amount;
        if (itemInSlot.count == 0) {
            Destroy(itemInSlot.gameObject);
            return true;
        }
        itemInSlot.RefereshCount();
        return true;
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

    public List<int> GetEmptySlots() {
        List<int> emptySlot = new List<int>();

        for (int i = 0; i < inventorySlots.Length; i++) {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null) {
                emptySlot.Add(i);
            }
        }
        return emptySlot;
    }

    public int GetFirstEmptySlot() {
        for (int i = 0; i < inventorySlots.Length; i++) {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null) {
                return i;
            }
        }
        return -1;
    }
    
}
