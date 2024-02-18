using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    [SerializeField] private Projectile projectile;

    [SerializeField] private Transform directionTra;
    [SerializeField] private Transform firePoint;
    
    public override bool Fire()
    {
        if (!base.Fire()) return false;

        var projectileObject = Instantiate(projectile);
        projectileObject.gameObject.SetActive(true);
        projectileObject.Shot(this, firePoint.position, directionTra.forward, damage);
        
        return true;
    }
}
