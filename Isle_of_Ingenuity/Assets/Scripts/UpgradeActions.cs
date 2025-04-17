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




    public void FixDock()
    {
        if (brokenDockInScene != null && fixedDockPrefab != null)
        {
            Vector3 dockPosition = brokenDockInScene.transform.position;
            Vector3 dockPositionOffset = new Vector3(dockPosition.x, dockPosition.y - 2, dockPosition.z);
            Quaternion dockRotation = brokenDockInScene.transform.rotation;

            Destroy(brokenDockInScene);
            Instantiate(fixedDockPrefab, dockPositionOffset, dockRotation);

            

            if (fixDockButton != null)
            {
                fixDockButton.interactable = false;
            }
            if (buildBoatButton != null)
            {
                buildBoatButton.interactable= true;
            }

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

            Destroy(boatPosition);
            Instantiate(boat, boatPlacePosition, boatPlaceRotation);

            if (buildBoatButton != null)
            {
                buildBoatButton.interactable = false;
            }

            Debug.Log("Boat Built!");
        }
        else
        {
            Debug.LogWarning("Boat references not set in UpgradeActions script.");
        }
    }
}
