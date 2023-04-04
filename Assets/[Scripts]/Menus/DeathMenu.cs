using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    private StoreVariables sv;

    private void Start()
    {
        sv = GetComponentInParent<StoreVariables>();
        Variables.health = 100;
        Variables.meds = sv.GetMeds();
        Variables.pistolMagAmmo = sv.GetPistolMag();
        Variables.pistolStockAmmo = sv.GetPistolStock();
        Variables.shotgunMagAmmo = sv.GetShotgunMag();
        Variables.shotgunStockAmmo = sv.GetShotgunStock();
        Variables.rifleMagAmmo = sv.GetRifleMag();
        Variables.rifleStockAmmo = sv.GetRifleStock();
    }

    public void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnQuit()
    {
        SceneManager.LoadScene(3);
    }
}