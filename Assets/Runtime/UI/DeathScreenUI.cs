using UnityEngine;

public class DeathScreenUI : MonoBehaviour
{
    [SerializeField] private HealthComponent health;
    [SerializeField] private GameObject deathPanel;

    private void OnEnable()
    {
        health.OnDeath += ShowDeathScreen;
    }

    private void Start()
    {
        deathPanel.SetActive(health.IsDead);
    }

    private void OnDisable()
    {
        health.OnDeath -= ShowDeathScreen;
    }

    private void ShowDeathScreen()
    {
        deathPanel.SetActive(true);
    }
}   