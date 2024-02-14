using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct WeaponHit
{
    public float damage;
    public float distance;
    public Transform shooterTransform;
    public Weapon shotWeapon;

    public WeaponHit(float damage, float distance, Transform shooter, Weapon weapon)
    {
        this.damage = damage;
        this.distance = distance;
        shooterTransform = shooter;
        shotWeapon = weapon;
    }
}
