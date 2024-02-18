using System;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Weapon : MonoBehaviour
{
    public float damage;
    public float timeBetweenShots;
    public float reloadTime;
    public int magazineSize;
    public int startAmmunition;
    public bool isAutomatic;
    public bool wontReload;
    public GameObject onHitEffect;
    public PlayerCameraController.CameraRecoil recoil;

    public bool IsReloading { get; private set; }
    public int Magazine { get; private set; }
    public int Ammunition { get; set; }
    public float CurrentReloadTime {get; set; }
    
    protected static Camera mCam;

    private float lastFired;
    private static PlayerCameraController cameraController;

    private void Start()
    {
        if (cameraController == null) cameraController = FindObjectOfType<PlayerCameraController>();
        
        Ammunition = startAmmunition;
        if (mCam == null) mCam = Camera.main;
    }

    private void Update()
    {
        if (IsReloading) CurrentReloadTime += Time.deltaTime;
        if (CurrentReloadTime >= reloadTime) IsReloading = false;
    }

    private void OnDisable()
    {
        CurrentReloadTime = 0;
    }

    public virtual bool Fire()
    {
        if (!wontReload)
        {
            if (IsReloading) return false;

            if (Magazine < 1)
            {
                Reload();
                return false;
            }
        }
        
        var firedDelta = Time.time - lastFired;
        if (firedDelta < timeBetweenShots) return false;
        lastFired = Time.time;

        cameraController.RecoilCamera(recoil);
        Magazine--;
        return true;
    }

    public virtual void Reload()
    {
        if (Ammunition < 1 || Magazine >= magazineSize) return;

        IsReloading = true;
        CurrentReloadTime = 0;

        int deltaAmmo = magazineSize - Magazine;
        
        if (Ammunition < deltaAmmo)
        {
            Magazine = Ammunition;
            Ammunition = 0;
        }
        else
        {
            Magazine = magazineSize;
            Ammunition -= deltaAmmo;
        }
    }
}