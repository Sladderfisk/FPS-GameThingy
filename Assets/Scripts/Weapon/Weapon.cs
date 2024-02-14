using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float damage;
    public float timeBetweenShots;
    public int magazineSize;
    public int Magazine { get; private set; }
    public int Ammunition { get; set; }
    
    protected static Camera mCam;

    private float lastFired;

    private void Start()
    {
        if (mCam == null) mCam = Camera.main;
    }

    public virtual bool Fire()
    {
        var firedDelta = Time.time - lastFired;
        if (firedDelta < timeBetweenShots) return false;
        lastFired = Time.time;
        
        if (Magazine > 1)
        {
            Reload();
            return false;
        }

        Magazine--;
        Shot();
        return true;
    }

    public void Reload()
    {
        if (Ammunition > magazineSize)
        {
            Magazine = Ammunition;
            Ammunition = 0;
        }

        Magazine = magazineSize;
        Ammunition -= magazineSize;
    }

    private void Shot()
    {
        
    }
}