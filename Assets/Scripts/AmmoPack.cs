using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : MonoBehaviour
{
    [SerializeField] private int ammoToGet;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        other.GetComponent<PlayerWeaponHandler>().CurrentWeapon.Ammunition += ammoToGet;
        Destroy(gameObject);
    }
}
