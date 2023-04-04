using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelTwoObjectives : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI objectiveText = default;

    [SerializeField] private GameObject enemies;
    [SerializeField] private GameObject scientistHealthBar;

    public static bool hasScientist = false;
    private int objective = 1;

    void Start()
    {
        hasScientist = false;
    }

    private void Update()
    {
        // If player saved scientist
        if (hasScientist)
            objective++;

        if (objective == 2)
        {
            scientistHealthBar.SetActive(true);
            enemies.SetActive(true);
        }

        UpdateText();
    }

    private void UpdateText()
    {
        if (objective == 1)
            objectiveText.text = "Find and rescue the Scientist";

        else if (objective == 2)
            objectiveText.text = "Escape the facility";
    }
}