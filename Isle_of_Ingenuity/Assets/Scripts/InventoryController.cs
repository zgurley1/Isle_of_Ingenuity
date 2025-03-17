using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject MainInventoryGroup; // Assign this in the Inspector
    public PlayerController playerController; // Assign PlayerController in the Inspector

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (MainInventoryGroup != null)
            {
                bool isActive = !MainInventoryGroup.activeSelf;
                MainInventoryGroup.SetActive(isActive);

                // Toggle inventory state
                // playerController.ToggleInventory(isActive); // Pause camera and movement when inventory is open
            }
            else
            {
                Debug.LogWarning("MainInventoryGroup is not assigned in the Inspector.");
            }
        }
    }
}

