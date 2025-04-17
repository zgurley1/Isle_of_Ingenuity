using UnityEngine;

public class Harvest : MonoBehaviour
{
    private Harvestable currentTarget;

    void Update() {
        if (currentTarget != null && Input.GetMouseButtonDown(0)) {
            Item selectedTool = InventoryManager.instance.GetSelectedItem(true);
            currentTarget.Harvest(selectedTool);
        }
    }

    private void OnTriggerEnter(Collider other) {
        Harvestable harvestable = other.GetComponent<Harvestable>();
        if (harvestable != null) {
            currentTarget = harvestable;
            Debug.Log("Entered range of " + other.name);
        }
    }

    private void OnTriggerExit(Collider other) {
        Harvestable harvestable = other.GetComponent<Harvestable>();
        if (harvestable != null && harvestable == currentTarget) {
            currentTarget = null;
            Debug.Log("Exited range of " + other.name);
        }
    }
}
