using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float mouseSensitivity = 2f;
    
    private bool isGrounded;
    private Rigidbody rb;
    // private float rotationX = 0f;
    // private float rotationY = 0f;

    public bool isInventoryOpen = false;
    public Transform cameraTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Cursor.lockState = CursorLockMode.Locked; // Lock cursor to center of screen
    }

    void Update()
    {
        // Player movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        rb.linearVelocity = new Vector3(move.x * moveSpeed, rb.linearVelocity.y, move.z * moveSpeed);
        
        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        // Mouse look
        // float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        // float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // rotationX -= mouseY;
        // rotationY += mouseX;
        // rotationX = Mathf.Clamp(rotationX, -90f, 90f); // Limit vertical rotation
        
        // transform.Rotate(Vector3.up * mouseX);
        // Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // Call this function from the InventoryController to pause and unpause the game
    // public void ToggleInventory(bool isOpen)
    // {
    //     isInventoryOpen = isOpen;

    //     if (isInventoryOpen)
    //     {
    //         Cursor.lockState = CursorLockMode.None; // Unlock cursor
    //         Cursor.visible = true; // Show cursor
    //     }
    //     else
    //     {
    //         Cursor.lockState = CursorLockMode.Locked; // Lock cursor again
    //         Cursor.visible = false; // Hide cursor
    //     }
    // }
}
