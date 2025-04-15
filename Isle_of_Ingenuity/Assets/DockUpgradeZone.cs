using UnityEngine;

public class DockUpgradeZone : MonoBehaviour
{
    public bool canOpenUpgradeScreen = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("UpgradeStation")) // Or check for a component instead
        {
            canOpenUpgradeScreen = true;
            Debug.LogWarning("Player entered upgrade region " + canOpenUpgradeScreen);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("UpgradeStation"))
        {
            canOpenUpgradeScreen = false;
            Debug.LogWarning("Player left upgrade region " + canOpenUpgradeScreen);
        }
    }
}
