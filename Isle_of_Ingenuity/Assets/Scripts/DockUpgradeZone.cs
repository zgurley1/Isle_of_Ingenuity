using UnityEngine;

public class DockUpgradeZone : MonoBehaviour
{
    public bool canOpenUpgradeScreen = false;
    public GameObject UpgradeMenu;
    public GameObject repairText;

    public InventoryManager InventoryManager;

    void Start() {
        InventoryManager  = FindAnyObjectByType<InventoryManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("UpgradeStation")) // Or check for a component instead
        {
            canOpenUpgradeScreen = true;
            repairText.SetActive(true);

        }


        if (other.CompareTag("InitialItems")) {
            Debug.Log("Initial Items");

            InventoryManager.AddItem(InventoryManager.axe);
            InventoryManager.AddItem(InventoryManager.pickaxe);

            other.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("UpgradeStation"))
        {
            canOpenUpgradeScreen = false;
            repairText.SetActive(false);
            UpgradeMenu.SetActive(false);
        }
    }
}
