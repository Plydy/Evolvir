using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private HealthComponent health;
    [SerializeField] private Image fill;

    private void OnEnable()
    {
        health.OnHealthChanged += UpdateHealth;
    }

    private void Start()
    {
        UpdateHealth(health.CurrentHealth);
    }

    private void OnDisable()
    {
        health.OnHealthChanged -= UpdateHealth;
    }

    private void UpdateHealth(float currentHealth)
    {
        fill.fillAmount = Mathf.Clamp01(currentHealth / health.MaxHealth);
    }
}