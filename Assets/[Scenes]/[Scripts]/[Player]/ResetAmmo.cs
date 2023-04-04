using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAmmo : MonoBehaviour
{
    void Awake()
    {
        Variables.meds = 0;
        Variables.health = 100;

        Variables.pistolMagAmmo = 0;
        Variables.pistolStockAmmo = 0;
        Variables.shotgunMagAmmo = 0;
        Variables.shotgunStockAmmo = 0;
        Variables.rifleMagAmmo = 0;
        Variables.rifleStockAmmo = 0;
    }
}