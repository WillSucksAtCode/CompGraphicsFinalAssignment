using System.Collections;
using UnityEngine;

public class LockInteraction : Interactable
{
    [SerializeField] private GameObject numPadUI;
    [SerializeField] private GameObject interactText;
    [SerializeField] private GameObject crosshair;

    public static bool enteringPassword = false;

    public override void OnInteract()
    {
        if (ObjectiveManager.codePieces == 2)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            numPadUI.SetActive(true);
            PauseMenu.inPasscode = true;

            enteringPassword = true;
            interactText.SetActive(false);
            crosshair.SetActive(false);
        }
    }

    public override void OnFocus()
    {
        if (ObjectiveManager.codePieces == 2)
        {
            if (!enteringPassword)
            {
                interactText.SetActive(true);
                crosshair.SetActive(true);
            }
        }
    }

    public override void OnLoseFocus()
    {
        interactText.SetActive(false);
    }
}