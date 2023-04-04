using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MedPack : Interactable
{
    private UI ui;
    [SerializeField] private GameObject interactText;

    [SerializeField] private GameObject pickupManager;
    private ItemPickupManager itemPickup;

    private void Start()
    {
        ui = transform.Find("/-- UI --/Canvas").GetComponent<UI>();
        itemPickup = pickupManager.GetComponent<ItemPickupManager>();
    }

    public override void OnInteract()
    {
        Variables.meds++;
        itemPickup.CollectMed();
        ui.UpdateMeds();
        GetComponent<FMODUnity.StudioEventEmitter>().Play();
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