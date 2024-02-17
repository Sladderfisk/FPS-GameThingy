using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private float mouseSensitivity;

    private List<CameraRecoil> recoils;
    private List<float> durations;

    //private float vertical;

    private Vector2 input;
    private Vector3 originalRot;
    

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        recoils = new List<CameraRecoil>();
        durations = new List<float>();
    }

    public void ShakeCamera(CameraRecoil recoil)
    {
        recoils.Add(recoil);
        durations.Add(0);
    }

    private void LateUpdate()
    {
        for (int i = 0; i < recoils.Count; i++)
        {
            var recoil = recoils[i];
            Debug.Log(recoil.duration);

            if (durations[i] >= recoil.duration)
            {
                recoils.Remove(recoil);
                durations.RemoveAt(i);
                continue;
            }
            
            var rotX = Random.Range(-recoil.pithStrength, recoil.pithStrength);
            var rotY = Random.Range(-recoil.yawStrength, recoil.yawStrength);
            var rotZ = Random.Range(-recoil.rollStrength, recoil.rollStrength);
            transform.Rotate(new (rotX, rotY, rotZ));
            
            durations[i] += Time.deltaTime;
        }
    }

    private void Update()
    {
        input.x = Input.GetAxis("Mouse X");
        input.y = Input.GetAxis("Mouse Y");
        
        var vertical = input.y * mouseSensitivity * Time.deltaTime;
        
        vertical = Mathf.Clamp(vertical, -90, 90);
        var horizontal = input.x * mouseSensitivity * Time.deltaTime;

        var eulerAngles = transform.eulerAngles;
        
        transform.eulerAngles = new(eulerAngles.x - vertical, eulerAngles.y + horizontal, 0);
        //parent.Rotate(Vector3.up, horizontal);
    }

    [Serializable]
    public struct CameraRecoil
    {
        public float pithStrength;
        public float yawStrength;
        public float rollStrength;
        public float duration;
    }
}
