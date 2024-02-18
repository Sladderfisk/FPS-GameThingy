using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HitScanWeapon : Weapon
{
    [SerializeField] private Transform startFirePosition;

    public override bool Fire()
    {
        if (!base.Fire()) return false;

        Shoot();
        return true;
    }

    private void Shoot()
    {
        var direction = mCam.transform.forward;
        if (Physics.Raycast(startFirePosition.position, direction, out var hit))
        {
            if (hit.collider.gameObject.TryGetComponent<IDamageable>(out var enemyHit)) Damage(enemyHit, hit);
        }
    }

    private void Damage(IDamageable enemy, RaycastHit rayHit)
    {
        var hit = new WeaponHit(damage, rayHit.distance, transform, this);
        enemy.OnHit(hit);
        
        Instantiate(onHitEffect, rayHit.point, quaternion.identity);
    }
}
