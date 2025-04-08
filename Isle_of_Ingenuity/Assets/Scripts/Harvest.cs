using UnityEngine;

public class Harvest : MonoBehaviour
{

    // public Item logItem;
    // public Item axeItem;
    // public Item pickaxeItem;
    // public Item stoneItem;
    // private bool isPlayerInTreeRange = false;
    // private bool isPlayerInRockRange = false;

    // void Start() {
        
    // }
    
    // void Update() {
    //     if (isPlayerInTreeRange && Input.GetMouseButtonDown(0)) {
    //         if (InventoryManager.instance.GetSelectedItem(true) == axeItem) {
    //             if (logItem != null && InventoryManager.instance != null) {
    //             InventoryManager.instance.AddItem(logItem);
    //             Debug.Log("Collected Wood!");
    //             } else {
    //                 Debug.LogError("Log item or InventoryManager is NULL.");
    //             }
    //         }
            
    //     }

    //     if (isPlayerInRockRange && Input.GetMouseButtonDown(0)) {
    //         if (InventoryManager.instance.GetSelectedItem(true) == pickaxeItem) {
    //             if (stoneItem != null && InventoryManager.instance != null) {
    //             InventoryManager.instance.AddItem(stoneItem);
    //             Debug.Log("Collected Stone!");
    //             } else {
    //                 Debug.LogError("Rock item or InventoryManager is NULL.");
    //             }
    //         }
            
    //     }
    // }

    // private void OnTriggerEnter(Collider other) {
    //     if (other.CompareTag("Tree")) {
    //         isPlayerInTreeRange = true;
    //         Debug.Log("Player entered tree range.");
    //     }

    //     if (other.CompareTag("Rock")) {
    //         isPlayerInRockRange = true;
    //         Debug.Log("Player entered rock range.");
    //     }
    // }

    // private void OnTriggerExit(Collider other) {
    //     if (other.CompareTag("Tree")) {
    //         isPlayerInTreeRange = false;
    //         Debug.Log("Player exited tree range.");
    //     }

    //     if (other.CompareTag("Rock")) {
    //         isPlayerInRockRange = false;
    //         Debug.Log("Player exited rock range.");
    //     }
    // }

    private Harvestable currentTarget;

    void Update() {
        if (currentTarget != null && Input.GetMouseButtonDown(0)) {
            Item selectedTool = InventoryManager.instance.GetSelectedItem(true);
            currentTarget.Harvest(selectedTool);
        }
    }

    private void OnTriggerEnter(Collider other) {
        Harvestable harvestable = other.GetComponent<Harvestable>();
        if (harvestable != null) {
            currentTarget = harvestable;
            Debug.Log("Entered range of " + other.name);
        }
    }

    private void OnTriggerExit(Collider other) {
        Harvestable harvestable = other.GetComponent<Harvestable>();
        if (harvestable != null && harvestable == currentTarget) {
            currentTarget = null;
            Debug.Log("Exited range of " + other.name);
        }
    }
}
