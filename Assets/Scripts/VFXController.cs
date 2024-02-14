using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXController : MonoBehaviour
{
    private ParticleSystem particleSystem;

    private float killAfterTime;

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();

        killAfterTime = particleSystem.main.duration;
        StartCoroutine(KillAfterTime());
    }

    private IEnumerator KillAfterTime()
    {
        yield return new WaitForSeconds(killAfterTime);
        
        Destroy(gameObject);
    }
}
