using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class InventoryController : MonoBehaviour
{
    //public PlayerController playerController;
    [Header("UI Elements")]
    
    public GameObject MainInventoryGroup;
    
    public GameObject BuildMenuGroup;
    public GameObject UpgradeMenuGroup;
    public GameObject ProcessMenuGroup;
    public GameObject CheatCodeMenuGroup;

    [Header("Buttons")]
    public Button woodProcessButton;
    public Button stoneProcessButton;
    public Button buildHouseButton;
    public Button upgradeDockButton;
    public Button upgradeBoatButton;


    

    [Header("OTHER")]
    public GameObject UpgradeText;

    private bool inventoryActive = false;
    private bool buildActive = false;
    private bool upgradeActive = false;
    private bool processActive = false;
    private bool cheatActive = false;

    public bool UIActive = false;

    private DockUpgradeZone dockUpgradeZone;
    private MaterialManager MaterialManager;
    private UpgradeActions UpgradeActions;
    private CheatCodeManager cheatCodeManager;

    void Start() {
        dockUpgradeZone = FindAnyObjectByType<DockUpgradeZone>();
        MaterialManager = FindAnyObjectByType<MaterialManager>();
        UpgradeActions = FindAnyObjectByType<UpgradeActions>();
        cheatCodeManager = FindAnyObjectByType<CheatCodeManager>();
        CursorSwitch(false);
    }

    

    void Update()
    {
        inventoryActive = MainInventoryGroup.activeSelf;
        buildActive = BuildMenuGroup.activeSelf;
        upgradeActive = UpgradeMenuGroup.activeSelf;
        cheatActive = CheatCodeMenuGroup.activeSelf;

        if (inventoryActive || buildActive || upgradeActive || processActive || cheatActive) {
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
                UpdateButtons(MaterialManager.houseCost, buildHouseButton);
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
                upgradeDockButton.interactable = false;
                upgradeBoatButton.interactable = false;

                SwitchUpgradeUI();
                if (!UpgradeActions.dockFixed) {
                    UpdateButtons(MaterialManager.dockCost, upgradeDockButton);
                }
                else if (UpgradeActions.dockFixed && !UpgradeActions.boatFixed){
                    UpdateButtons(MaterialManager.boatCost, upgradeBoatButton);
                }
            }
            else
            {
                Debug.LogWarning("MainInventoryGroup 'upgrade' is not assigned in the Inspector.");
            }
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (CheatCodeMenuGroup != null)
            {
                if (!cheatCodeManager.canvasActive) {
                    cheatCodeManager.SwitchUI();
                    CloseAll();
                    return;
                }
                
                
                inventoryActive = false;
                upgradeActive = false;
                processActive = false;
                buildActive = false;
                ProcessMenuGroup.SetActive(processActive);
                MainInventoryGroup.SetActive(inventoryActive);
                UpgradeMenuGroup.SetActive(upgradeActive);
                BuildMenuGroup.SetActive(buildActive);

                cheatActive = !cheatActive;
                CursorSwitch(cheatActive);
                CheatCodeMenuGroup.SetActive(cheatActive);
            }
            else
            {
                Debug.LogWarning("MainInventoryGroup 'build' is not assigned in the Inspector.");
            }
        }
    }

    void UpdateButtons(List<int> cost, Button button) {
        var matNum = MaterialManager.getAllMatNum();
        int numWood = matNum.Item1;
        int numStone = matNum.Item2;
        int numPlank = matNum.Item3;
        int numBrick = matNum.Item4;

        if (numWood < cost[0]) {
            button.interactable = false;
            Debug.Log("Not enough wood.. Amount:" + numWood + " Cost: " + cost[0]);
        }
        else if (numStone < cost[1]) {
            button.interactable = false;
            Debug.Log("Not enough stone");
        }
        else if (numPlank < cost[2]) {
            button.interactable = false;
            Debug.Log("Not enough planks");
        }
        else if (numBrick < cost[3]) {
            button.interactable = false;
            Debug.Log("Not enough bricks");
        }
        else {
            button.interactable = true;
        }
    }
    
    public void CloseAll()
    {
        upgradeActive = false;
        inventoryActive = false;
        buildActive = false;
        processActive = false;
        cheatActive = false;


        ProcessMenuGroup.SetActive(processActive);
        UpgradeMenuGroup.SetActive(upgradeActive);
        BuildMenuGroup.SetActive(buildActive);
        MainInventoryGroup.SetActive(inventoryActive);
        CheatCodeMenuGroup.SetActive(cheatActive);
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

