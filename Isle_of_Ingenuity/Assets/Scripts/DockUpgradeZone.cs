using UnityEngine;

public class DockUpgradeZone : MonoBehaviour
{
    public bool canOpenUpgradeScreen = false;
    public GameObject UpgradeMenu;
    public GameObject repairText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("UpgradeStation")) // Or check for a component instead
        {
            canOpenUpgradeScreen = true;
            repairText.SetActive(true);

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
