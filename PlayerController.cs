
using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField] private float groundSpeed = 10f;
    [SerializeField] private float airSpeed = 5f;
    [SerializeField] private float xSensitivity = 10f;
    [SerializeField] private float ySensitivity = 10f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float groundDrag = 6f;
    [SerializeField] private float airDrag = 2f;
    
    [Header("Ground Detection")]
    [SerializeField] private float groundCheck = 0.4f;
    [SerializeField] private LayerMask groundMask;
    private bool isGrounded = false;

    private float playerHeight = 2f;
    private float speed = 0;
    private PlayerMovement motor;

    void Start()
    {
        motor = GetComponent<PlayerMovement>();
        
    }

    private void Update()
    {
        
        //Jump Code + Drag/Speed changes
        isGrounded = Physics.CheckSphere(transform.position - new Vector3(0,playerHeight/2,0), groundCheck,groundMask);
        if (isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                motor.Jump(jumpForce);
            }

            motor.setDrag(groundDrag);
            speed = groundSpeed;
        }
        else
        {
            motor.setDrag(airDrag);
            speed = airSpeed;
        }
        
        //take inputs and create movement vectors
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 horizontalMovement = transform.right * xMov;
        Vector3 verticalMovement = transform.forward * zMov;
        Vector3 velocity = (horizontalMovement + verticalMovement).normalized * speed;
        
        //apply movement
        motor.Move(velocity);
        
        //calculate rotation around Y-axis
        float yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 rotation = new Vector3(0f, yRotation, 0f) * xSensitivity;
        
        //apply rotation
        motor.Rotate(rotation);
        
        //calculate Camera rotation
        float xRotation = Input.GetAxisRaw("Mouse Y");
        Vector3 camRotation = new Vector3(xRotation, 0f, 0f) * ySensitivity;
        
        //Apply Camera Rotation
        motor.RotateCamera(camRotation);
        
    }
}
