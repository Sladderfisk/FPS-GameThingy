using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    private Vector3 input;
    
    public CharacterController Controller { get; private set; }

    private void Awake()
    {
        Controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        var forward = transform.forward;
        var right = transform.right;
        
        var move = forward * (movementSpeed * input.y);
        var strafe = right * (movementSpeed * input.x);
        
        Controller.Move(Time.fixedDeltaTime * (move + strafe));
    }
}
