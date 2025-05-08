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
    // public List<int> woodSlots = new List<int>();
    // public List<int> stoneSlots = new List<int>();

    // public List<int> woodNum = new List<int>();
    // public List<int> stoneNum = new List<int>();

    private MaterialManager MaterialManager;


    [SerializeField] private Item wood;
    [SerializeField] private Item stone;
    [SerializeField] private Item plank;
    [SerializeField] private Item brick;
    void Start()
    {
        InventoryController = FindAnyObjectByType<InventoryController>();
        MaterialManager = FindAnyObjectByType<MaterialManager>();
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
                    MaterialManager.ClearList();
                    MaterialManager.FillList();

                    // Debug.Log("Wood slots length" + MaterialManager.woodSlots.Count);
                    // Debug.Log("Stone slots length" + MaterialManager.stoneSlots.Count);

                    UpdateButtons();
                }
            }
        }
    }

    // void ClearList() {
    //     woodSlots.Clear();
    //     stoneSlots.Clear();
    //     woodNum.Clear();
    //     stoneNum.Clear();
    // }
    // void FillList() {
    //     var list = MaterialManager.fillList();

    //     woodSlots = list.Item1;
    //     woodNum = list.Item2;
    //     stoneSlots = list.Item3;
    //     stoneNume = list.Item4;
    // }

    private void UpdateButtons() {
        if (MaterialManager.stoneSlots.Count > 0) {
            stoneProcessButton.interactable = true;
        } else {
            stoneProcessButton.interactable = false;
        }

        if (MaterialManager.woodSlots.Count > 0) {
            woodProcessButton.interactable = true;
        } else {
            woodProcessButton.interactable = false;
        }
    }
    

    public void ProcessWood() {
        int inventoryIndex = MaterialManager.GetFirstIndex(MaterialManager.woodNum);

        if (inventoryIndex != -1) {
            bool added = MaterialManager.InventoryManager.AddItem(plank);

            if (added) {
                MaterialManager.InventoryManager.RemoveItem(MaterialManager.woodSlots[inventoryIndex], 1);
                MaterialManager.woodNum[inventoryIndex] -= 1;
            }
        }
    }

    public void ProcessStone() {
        int inventoryIndex = MaterialManager.GetFirstIndex(MaterialManager.stoneNum);

        if (inventoryIndex != -1) {
            bool added = MaterialManager.InventoryManager.AddItem(brick);

            if (added) {
                MaterialManager.InventoryManager.RemoveItem(MaterialManager.stoneSlots[inventoryIndex], 1);
                MaterialManager.stoneNum[inventoryIndex] -= 1;
            }
        }
    }


}
