using UnityEngine;
using UnityEngine.UI;
using System;

public class InventoryController : MonoBehaviour
{
    //public PlayerController playerController;
    [Header("UI Elements")]
    
    public GameObject MainInventoryGroup; // Assign this in the Inspector
    //public GameObject TestGroup;
    
    public GameObject BuildMenuGroup;
    public GameObject UpgradeMenuGroup;
    public GameObject ProcessMenuGroup;

    [Header("Buttons")]
    public Button woodProcessButton;
    public Button stoneProcessButton;
    public Button buildHouseButton;


    

    [Header("OTHER")]
    public GameObject UpgradeText;

    private bool inventoryActive = false;
    private bool buildActive = false;
    private bool upgradeActive = false;
    private bool processActive = false;

    public bool UIActive = false;

    private DockUpgradeZone dockUpgradeZone;
    private MaterialManager MaterialManager;

    void Start() {
        dockUpgradeZone = FindAnyObjectByType<DockUpgradeZone>();
        MaterialManager = FindAnyObjectByType<MaterialManager>();
        CursorSwitch(false);
    }

    

    void Update()
    {
        inventoryActive = MainInventoryGroup.activeSelf;
        buildActive = BuildMenuGroup.activeSelf;
        upgradeActive = UpgradeMenuGroup.activeSelf;

        if (inventoryActive || buildActive || upgradeActive || processActive) {
            UIActive = true;
        } else {
            UIActive = false;
        }
            


        if (Input.GetKeyDown(KeyCode.I))
        {
            if (MainInventoryGroup != null) {
                buildActive = false;
                upgradeActive = false;
                processActive = false;
                ProcessMenuGroup.SetActive(processActive);
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
                processActive = false;
                ProcessMenuGroup.SetActive(processActive);
                MainInventoryGroup.SetActive(inventoryActive);
                UpgradeMenuGroup.SetActive(upgradeActive);

                buildActive = !buildActive;
                CursorSwitch(buildActive);
                UpdateButtons();
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

    void UpdateButtons() {
        var matNum = MaterialManager.getBaseMatNum();
        int numWood = matNum.Item1;
        int numStone = matNum.Item2;

        if (numWood >= MaterialManager.houseCostWood && numStone >= MaterialManager.houseCostStone) {
            buildHouseButton.interactable = true;
        }
        else {
            buildHouseButton.interactable = false;
        }


        Debug.Log("Wood num: " + numWood);
        Debug.Log("Stone num: " + numStone);
    }
    
    public void CloseAll()
    {
        upgradeActive = false;
        inventoryActive = false;
        buildActive = false;
        processActive = false;


        ProcessMenuGroup.SetActive(processActive);
        UpgradeMenuGroup.SetActive(upgradeActive);
        BuildMenuGroup.SetActive(buildActive);
        MainInventoryGroup.SetActive(inventoryActive);
        CursorSwitch(false);
    }

    public void SwitchUpgradeUI()
    {
        inventoryActive = false;
        buildActive = false;
        processActive = false;
        ProcessMenuGroup.SetActive(processActive);
        BuildMenuGroup.SetActive(buildActive);
        MainInventoryGroup.SetActive(inventoryActive);

        upgradeActive = !upgradeActive;
        CursorSwitch(upgradeActive);
        UpgradeText.SetActive(!upgradeActive);
        UpgradeMenuGroup.SetActive(upgradeActive);
    }

    public void OpenProcessUI() {
        CloseAll();
        processActive = true;
        ProcessMenuGroup.SetActive(processActive);
        CursorSwitch(processActive);
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

