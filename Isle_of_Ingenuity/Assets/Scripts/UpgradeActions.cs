using UnityEngine;
using UnityEngine.UI;

public class UpgradeActions : MonoBehaviour
{
    public GameObject fixedDockPrefab;
    public GameObject brokenDockInScene;
    public Button fixDockButton;

    public void FixDock()
    {
        if (brokenDockInScene != null && fixedDockPrefab != null)
        {
            Vector3 dockPosition = brokenDockInScene.transform.position;
            Quaternion dockRotation = brokenDockInScene.transform.rotation;

            Destroy(brokenDockInScene);
            Instantiate(fixedDockPrefab, dockPosition, dockRotation);

            if (fixDockButton != null)
            {
                fixDockButton.interactable = false;
            }

            Debug.Log("Dock repaired!");
        }
        else
        {
            Debug.LogWarning("Dock references not set in UpgradeActions script.");
        }
    }
}
