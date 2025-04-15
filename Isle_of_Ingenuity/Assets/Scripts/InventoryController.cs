using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject MainInventoryGroup; // Assign this in the Inspector
    public GameObject TestGroup;
    public PlayerController playerController;
    public GameObject BuildMenuGroup;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (MainInventoryGroup != null)
            {
                bool isMainActive = !MainInventoryGroup.activeSelf;
                bool isTestActive = !TestGroup.activeSelf;
                MainInventoryGroup.SetActive(isMainActive);
                // TestGroup.SetActive(isTestActive);

                // Toggle inventory state
                // playerController.ToggleInventory(isActive); // Pause camera and movement when inventory is open
            }
            else
            {
                Debug.LogWarning("MainInventoryGroup is not assigned in the Inspector.");
            }
        }

        // if (Input.GetKeyDown(KeyCode.B))
        // {
        //     if (BuildMenuGroup != null)
        //     {
        //         bool isMainActive = !BuildMenuGroup.activeSelf;
        //         BuildMenuGroup.SetActive(isMainActive);

        //         // Toggle inventory state
        //         // playerController.ToggleInventory(isActive); // Pause camera and movement when inventory is open
        //     }
        //     else
        //     {
        //         Debug.LogWarning("MainInventoryGroup is not assigned in the Inspector.");
        //     }
        // }
    }
}

