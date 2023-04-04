using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : Interactable
{
    [SerializeField] private GameObject interactText;
    [SerializeField] private GameObject noteImage;

    public override void OnInteract()
    {
        noteImage.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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