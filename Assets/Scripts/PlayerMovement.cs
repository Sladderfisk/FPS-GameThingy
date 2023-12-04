using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float size;
    [SerializeField] private float offset;
    [SerializeField] private float movementSpeed;
    [Space] 
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float airControl;
    [Space]
    [SerializeField] private float gravityAcceleration;
    [FormerlySerializedAs("maxFallVelocity")] [SerializeField] private float minFallVelocity;

    private bool jumpInput;
    private bool isGrounded;
    private float velocityY;
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
        input = input.normalized;
        
        jumpInput = Input.GetKeyDown(KeyCode.Space);

        var ray = new Ray(transform.position, -transform.up);
        isGrounded = Physics.SphereCast(ray, size,offset);
        Fall();
        Move();
        Jump();
        
        Controller.Move(Vector3.up * velocityY);
    }

    private void FixedUpdate()
    {
        
    }

    private void Jump()
    {
        //if (jumpInput) Debug.Log("Jump");

        if (jumpInput && isGrounded) velocityY += jumpVelocity;
    }

    private void Move()
    {
        var forward = transform.forward;
        var right = transform.right;
        
        var move = forward * (movementSpeed * input.y);
        var strafe = right * (movementSpeed * input.x);

        if (isGrounded) currentMovementVelocity = Time.deltaTime * (move + strafe);
        //else currentMovementVelocity = airControl * Time.deltaTime * (move + strafe);
        
        Controller.Move(currentMovementVelocity);
    }

    private void Fall()
    {
        if (isGrounded) 
            velocityY = 0;
        
        //else currentFallVelocity = Controller.velocity.y;
        
        velocityY -= gravityAcceleration * Time.deltaTime;
        if (velocityY < -minFallVelocity) velocityY = -minFallVelocity;

        var currentVelocity = Controller.velocity;
    }

    private void OnDrawGizmos()
    {
        var pos = transform.position;
        Gizmos.DrawWireSphere(new(pos.x, pos.y - offset, pos.z), size);
    }
}
