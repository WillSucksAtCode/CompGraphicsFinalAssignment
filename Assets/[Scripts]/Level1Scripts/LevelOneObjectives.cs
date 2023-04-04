using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelOneObjectives : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI objectiveText = default;

    public static bool keycard = false;
    private int objective = 1;

    void Start()
    {
        keycard = false;
    }

    private void Update()
    {
        // If player picked up keycard
        if (keycard)
            objective++;

        UpdateText();
    }

    private void UpdateText()
    {
        if (objective == 1)
            objectiveText.text = "Find a working keycard";

        else if (objective == 2)
            objectiveText.text = "Enter the elevator";
    }
}