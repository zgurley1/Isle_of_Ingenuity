using UnityEngine;

public class DockUpgradeZone : MonoBehaviour
{
    public bool canOpenUpgradeScreen = false;
    public bool canRideBoat = false;
    public GameObject UpgradeMenu;
    public GameObject repairText;
    public GameObject boardBoatText;

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

        if (other.CompareTag("ShipCollider")) {
            canRideBoat = true;
            boardBoatText.SetActive(true);
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
        if (other.CompareTag("ShipCollider"))
        {
            canRideBoat = false;
            boardBoatText.SetActive(false);
        }
    }
}
