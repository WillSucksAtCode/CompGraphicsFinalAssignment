using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialObjectiveManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI objectiveText = default;
    [SerializeField] private GameObject supplyWaypoint;
    [SerializeField] private GameObject enemyWaypoint;
    [SerializeField] private GameObject transferWaypoint;
    [SerializeField] private GameObject fadeOutTrigger;

    [SerializeField] private GameObject enemy1;
    [SerializeField] private GameObject enemy2;
    [SerializeField] private GameObject enemy3;
    [SerializeField] private GameObject enemy4;

    [SerializeField] private GameObject medkit;
    [SerializeField] private GameObject pistolAmmo;
    [SerializeField] private GameObject shotgunAmmo;
    [SerializeField] private GameObject ARAmmo;

    private int objective = 1;

    private void Update()
    {
        // If player picked up all supplies, change objective
        if (!medkit.activeSelf && !pistolAmmo.activeSelf && !shotgunAmmo.activeSelf && !ARAmmo.activeSelf && objective == 1)
            objective++;

        if (objective == 2 && !enemy1.activeSelf && !enemy2.activeSelf && !enemy3.activeSelf && !enemy4.activeSelf)
        {
            objective++;
            fadeOutTrigger.SetActive(true);
        }

        // Change waypoints
        if (objective == 1)
        {
            supplyWaypoint.GetComponent<CompassElement>().SetEnable(true);
            enemyWaypoint.GetComponent<CompassElement>().SetEnable(false);
            transferWaypoint.GetComponent<CompassElement>().SetEnable(false);
        }
        else if (objective == 2)
        {
            supplyWaypoint.GetComponent<CompassElement>().SetEnable(false);
            enemyWaypoint.GetComponent<CompassElement>().SetEnable(true);
            transferWaypoint.GetComponent<CompassElement>().SetEnable(false);
        }
        else if (objective == 3)
        {
            supplyWaypoint.GetComponent<CompassElement>().SetEnable(false);
            enemyWaypoint.GetComponent<CompassElement>().SetEnable(false);
            transferWaypoint.GetComponent<CompassElement>().SetEnable(true);
        }

        
        UpdateText();
    }

    private void UpdateText()
    {
        if (objective == 1)
            objectiveText.text = "Collect supplies from the outpost";

        else if (objective == 2)
            objectiveText.text = "Eliminate the infected";

        else if (objective == 3)
            objectiveText.text = "Reach the extraction point";
    }
}