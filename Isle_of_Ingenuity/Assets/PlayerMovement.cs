using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10;
    
    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float xspeed = Input.GetAxisRaw("Horizontal");
        float zspeed = Input.GetAxisRaw("Vertical");

        rb.linearVelocity = new Vector3(xspeed, rb.linearVelocity.y, zspeed) * speed;
    }
}
