using UnityEngine;
using UnityEngine.UI;

public class UpgradeActions : MonoBehaviour
{
    public GameObject fixedDockPrefab;
    public GameObject brokenDockInScene;

    public GameObject boatPosition;
    public GameObject boat;
    public Button fixDockButton;

    public Button buildBoatButton;


    public bool dockFixed = false;
    public bool boatFixed = false;

    private MaterialManager MaterialManager;

    void Start() {
        MaterialManager = FindAnyObjectByType<MaterialManager>();
    }

    public void FixDock()
    {
        if (brokenDockInScene != null && fixedDockPrefab != null)
        {
            Vector3 dockPosition = brokenDockInScene.transform.position;
            Vector3 dockPositionOffset = new Vector3(dockPosition.x, dockPosition.y - 2, dockPosition.z);
            Quaternion dockRotation = brokenDockInScene.transform.rotation;

            Destroy(brokenDockInScene);
            Instantiate(fixedDockPrefab, dockPositionOffset, dockRotation);

            MaterialManager.UpgradeCostDock();
            dockFixed = true;

            Debug.Log("Dock repaired!");
        }
        else
        {
            Debug.LogWarning("Dock references not set in UpgradeActions script.");
        }
    }

    public void BuildBoat()
    {
        if (boatPosition != null && boat != null)
        {
            Vector3 boatPlacePosition = boatPosition.transform.position;
            Quaternion boatPlaceRotation = boatPosition.transform.rotation;

            Instantiate(boat, boatPlacePosition, boatPlaceRotation);

            MaterialManager.UpgradeCostBoat();
            boatFixed = true;

            Debug.Log("Boat Built!");
        }
        else
        {
            Debug.LogWarning("Boat references not set in UpgradeActions script.");
        }
    }
}
