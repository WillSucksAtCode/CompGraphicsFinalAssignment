using System.Collections;
using UnityEngine;

public class Code : Interactable
{
    [SerializeField] private GameObject CodeUI;
    [SerializeField] private GameObject interactText;

    public override void OnInteract()
    {
        CodeUI.SetActive(true);
        ObjectiveManager.codePieces++;
        this.gameObject.SetActive(false);
    }

    private IEnumerator ShowUI()
    {
        CodeUI.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        CodeUI.SetActive(false);
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