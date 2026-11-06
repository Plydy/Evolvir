using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 100;

    public float CurrentHealth { get; private set; }
    
    public int MaxHealth => maxHealth;
    
    public bool IsDead => CurrentHealth <= 0;
   
    public event Action<float> OnHealthChanged;
    public event Action OnDeath;

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (CurrentHealth <= 0 || damage <= 0f)
            return;
        
        CurrentHealth -= damage;
        
        CurrentHealth = Mathf.Max(CurrentHealth, 0);
         
        Debug.Log($"Damage: {damage} | HP: {CurrentHealth}/{maxHealth}");
        
        OnHealthChanged?.Invoke(CurrentHealth);

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    [ContextMenu("Test Damage")]
    private void TestDamage()
    {
        TakeDamage(25f);
    }

    [ContextMenu("Test Heal")]
    private void TestHeal()
    {
        Heal(25f);
    }

    public void Heal(float amount)
    {
        if (CurrentHealth <= 0 || amount <= 0f)
            return;
        
        CurrentHealth += amount;
        
        CurrentHealth = Mathf.Min(CurrentHealth, maxHealth);
        
        Debug.Log($"Heal: {amount} | HP: {CurrentHealth}/{maxHealth}");
        
        OnHealthChanged?.Invoke(CurrentHealth);
    }
    
    private void Die()
    {
        OnDeath?.Invoke();
        Debug.Log($"{gameObject.name} died!");
    }
}
