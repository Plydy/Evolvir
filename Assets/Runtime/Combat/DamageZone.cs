using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField, Min(0f)] private float damage = 25f;

    private void OnTriggerEnter(Collider other)
    {
        if (!(other is CharacterController))
            return;

        if (other.TryGetComponent(out HealthComponent health))
            health.TakeDamage(damage);
    }
}