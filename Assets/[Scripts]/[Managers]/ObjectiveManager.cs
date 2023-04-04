using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI objectiveText = default;
    [SerializeField] private GameObject bunkerWaypoint;

    public static int codePieces = 0;
    private int objective = 1;

    void Start()
    {
        codePieces = 0;
    }

    private void Update()
    {
        // If player has both code pieces, change objective
        if (codePieces >= 2)
            objective++;

        // Bunker Waypoint
        if (objective == 1)
            bunkerWaypoint.GetComponent<CompassElement>().SetEnable(false);
        if (objective == 2)
            bunkerWaypoint.GetComponent<CompassElement>().SetEnable(true);

        UpdateText();
    }

    private void UpdateText()
    {
        if (objective == 1)
            objectiveText.text = $"Find the code pieces: {codePieces}/2";

        if (objective == 2)
            objectiveText.text = "Enter the code at the bunker";
    }
}