using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX : MonoBehaviour
{
    private ParticleSystem particleSystem;

    private float lifeTime;

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime >= particleSystem.main.duration) Destroy(gameObject);
    }
}
