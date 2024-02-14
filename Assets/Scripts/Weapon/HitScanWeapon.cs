using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScanWeapon : Weapon
{
    [SerializeField] private Transform startFirePosition;
    [SerializeField] private GameObject hitParticle;

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
            var particle = Instantiate(hitParticle, hit.point, Quaternion.identity);
            //Debug.Log("Hit:     " + hit.point);
            
            if (hit.collider.gameObject.TryGetComponent<IDamageable>(out var enemyHit)) Damage(enemyHit, hit);
        }
    }

    private void Damage(IDamageable enemy, RaycastHit rayHit)
    {
        var hit = new WeaponHit(damage, rayHit.distance, transform, this);
        enemy.OnHit(hit);
    }
}
