using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;


public class Enemy : MonoBehaviour, IDamageable
{
    [FormerlySerializedAs("health")] [SerializeField] private float startHealth;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float fallSpeed;
    [SerializeField] private float damage;
    [SerializeField] private float hitDistance;
    [FormerlySerializedAs("coolDown")] [SerializeField] private float coolDownTime;
    [SerializeField] private CharacterController controller;

    private float currentHealth;
    private float velocityY;
    private float timeSinceLastHit;

    private PlayerMovement player;

    public float StartHealth => startHealth;
    
    public event Action<WeaponHit, float> OnDamage;

    private void Awake()
    {
        currentHealth = startHealth;

        player = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        timeSinceLastHit += Time.deltaTime;
        
        TryDamagePlayer();
        Rotate();
        Fall();

        controller.Move((player.transform.position - transform.position).normalized * (movementSpeed * Time.deltaTime));
        controller.Move(Vector3.up * velocityY);
    }

    private void Rotate()
    {
        var angle = Mathf.Atan2(player.transform.position.x - transform.position.x,
            player.transform.position.z - transform.position.z);

        transform.eulerAngles = new(0, angle * Mathf.Rad2Deg, 0);
    }
    
    private void Fall()
    {
        if (controller.isGrounded) velocityY = 0;

        velocityY -= fallSpeed * Time.deltaTime;
        if (velocityY < -5) velocityY = -5;
    }

    private void TryDamagePlayer()
    {
        if (timeSinceLastHit < coolDownTime) return;
        if (!Physics.Raycast(transform.position, transform.forward, out var hit)) return;
        if (hit.distance > hitDistance) return;
        if (!hit.collider.CompareTag("Player")) return;
        
        hit.collider.GetComponent<IDamageable>().OnHit(new (damage, 0, transform, null));
        timeSinceLastHit = 0;
    }

    public void OnHit(WeaponHit hit)
    {
        currentHealth -= hit.damage;
        OnDamage?.Invoke(hit, currentHealth);
        
        if (currentHealth <= 0) Destroy(gameObject);
    }
}
