using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public Transform playerBody; // Assign your player object here
    public float mouseSensitivity = 2f;
    private float rotationX = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Horizontal rotation: turn the player
        playerBody.Rotate(Vector3.up * mouseX);

        // Vertical rotation: tilt the camera (this object)
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }
}