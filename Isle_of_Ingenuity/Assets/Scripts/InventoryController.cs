using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject MainInventoryGroup; // Assign this in the Inspector
    //public GameObject TestGroup;
    public PlayerController playerController;
    public GameObject BuildMenuGroup;
    public GameObject UpgradeMenuGroup;

    public GameObject UpgradeText;
    private bool inventoryActive = false;
    private bool buildActive = false;
    private bool upgradeActive = false;

    private DockUpgradeZone dockUpgradeZone;

    void Start() {
        dockUpgradeZone = FindAnyObjectByType<DockUpgradeZone>();
        CursorSwitch(false);
    }

    

    void Update()
    {
        inventoryActive = MainInventoryGroup.activeSelf;
        buildActive = BuildMenuGroup.activeSelf;
        upgradeActive = UpgradeMenuGroup.activeSelf;


        if (Input.GetKeyDown(KeyCode.I))
        {
            if (MainInventoryGroup != null) {
                buildActive = false;
                upgradeActive = false;
                BuildMenuGroup.SetActive(buildActive);
                UpgradeMenuGroup.SetActive(upgradeActive);

                inventoryActive = !inventoryActive;
                CursorSwitch(inventoryActive);
                MainInventoryGroup.SetActive(inventoryActive);
            }
            else
            {
                Debug.LogWarning("MainInventoryGroup 'inventory' is not assigned in the Inspector.");
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (BuildMenuGroup != null)
            {
                inventoryActive = false;
                upgradeActive = false;
                MainInventoryGroup.SetActive(inventoryActive);
                UpgradeMenuGroup.SetActive(upgradeActive);

                buildActive = !buildActive;
                CursorSwitch(buildActive);
                BuildMenuGroup.SetActive(buildActive);
            }
            else
            {
                Debug.LogWarning("MainInventoryGroup 'build' is not assigned in the Inspector.");
            }
        }

        if (dockUpgradeZone.canOpenUpgradeScreen && Input.GetKeyDown(KeyCode.E))
        {
            if (UpgradeMenuGroup != null)
            {
                SwitchUpgradeUI();
            }
            else
            {
                Debug.LogWarning("MainInventoryGroup 'upgrade' is not assigned in the Inspector.");
            }
        }
    }

    public void CloseAll()
    {
        upgradeActive = false;
        inventoryActive = false;
        buildActive = false;

        UpgradeMenuGroup.SetActive(upgradeActive);
        BuildMenuGroup.SetActive(buildActive);
        MainInventoryGroup.SetActive(inventoryActive);
        CursorSwitch(false);
    }

    public void SwitchUpgradeUI()
    {
        inventoryActive = false;
        buildActive = false;
        BuildMenuGroup.SetActive(buildActive);
        MainInventoryGroup.SetActive(inventoryActive);

        upgradeActive = !upgradeActive;
        CursorSwitch(upgradeActive);
        UpgradeText.SetActive(!upgradeActive);
        UpgradeMenuGroup.SetActive(upgradeActive);
    }

    public void CursorSwitch(bool UIActive) {
        if (UIActive)
        {
            CursorManager.Instance.EnableUICursor();

        }
        else {
            CursorManager.Instance.EnableGameplayCursor();
        }
    }
}

