using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;


public class Enemy : MonoBehaviour, IDamageable
{
    [FormerlySerializedAs("health")] [SerializeField] private float startHealth;

    private float currentHealth;

    public float StartHealth => startHealth;
    
    public event Action<WeaponHit, float> OnDamage;

    private void Awake()
    {
        currentHealth = startHealth;
    }

    public void OnHit(WeaponHit hit)
    {
        currentHealth -= hit.damage;
        OnDamage?.Invoke(hit, currentHealth);
    }
}
