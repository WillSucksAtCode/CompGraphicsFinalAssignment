using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText = default;
    [SerializeField] private Image healthBar;

    [SerializeField] private TextMeshProUGUI medText = default;

    public VolumeProfile mVolumeProfile;
    private Vignette mVignette;

    private FirstPersonController fpsController;

    private void OnEnable()
    {
        FirstPersonController.OnDamage += UpdateHealth;
        FirstPersonController.OnHeal += UpdateHealth;
    }

    private void OnDisable()
    {
        FirstPersonController.OnDamage -= UpdateHealth;
        FirstPersonController.OnHeal -= UpdateHealth;
    }

    private void Start()
    {
        fpsController = transform.Find("/-- Player --/FirstPersonController").GetComponent<FirstPersonController>();
        
        for (int i = 0; i < mVolumeProfile.components.Count; i++)
        {
            if (mVolumeProfile.components[i].name == "Vignette")
                mVignette = (Vignette)mVolumeProfile.components[i];
        }

        UpdateHealth(Variables.health);
    }

    private void UpdateHealth(float currentHealth)
    {
        float healthPercentage = currentHealth / fpsController.maxHealth;

        healthText.text = currentHealth.ToString("00");
        healthBar.fillAmount = healthPercentage;

        ClampedFloatParameter intensity = mVignette.intensity;
        intensity.value = 0.5f - (0.5f * healthPercentage);
    }

    public void UpdateMeds()
    {
        medText.text = Variables.meds.ToString("00");
    }
}