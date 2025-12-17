using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    PlayerHealth health;

    void Awake()
    {
        health = GetComponent<PlayerHealth>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponentInParent<DamageDealer>();

        if (damageDealer != null)
        {
            health.TakeDamage(damageDealer.GetDamage());
            damageDealer.Hit();
        }
    }
}
