using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class MaterialManager : MonoBehaviour
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

    public InventoryManager InventoryManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InventoryManager = FindAnyObjectByType<InventoryManager>();
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

    public void ClearList() {
        woodNum.Clear();
        woodSlots.Clear();
        stoneNum.Clear();
        stoneSlots.Clear();
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
}
