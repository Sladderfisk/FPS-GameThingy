using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private float mouseSensitivity;

    private Vector2 input;

    private void Update()
    {
        input.x = Input.GetAxisRaw("Mouse X");
        input.y = Input.GetAxisRaw("Mouse Y");

        var deltaX = transform.eulerAngles.x + input.x;
        
        var vertical = Mathf.Clamp(deltaX, -89.9999f, 89.99999f) * mouseSensitivity * Time.deltaTime;
        var horizontal = input.y * mouseSensitivity * Time.deltaTime;
        
        transform.Rotate(Vector3.right, vertical);
        parent.Rotate(Vector3.up, horizontal);
    }
}
