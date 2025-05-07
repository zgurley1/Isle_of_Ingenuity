using UnityEngine;
using Assets.SimpleSpinner;

public class Harvest : MonoBehaviour
{
    private Harvestable currentTarget;
    public SimpleSpinner spinner;

    void Start() {
        if (spinner == null) {
            spinner = GetComponentInChildren<SimpleSpinner>();
            if (spinner == null) {
                Debug.LogWarning("Spinner not assigned or found in children!");
            }
        }
    }

    void Update() {
        if (currentTarget != null && Input.GetMouseButtonDown(0)) {
            Item selectedTool = InventoryManager.instance.GetSelectedItem(true);
            // spinner.PlaySpin();
            
            Debug.Log($"[{gameObject.name}] calling Harvest()");
            currentTarget.Harvest(selectedTool, spinner);
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
