using UnityEngine;

public class DockRepairTrigger : MonoBehaviour
{
    public GameObject repairMenu;  // Reference to the repair menu UI
    private bool playerInRange = false;

    void Update() {
        if (playerInRange && Input.GetKeyDown(KeyCode.E)) {
            // Open the repair menu when the player presses the 'E' key
            repairMenu.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            playerInRange = true;
            Debug.Log("Press E to repair the dock.");
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            playerInRange = false;
            repairMenu.SetActive(false);  // Hide the repair menu when the player leaves
        }
    }
}
