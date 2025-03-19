using UnityEngine;

public class Harvest : MonoBehaviour
{

    public Item logItem;
    public Item axeItem;
    private bool isPlayerInRange = false;

    void Start() {
        
    }
    
    void Update() {
        if (isPlayerInRange && Input.GetMouseButtonDown(0)) {
            if (InventoryManager.instance.GetSelectedItem(true) == axeItem) {
                if (logItem != null && InventoryManager.instance != null) {
                InventoryManager.instance.AddItem(logItem);
                Debug.Log("Collected Wood!");
                } else {
                    Debug.LogError("Log item or InventoryManager is NULL.");
                }
            }
            
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Tree")) {
            isPlayerInRange = true;
            Debug.Log("Player entered tree range.");
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Tree")) {
            isPlayerInRange = false;
            Debug.Log("Player exited tree range.");
        }
    }

    
}
