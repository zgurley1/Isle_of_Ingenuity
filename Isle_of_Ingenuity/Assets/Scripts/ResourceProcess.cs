using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ResourceProcess : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private bool seek = true;
    private InventoryController InventoryController;

    [Header("Process Buttons")]
    public Button stoneProcessButton;
    public Button woodProcessButton;
    
    [Header("Inventory List")]
    public List<int> woodSlots = new List<int>();
    public List<int> stoneSlots = new List<int>();

    public List<int> woodNum = new List<int>();
    public List<int> stoneNum = new List<int>();

    private InventoryManager InventoryManager;

    [SerializeField] private Item wood;
    [SerializeField] private Item stone;
    [SerializeField] private Item plank;
    [SerializeField] private Item brick;
    void Start()
    {
        InventoryController = FindAnyObjectByType<InventoryController>();
        InventoryManager = FindAnyObjectByType<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!InventoryController.UIActive) {
            if (Camera.main == null)
                return;

            

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("ResourceProcessor"))
            {

                if (Input.GetMouseButtonDown(0)) // Left-click
                {
                    InventoryController.OpenProcessUI();
                    ClearList();
                    FillList();

                    Debug.Log("Wood slots length" + woodSlots.Count);
                    Debug.Log("Stone slots length" + stoneSlots.Count);

                    UpdateButtons();
                }
            }
        }
    }

    void ClearList() {
        woodSlots.Clear();
        stoneSlots.Clear();
        woodNum.Clear();
        stoneNum.Clear();
    }
    void FillList() {
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

    private void UpdateButtons() {
        if (stoneSlots.Count > 0) {
            stoneProcessButton.interactable = true;
        } else {
            stoneProcessButton.interactable = false;
        }

        if (woodSlots.Count > 0) {
            woodProcessButton.interactable = true;
        } else {
            woodProcessButton.interactable = false;
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

    private int GetFirstIndex(List<int> material) {
        for (int i = 0; i < material.Count; i++) {
            if (material[i] > 0) {
                return i;
            }
        }
        return -1;
    }
    

    public void ProcessWood() {
        int inventoryIndex = GetFirstIndex(woodNum);

        if (inventoryIndex != -1) {
            bool added = InventoryManager.AddItem(plank);

            if (added) {
                InventoryManager.RemoveItem(woodSlots[inventoryIndex]);
                woodNum[inventoryIndex] -= 1;
            }
        }
    }

    public void ProcessStone() {
        int inventoryIndex = GetFirstIndex(stoneNum);

        if (inventoryIndex != -1) {
            bool added = InventoryManager.AddItem(brick);

            if (added) {
                InventoryManager.RemoveItem(stoneSlots[inventoryIndex]);
                stoneNum[inventoryIndex] -= 1;
            }
        }
    }


}
