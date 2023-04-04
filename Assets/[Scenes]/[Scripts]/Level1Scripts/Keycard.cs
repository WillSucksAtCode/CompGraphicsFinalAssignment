using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycard : Interactable
{
    [SerializeField] private GameObject interactText;

    [SerializeField] private GameObject pickupManager;
    private ItemPickupManager itemPickup;

    void Start()
    {
        itemPickup = pickupManager.GetComponent<ItemPickupManager>();
    }

    public override void OnInteract()
    {
        LevelOneObjectives.keycard = true;
        Elevator.hasKeycard = true;
        itemPickup.CollectKeycard();
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