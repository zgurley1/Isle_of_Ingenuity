using UnityEngine;

public class BuildManager : MonoBehaviour
{

    public GameObject BuildMenu; // Assign this in the Inspector
    public GameObject TestGroup;
    public PlayerController playerController;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.B))
        // {
        //     if (BuildMenu != null)
        //     {
        //         bool isMainActive = !BuildMenu.activeSelf;
        //         BuildMenu.SetActive(isMainActive);

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
