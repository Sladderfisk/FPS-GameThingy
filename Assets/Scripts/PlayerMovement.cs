using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [Space] 
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float jumpTime;
    [SerializeField] private float airControl;
    [Space]
    [SerializeField] private float gravityAcceleration;
    [SerializeField] private float maxFallVelocity;

    private bool jumpInput;
    private bool isGrounded;
    private float currentJumpTime;
    private float currentFallVelocity;
    private Vector3 input;
    private Vector3 currentMovementVelocity;
    
    public CharacterController Controller { get; private set; }

    private void Awake()
    {
        Controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        jumpInput = Input.GetKeyDown(KeyCode.Space);
        
        isGrounded = Controller.isGrounded;
        
        Move();
        Fall();
        Jump();
    }

    private void FixedUpdate()
    {
        
    }

    private void Jump()
    {
        //if (jumpInput) Debug.Log("Jump");
        
        if (jumpInput && isGrounded)
        {
            currentJumpTime = 0;
        }

        currentJumpTime += Time.deltaTime;

        if (currentJumpTime < jumpTime) Controller.Move(Vector3.up * jumpVelocity);
    }

    private void Move()
    {
        var forward = transform.forward;
        var right = transform.right;
        
        var move = forward * (movementSpeed * input.y);
        var strafe = right * (movementSpeed * input.x);

        if (isGrounded) currentMovementVelocity = Time.deltaTime * (move + strafe);
        
        Controller.Move(currentMovementVelocity);
    }

    private void Fall()
    {
        if (isGrounded) currentFallVelocity = 0;
        //else currentFallVelocity = Controller.velocity.y;
        
        currentFallVelocity += gravityAcceleration * Time.deltaTime;
        if (currentFallVelocity > maxFallVelocity) currentFallVelocity = maxFallVelocity;

        var currentVelocity = Controller.velocity;
        Controller.Move(Vector3.down * currentFallVelocity);
    }
}
