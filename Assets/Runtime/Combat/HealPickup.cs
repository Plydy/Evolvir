using UnityEngine;

public class HealPickup : MonoBehaviour
{
    [SerializeField, Min(0f)] private float healAmount = 25f;

    private void OnTriggerEnter(Collider other)
    {
        if (!(other is CharacterController))
            return;

        if (!other.TryGetComponent(out HealthComponent health))
            return;

        if (health.IsDead || health.CurrentHealth >= health.MaxHealth || healAmount <= 0f)
            return;

        health.Heal(healAmount);
        Destroy(gameObject);
    }
}