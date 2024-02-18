﻿using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerWeaponHandler : MonoBehaviour, IDamageable
{
        [SerializeField] private float startHealth;
        [SerializeField] private PlayerCameraController.CameraRecoil recoil;
        [SerializeField] private float scrollWheelBreakPoint;
        [Space]
        [SerializeField] private Weapon[] weapons;
        [SerializeField] private RectTransform worldTextCanvas;
        [SerializeField] private WorldSpaceText worldSpaceText; 
        
        private Volume postProcessing;

        private Weapon currentWeapon;

        private float health;
        private int currentWeaponIndex;
        private float scrollWheelDelta;

        public Weapon CurrentWeapon => currentWeapon;

        private void Start()
        {
                postProcessing = FindObjectOfType<Volume>();

                health = startHealth;
                
                foreach (var weapon in weapons)
                {
                        weapon.gameObject.SetActive(false);
                }

                SwitchWeapon(weapons[0]);
        }

        private void Update()
        {
                HandleWeaponSwitching();
                FireWeapon();
                
                if (Input.GetKeyDown(KeyCode.R)) currentWeapon?.Reload();
                if (Input.GetKeyDown(KeyCode.G))
                {
                        FindObjectOfType<PlayerCameraController>().ShakeCamera(recoil);
                }
        }

        private void FireWeapon()
        {
                if (currentWeapon == null) return;

                if (currentWeapon.isAutomatic) { if (Input.GetMouseButton(0)) currentWeapon.Fire(); }
                else if (Input.GetMouseButtonDown(0)) currentWeapon.Fire();
        }

        private void HandleWeaponSwitching()
        {
                if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchWeapon(0);
                if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchWeapon(1);
                if (Input.GetKeyDown(KeyCode.Alpha3)) SwitchWeapon(2);

                        scrollWheelDelta += Input.mouseScrollDelta.y;

                if (Mathf.Abs(scrollWheelDelta) < scrollWheelBreakPoint) return;
                
                NextWeapon((int)Mathf.Sign(scrollWheelDelta));
                SwitchWeapon(weapons[currentWeaponIndex]);

                scrollWheelDelta = 0;
        }

        private void SwitchWeapon(int index)
        {
                SwitchWeapon(weapons[index]);
                currentWeaponIndex = index;
        }

        private void SwitchWeapon(Weapon newWeapon)
        {
                if (currentWeapon != null) currentWeapon.gameObject.SetActive(false);
                currentWeapon = newWeapon;
                currentWeapon.gameObject.SetActive(true);
        }

        /// <summary></summary>
        /// <param name="next">
        ///     Next has to be 1 or -1.
        /// </param>
        private void NextWeapon(int next)
        {
                if (!(next is 1 or -1))
                {
                        Debug.LogWarning("next has to be 1 or -1! and not: " + next);
                        return;
                }

                var length = weapons.Length;
                var newIndex = currentWeaponIndex + next;

                if (newIndex < 0) newIndex += length;
                else if (newIndex >= length) newIndex -= length;
                
                currentWeaponIndex = newIndex;
        }

        public void OnHit(WeaponHit weaponHit)
        {
                health -= weaponHit.damage;

                postProcessing.profile.TryGet(out Vignette vignette);
                vignette.intensity.value = 1 - health / 100;
                
                // Jag ville också ändra saturation men det fungerade inte. Den bettedde sig gätte kånstigt,

                if (health <= 0) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
}