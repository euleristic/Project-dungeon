using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float mouseInputFactor;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Camera cam;
    [SerializeField] private float pushOut;
    private Rigidbody rb;
    private bool colliding, onGround;
    private Vector2 mouseInput;
    private Vector2 movementInput;
    void Start()
    {
        mouseInput.x = mouseInput.y = 0f;
        movementInput.x = movementInput.y = 0f;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        print("On Ground: " + onGround + ", Colliding: " + colliding);

        //Handling Mouse Input
        mouseInput.x += mouseInputFactor * Input.GetAxis("Mouse X");
        mouseInput.y += mouseInputFactor * Input.GetAxis("Mouse Y");

        if (mouseInput.y > 89f) mouseInput.y = 89f;
        if (mouseInput.y < -89f) mouseInput.y = -89f;
        if (mouseInput.x > 180f) mouseInput.x -= 360f;
        if (mouseInput.x < -180f) mouseInput.x += 360f;

        //Handling WASD Input
        movementInput.y = moveSpeed * Input.GetAxis("Vertical");
        movementInput.x = moveSpeed * Input.GetAxis("Horizontal");

        //Jumping
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rb.AddForce(0f, 200f, 0f);
        }


        //Output
        transform.rotation = Quaternion.Euler(0f, mouseInput.x, 0f);
        cam.transform.rotation = Quaternion.Euler(-mouseInput.y, mouseInput.x, 0f);
        rb.AddForce(transform.forward.x * movementInput.y, 0f, transform.forward.z * movementInput.y);
        rb.AddForce(transform.forward.z * movementInput.x, 0f, -transform.forward.x * movementInput.x);
        if (rb.velocity.magnitude > 0.5)
        {
            rb.velocity.Normalize();
            rb.velocity *= 0.5f;
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Floor"))
            onGround = true;
        else
            colliding = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Floor"))
            onGround = false;
        else
            colliding = false;
    }
}

