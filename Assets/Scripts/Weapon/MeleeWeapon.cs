using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [SerializeField] private Animator animator;
    [SerializeField] private MeleeFire[] meleeCombo;
    [SerializeField] private float timeToLoseCombo;
    [SerializeField] private float maxHitDistance;

    private float timeSinceLastCombo;
    private int comboIndex;

    private void Awake()
    {
        magazineSize = int.MaxValue;
        Ammunition = int.MaxValue;
    }

    private void Update()
    {
        timeSinceLastCombo += Time.deltaTime;
    }

    public override bool Fire()
    {
        if (!base.Fire()) return false;

        if (timeSinceLastCombo >= timeToLoseCombo) comboIndex = 0;
        else
        {
            comboIndex++;
            if (comboIndex >= meleeCombo.Length) comboIndex = 0;
        }
        
        timeBetweenShots = meleeCombo[comboIndex].time;
        timeSinceLastCombo = 0;
        
        TryToDamage();
        
        return true;
    }

    private void TryToDamage()
    {
        animator.SetTrigger(meleeCombo[comboIndex].animationTrigger);

        Transform tra = Camera.main.transform;
        if (Physics.Raycast(tra.position, tra.forward, out var hit))
        {
            if (hit.distance > maxHitDistance) return;

            if (hit.collider.TryGetComponent<IDamageable>(out var entity))
            {
                entity.OnHit(new (meleeCombo[comboIndex].damage, hit.distance, transform, this));
                Instantiate(onHitEffect, hit.point, Quaternion.identity);
            }
        }
    }

    [Serializable]
    public struct MeleeFire
    {
        public float damage;
        public string animationTrigger;
        public float time;
    }
}
