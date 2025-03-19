using UnityEngine;

public class Harvest : MonoBehaviour
{

    public Item logItem;
    private bool isPlayerInRange = false;

    void Start() {
        
    }
    
    void Update() {
        if (isPlayerInRange && Input.GetMouseButtonDown(0)) // Left click
        {
            if (logItem != null && InventoryManager.instance != null)
            {
                InventoryManager.instance.AddItem(logItem);
                Debug.Log("Collected Wood!");
            }
            else
            {
                Debug.LogError("Log item or InventoryManager is NULL.");
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Tree")) // Ensure Player has the correct tag
        {
            isPlayerInRange = true;
            Debug.Log("Player entered tree range.");
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Tree")) // Ensure Player has the correct tag
        {
            isPlayerInRange = false;
            Debug.Log("Player exited tree range.");
        }
    }

    
}
