using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour
{

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;
    private Vector3 jumpVal = Vector3.zero;
    private bool JumpRequested = false;
    private float drag = 0f;
    private Rigidbody rb;
    [SerializeField] private Camera cam;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    public void Move(Vector3 velocity)
    {
        this.velocity = velocity;
    }

    void PerformMovement()
    {
        if (velocity != Vector3.zero)
            rb.MovePosition(transform.position + velocity * Time.fixedDeltaTime);
    }

    public void Rotate(Vector3 rotation)
    {
        this.rotation = rotation;
    }

    void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
    }

    public void RotateCamera(Vector3 cameraRotation)
    {
        this.cameraRotation = cameraRotation;
    }

    void PerformCameraRotation()
    {
        if(cam != null) cam.transform.Rotate(-cameraRotation);
    }

    public void Jump(float jumpForce)
    {
        jumpVal = transform.up * jumpForce;
        JumpRequested = true;
    }

    void PerformJump()
    {
        if (JumpRequested)
        {
            rb.AddForce(jumpVal,ForceMode.Impulse);
            JumpRequested = false;
        }
        
    }

    public void setDrag(float drag)
    {
        this.drag = drag;
    }

    void PerformDragUpdate()
    {
        rb.drag = drag;
    }

    void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
        PerformCameraRotation();
        PerformJump();
        PerformDragUpdate();
    }
}
