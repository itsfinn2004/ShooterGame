using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float mouseSensitivity = 2f;

    private float verticalRotation = 0f;
    public float upDownRange = 60f;

    private Rigidbody rb;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Rotation
        float horizontalRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0f, horizontalRotation, 0f);

        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        // Movement
        float moveForward = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        float moveSideways = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;

        Vector3 velocity = transform.forward * moveForward + transform.right * moveSideways;
        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);

        // Jumping (optional)
        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
    }
}