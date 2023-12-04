using System;
using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
        [SerializeField] private Weapon[] weapons;

        private void Update()
        {
                if (Input.GetMouseButton(0)) weapons[0].Fire();
        }
}