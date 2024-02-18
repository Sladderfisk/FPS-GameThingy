using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private Slider reloadTimeSlider;
    [SerializeField] private PlayerWeaponHandler weaponHandler;

    private void Update()
    {
        if (weaponHandler.CurrentWeapon.wontReload) return;
        
        SetAmmoText();
        SetReloadTimer();
    }

    private void SetAmmoText()
    {
        var weapon = weaponHandler.CurrentWeapon;

        var magazine = weapon.Magazine;
        var ammunition = weapon.Ammunition;

        string text = magazine + "/" + ammunition;
        ammoText.text = text;
    }

    private void SetReloadTimer()
    {
        var weapon = weaponHandler.CurrentWeapon;
        var isReloading = weapon.IsReloading;
        
        reloadTimeSlider.gameObject.SetActive(isReloading);

        if (!isReloading)
        {
            reloadTimeSlider.value = 0;
            return;
        }

        reloadTimeSlider.maxValue = weapon.reloadTime;
        reloadTimeSlider.value = weapon.CurrentReloadTime;
    }
}
