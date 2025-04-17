using UnityEngine;

public class FlyMovement : MonoBehaviour
{
    public float speed = 5f;
    public float sprintMultiplier = 2f;
    public float sensitivity = 2f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor to screen
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMouseLook();
        HandleMovement();
    }

    void HandleMouseLook()
    {
        rotationX += Input.GetAxis("Mouse X") * sensitivity;
        rotationY -= Input.GetAxis("Mouse Y") * sensitivity;
        rotationY = Mathf.Clamp(rotationY, -90f, 90f); // Prevent flipping

        transform.localRotation = Quaternion.Euler(rotationY, rotationX, 0f);
    }

    void HandleMovement()
    {
        float moveSpeed = speed * (Input.GetKey(KeyCode.LeftShift) ? sprintMultiplier : 1f);

        Vector3 moveDirection = new Vector3(
            Input.GetAxis("Horizontal"), 
            (Input.GetKey(KeyCode.Space) ? 1f : 0f) - (Input.GetKey(KeyCode.LeftControl) ? 1f : 0f), 
            Input.GetAxis("Vertical")
        );

        transform.position += transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime;
    }
}