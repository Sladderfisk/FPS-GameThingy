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

    private List<CameraRecoil> shakes;
    private List<float> shakeDurations;

    private bool hasRecoilThisFrame;
    private float recoilDuration;
    private CameraRecoil currentRecoil;

    private float vertical;

    private Vector2 input;
    private Vector3 originalRot;
    

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        shakes = new List<CameraRecoil>();
        shakeDurations = new List<float>();
    }

    public void ShakeCamera(CameraRecoil shake)
    {
        shakes.Add(shake);
        shakeDurations.Add(0);
    }

    public void RecoilCamera(CameraRecoil recoil)
    {
        if (hasRecoilThisFrame) return;
        
        currentRecoil = recoil;
        recoilDuration = 0;
        
        hasRecoilThisFrame = true;
    }

    private void LateUpdate()
    {
        Transform tra = transform;
        
        for (int i = 0; i < shakes.Count; i++)
        {
            var recoil = shakes[i];

            if (shakeDurations[i] >= recoil.duration)
            {
                shakes.Remove(recoil);
                shakeDurations.RemoveAt(i);
                continue;
            }
            
            var rotX = Random.Range(-recoil.pithStrength, recoil.pithStrength);
            var rotY = Random.Range(-recoil.yawStrength, recoil.yawStrength);
            var rotZ = Random.Range(-recoil.rollStrength, recoil.rollStrength);

            vertical -= rotX;
            tra.Rotate(new (0, rotY, rotZ));
            
            shakeDurations[i] += Time.deltaTime;
        }

        if (hasRecoilThisFrame)
        {
            recoilDuration += Time.deltaTime;
            vertical = Mathf.Lerp(vertical, vertical - currentRecoil.pithStrength, Time.deltaTime / currentRecoil.duration);

            if (recoilDuration >= currentRecoil.duration) hasRecoilThisFrame = false;
        }
    }

    private void Update()
    {
        input.x = Input.GetAxis("Mouse X");
        input.y = Input.GetAxis("Mouse Y");
        
        vertical -= input.y * mouseSensitivity * Time.deltaTime;
        
        var eulerAngles = transform.eulerAngles;

        vertical = Mathf.Clamp(vertical, -90, 90);
        var horizontal = input.x * mouseSensitivity * Time.deltaTime;

        transform.eulerAngles = new(vertical, eulerAngles.y, 0);
        parent.Rotate(Vector3.up, horizontal);
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
