using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class MaterialManager: MonoBehaviour
{
    [Header("Materials")]
    [SerializeField] private Item wood;
    [SerializeField] private Item stone;
    [SerializeField] private Item plank;
    [SerializeField] private Item brick;

    [Header("Material List")]
    public List<int> woodSlots = new List<int>();
    public List<int> woodNum = new List<int>();
    public List<int> stoneSlots = new List<int>();
    public List<int> stoneNum = new List<int>();
    public List<int> plankSlots = new List<int>();
    public List<int> plankNum = new List<int>();
    public List<int> brickSlots = new List<int>();
    public List<int> brickNum = new List<int>();


    [Header("Cost")]
    public int houseCostWood = 3;
    public int houseCostStone = 3;

    public int dockCostWood = 1;
    public int dockCostStone = 1;
    public int dockCostPlank = 1;
    public int dockCostBrick = 1;
    public int boatCostWood = 1;
    public int boatCostStone = 1;
    public int boatCostPlank = 1;
    public int boatCostBrick = 1;

    public List<int> houseCost;
    public List<int> dockCost;
    public List<int> boatCost;

    public InventoryManager InventoryManager;

    public static MaterialManager Instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InventoryManager = FindAnyObjectByType<InventoryManager>();
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(Instance);
        }

        houseCost = new List<int>{houseCostWood, houseCostStone, 0,0};
        dockCost = new List<int>{dockCostWood, dockCostStone, dockCostPlank, dockCostBrick};
        boatCost = new List<int>{boatCostWood, boatCostStone, boatCostPlank, boatCostBrick};
    }

    public Tuple<int,int> getBaseMatNum() {
        int numWood = 0;
        int numStone = 0;

        for (int i = 0; i < InventoryManager.inventorySlots.Length; i++) {
            InventorySlot slot = InventoryManager.inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == wood) {
                numWood += itemInSlot.count;
                
            } else if (itemInSlot != null && itemInSlot.item == stone) {
                numStone += itemInSlot.count;
            }
        }

        Tuple<int,int> matNum = new Tuple<int,int>(numWood,numStone);
        return matNum;
    }

    public Tuple<int,int,int,int> getAllMatNum() {
        int numWood = 0;
        int numStone = 0;
        int numPlank = 0;
        int numBrick = 0;

        for (int i = 0; i < InventoryManager.inventorySlots.Length; i++) {
            InventorySlot slot = InventoryManager.inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == wood) {
                numWood += itemInSlot.count;
                
            } else if (itemInSlot != null && itemInSlot.item == stone) {
                numStone += itemInSlot.count;
            } else if (itemInSlot != null && itemInSlot.item == plank) {
                numPlank += itemInSlot.count;
            } else if (itemInSlot != null && itemInSlot.item == brick) {
                numBrick += itemInSlot.count;
            }
        }

        Tuple<int,int, int, int> matNum = new Tuple<int,int,int,int>(numWood,numStone, numPlank, numBrick);
        return matNum;
    }

    public void ClearList() {
        woodNum.Clear();
        woodSlots.Clear();
        stoneNum.Clear();
        stoneSlots.Clear();
        plankNum.Clear();
        plankSlots.Clear();
        brickNum.Clear();
        brickSlots.Clear();
    }

    public void FillList() {
        for (int i = 0; i < InventoryManager.inventorySlots.Length; i++) {
            InventorySlot slot = InventoryManager.inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == wood) {
                woodSlots.Add(i);
                woodNum.Add(itemInSlot.count);
                
            } else if (itemInSlot != null && itemInSlot.item == stone) {
                stoneSlots.Add(i);
                stoneNum.Add(itemInSlot.count);
            } else if (itemInSlot != null && itemInSlot.item == plank) {
                plankSlots.Add(i);
                plankNum.Add(itemInSlot.count);
            } else if (itemInSlot != null && itemInSlot.item == brick) {
                brickSlots.Add(i);
                brickNum.Add(itemInSlot.count);
            }
        }
    }

    public int GetMatNum(List<int> material) {
        int numMat = 0;

        
        for (int i = 0; i < material.Count; i++) {
            InventorySlot slot = InventoryManager.inventorySlots[woodSlots[i]];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            int MatInSlot = itemInSlot.count;

            numMat += MatInSlot;
        }

        Debug.Log("Num Mat in inv: " + numMat);
        return numMat;
    }

    public int GetFirstIndex(List<int> material) {
        for (int i = 0; i < material.Count; i++) {
            if (material[i] > 0) {
                return i;
            }
        }
        return -1;
    }


    public void BuildHouse() {
        ClearList();
        FillList();
        //Find inventory slot that has material in it
        int woodIndex = GetFirstIndex(woodNum);
        int stoneIndex = GetFirstIndex(stoneNum);

        Debug.Log("Wood Index: " + woodIndex + "   Stone Index: " + stoneIndex);

        //Remove material from inventory slot
        if (woodIndex != -1 && stoneIndex != -1) {
            bool removedWood = InventoryManager.RemoveItem(woodSlots[woodIndex], houseCostWood);
            bool removedStone = InventoryManager.RemoveItem(stoneSlots[stoneIndex], houseCostStone);

            Debug.Log("Wood Removed: " + removedWood + " Stone Removed: " + removedStone);

            if (removedWood && removedStone) {
                woodNum[woodIndex] -= houseCostWood;
                stoneNum[stoneIndex] -= houseCostStone;
            }
        }
    }

    public void UpgradeCostDock() {
        ClearList();
        FillList();
        //Find inventory slot that has material in it
        int woodIndex = GetFirstIndex(woodNum);
        int stoneIndex = GetFirstIndex(stoneNum);
        int plankIndex = GetFirstIndex(plankNum);
        int brickIndex = GetFirstIndex(brickNum);

        //Remove material from inventory slot
        if (woodIndex != -1 && stoneIndex != -1 && plankIndex != -1 && brickIndex != -1) {
            bool removedWood = InventoryManager.RemoveItem(woodSlots[woodIndex], dockCostWood);
            bool removedStone = InventoryManager.RemoveItem(stoneSlots[stoneIndex], dockCostStone);
            bool removedPlank = InventoryManager.RemoveItem(plankSlots[plankIndex], dockCostPlank);
            bool removedBrick = InventoryManager.RemoveItem(brickSlots[brickIndex], dockCostBrick);

            Debug.Log("Wood Removed: " + removedWood + " Stone Removed: " + removedStone);

            if (removedWood && removedStone && removedPlank && removedBrick) {
                woodNum[woodIndex] -= dockCostWood;
                stoneNum[stoneIndex] -= dockCostStone;
                plankNum[plankIndex] -= dockCostPlank;
                brickNum[brickIndex] -= dockCostBrick;
            }
        }
    }

    public void UpgradeCostBoat() {
        ClearList();
        FillList();
        //Find inventory slot that has material in it
        int woodIndex = GetFirstIndex(woodNum);
        int stoneIndex = GetFirstIndex(stoneNum);
        int plankIndex = GetFirstIndex(plankNum);
        int brickIndex = GetFirstIndex(brickNum);

        //Remove material from inventory slot
        if (woodIndex != -1 && stoneIndex != -1 && plankIndex != -1 && brickIndex != -1) {
            bool removedWood = InventoryManager.RemoveItem(woodSlots[woodIndex], boatCostWood);
            bool removedStone = InventoryManager.RemoveItem(stoneSlots[stoneIndex], boatCostStone);
            bool removedPlank = InventoryManager.RemoveItem(plankSlots[plankIndex], boatCostPlank);
            bool removedBrick = InventoryManager.RemoveItem(brickSlots[brickIndex], boatCostBrick);

            Debug.Log("Wood Removed: " + removedWood + " Stone Removed: " + removedStone);

            if (removedWood && removedStone && removedPlank && removedBrick) {
                woodNum[woodIndex] -= boatCostWood;
                stoneNum[stoneIndex] -= boatCostStone;
                plankNum[plankIndex] -= boatCostPlank;
                brickNum[brickIndex] -= boatCostBrick;
            }
        }
    }
}
