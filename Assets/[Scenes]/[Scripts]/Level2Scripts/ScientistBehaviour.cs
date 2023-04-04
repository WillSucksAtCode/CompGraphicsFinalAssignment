using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ScientistBehaviour : Interactable
{
    [SerializeField] private GameObject interactText;
    [SerializeField] private GameObject weaponHolder;
    [SerializeField] private GameObject scientistDeathScreen;

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform player;

    public static int health;
    [SerializeField] private Image healthBar;

    ScientistAnimController animController;

    public override void OnInteract()
    {
        LevelTwoObjectives.hasScientist = true;
        animController.StateSelector = ScientistAnimController.AnimState.Walk;
    }

    void Start()
    {
        health = 100;
        agent = GetComponent<NavMeshAgent>();
        animController = GetComponent<ScientistAnimController>();
        animController.StateSelector = ScientistAnimController.AnimState.Chair;
    }

    void Update()
    {
        if (LevelTwoObjectives.hasScientist)
        {
            agent.SetDestination(player.position);
            animController.StateSelector = ScientistAnimController.AnimState.Walk;

            float distance = Vector3.Distance(transform.position, player.position);
            if (distance < 3)
            {
                animController.StateSelector = ScientistAnimController.AnimState.Idle;
                agent.SetDestination(transform.position);
            }

            UpdateHealth(health);

            if (health <= 0)
                KillScientist();
        }
    }

    private void UpdateHealth(float currentHealth)
    {
        float healthPercentage = currentHealth / 100;
        healthBar.fillAmount = healthPercentage;
    }

    private void KillScientist()
    {
        Time.timeScale = 0;
        weaponHolder.SetActive(false);
        scientistDeathScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public override void OnFocus()
    {
        if (!LevelTwoObjectives.hasScientist)
            interactText.SetActive(true);
    }

    public override void OnLoseFocus()
    {
        interactText.SetActive(false);
    }
}