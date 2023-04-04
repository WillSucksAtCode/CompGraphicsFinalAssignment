using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreVariables : MonoBehaviour
{
    int meds;
    int pistolMag;
    int pistolStock;
    int shotgunMag;
    int shotgunStock;
    int rifleMag;
    int rifleStock;

    void Start()
    {
        meds = Variables.meds;
        pistolMag = Variables.pistolMagAmmo;
        pistolStock = Variables.pistolStockAmmo;
        shotgunMag = Variables.shotgunMagAmmo;
        shotgunStock = Variables.shotgunStockAmmo;
        rifleMag = Variables.rifleMagAmmo;
        rifleStock = Variables.rifleStockAmmo;
    }

    public int GetMeds() { return meds; }
    public int GetPistolMag() { return pistolMag; }
    public int GetPistolStock() { return pistolStock; }
    public int GetShotgunMag() { return shotgunMag; }
    public int GetShotgunStock() { return shotgunStock; }
    public int GetRifleMag() { return rifleMag; }
    public int GetRifleStock() { return rifleStock; }
}