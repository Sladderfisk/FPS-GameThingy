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

        var projectileObject = Instantiate(projectile.gameObject);
        projectileObject.SetActive(true);
        var shotProjectile = projectileObject.GetComponent<Projectile>();
        shotProjectile.Shot(this, firePoint.position, directionTra.forward);
        
        return true;
    }
}
