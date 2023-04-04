using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTwoElevator : Interactable
{
    [SerializeField] private GameObject interactText;
    [SerializeField] private GameObject fadeOutScreen;
    [SerializeField] private GameObject allUI;
    [SerializeField] private GameObject weaponHolder;

    public override void OnInteract()
    {
        if (LevelTwoObjectives.hasScientist)
        {
            fadeOutScreen.SetActive(true);
            allUI.SetActive(false);
            weaponHolder.SetActive(false);
            Invoke("EndLevel", 3);
        }
    }

    public override void OnFocus()
    {
        if (LevelTwoObjectives.hasScientist)
            interactText.SetActive(true);
    }

    public override void OnLoseFocus()
    {
        interactText.SetActive(false);
    }

    void EndLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}