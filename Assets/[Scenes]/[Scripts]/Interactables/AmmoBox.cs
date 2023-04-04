using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : Interactable, IDataPersistence
{
    [SerializeField] private string id;

    [SerializeField] private bool pistolAmmo;
    [SerializeField] private bool shotgunAmmo;
    [SerializeField] private bool rifleAmmo;

    [SerializeField] private GameObject pistol;
    [SerializeField] private GameObject shotgun;
    [SerializeField] private GameObject rifle;

    [SerializeField] private GameObject interactText;

    private int minPistol = 8;
    private int maxPistol = 12;
    private int minshotgun = 4;
    private int maxshotgun = 6;
    private int minRifle = 30;
    private int maxRifle = 45;

    [SerializeField] private GameObject pickupManager;
    private ItemPickupManager itemPickup;

    private bool collected = false;
    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public void LoadData(GameData data)
    {
        data.AmmoBoxesCollected.TryGetValue(id, out collected);

        if (collected)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void SaveData(GameData data)
    {
        if (data.AmmoBoxesCollected.ContainsKey(id))
        {
            data.AmmoBoxesCollected.Remove(id);
        }
        
        data.AmmoBoxesCollected.Add(id, collected);
    }

    void Start()
    {
        itemPickup = pickupManager.GetComponent<ItemPickupManager>();
    }

    public override void OnInteract()
    {
        int amount = 0;

        if (pistolAmmo)
        {
            amount = Random.Range(minPistol, maxPistol);
            //pistol.GetComponent<Gun>().AddAmmo(amount);
            Variables.pistolStockAmmo += amount;
            itemPickup.CollectAmmo(amount, "Pistol");
            GetComponent<FMODUnity.StudioEventEmitter>().Play();
        }
        else if (shotgunAmmo)
        {
            amount = Random.Range(minshotgun, maxshotgun);
            //shotgun.GetComponent<Gun>().AddAmmo(amount);
            Variables.shotgunStockAmmo += amount;
            itemPickup.CollectAmmo(amount, "Shotgun");
            GetComponent<FMODUnity.StudioEventEmitter>().Play();
        }
        else if (rifleAmmo)
        {
            amount = Random.Range(minRifle, maxRifle);
            //rifle.GetComponent<Gun>().AddAmmo(amount);
            Variables.rifleStockAmmo += amount;
            itemPickup.CollectAmmo(amount, "Rifle");
            GetComponent<FMODUnity.StudioEventEmitter>().Play();
        }

        collected = true;
        this.gameObject.SetActive(false);
    }

    public override void OnFocus()
    {
        interactText.SetActive(true);
    }

    public override void OnLoseFocus()
    {
        interactText.SetActive(false);
    }
}