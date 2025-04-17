using UnityEngine;

public class ResourceProcess : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private bool seek = true;
    private InventoryController InventoryController;
    void Start()
    {
        InventoryController = FindAnyObjectByType<InventoryController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!InventoryController.UIActive) {
            if (Camera.main == null)
                return;

            

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("ResourceProcessor"))
            {

                if (Input.GetMouseButtonDown(0)) // Left-click
                {
                    InventoryController.OpenProcessUI();
                }
            }
        }
    }
}
