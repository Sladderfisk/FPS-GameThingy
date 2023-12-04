using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Serialization;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float explosionRadius;
    [SerializeField] private float explosionEffect;

    private Vector3 velocity;

    private ProjectileWeapon shooter;
    private Rigidbody proRigidbody;

    private void OnEnable()
    {
        proRigidbody = GetComponent<Rigidbody>();
    }

    public void Shot(ProjectileWeapon shooter, Vector3 pos, Vector3 dir)
    {
        this.shooter = shooter;
        transform.position = pos;
        transform.LookAt(pos + dir);
        velocity = dir * maxSpeed;
    }

    private void Update()
    {
        proRigidbody.velocity = velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var hit = Physics.OverlapSphere(transform.position, explosionRadius);
        
        PushObjectsBack(hit);
        
        // Play explosion effect!
        Destroy(gameObject);
    }

    private void PushObjectsBack(Collider[] objects)
    {
        foreach (var hit in objects)
        {
            var rb = hit.attachedRigidbody;
            if (rb == null) continue;
            
            var deltaPos = transform.position - hit.transform.position;
            var dir = -deltaPos.normalized;
            
            var distance = Vector3.Distance(transform.position, hit.transform.position);

            var effect = 1 - distance / explosionRadius;
            var force = explosionEffect * effect;
            
            rb.AddForce(dir * force);
        }
    }
}
