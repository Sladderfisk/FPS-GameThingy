using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private float mouseSensitivity;

    private float vertical;

    private Vector2 input;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        input.x = Input.GetAxis("Mouse X");
        input.y = Input.GetAxis("Mouse Y");
        
        vertical += input.y * mouseSensitivity * Time.deltaTime;
        
        vertical = Mathf.Clamp(vertical, -90, 90);
        var horizontal = input.x * mouseSensitivity * Time.deltaTime;

        var eulerAngles = transform.eulerAngles;
        
        transform.eulerAngles = new(-vertical, eulerAngles.y, eulerAngles.z);
        parent.Rotate(Vector3.up, horizontal);
    }
}
