using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance;

    [Header("UI Elements")]
    public GameObject crosshair; // Assign a UI Image or prefab for the crosshair

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void EnableGameplayCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (crosshair != null)
            crosshair.SetActive(true);
    }

    public void EnableUICursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (crosshair != null)
            crosshair.SetActive(false);
    }
}
