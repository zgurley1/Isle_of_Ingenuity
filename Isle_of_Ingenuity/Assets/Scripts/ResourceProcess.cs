using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceProcess : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private bool seek = true;
    private InventoryController InventoryController;

    public List<int> woodSlots = new List<int>();
    public List<int> stoneSlots = new List<int>();

    private InventoryManager InventoryManager;

    [SerializeField] private Item wood;
    [SerializeField] private Item stone;
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

                }
            }
        }
    }

    void ClearList() {
        woodSlots.Clear();
        stoneSlots.Clear();
    }
    void FillList() {
        for (int i = 0; i < InventoryManager.inventorySlots.Length; i++) {
            InventorySlot slot = InventoryManager.inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == wood) {
                woodSlots.Add(i);
                
            } else if (itemInSlot != null && itemInSlot.item == stone) {
                stoneSlots.Add(i);
                
            }
        }
    }


}
