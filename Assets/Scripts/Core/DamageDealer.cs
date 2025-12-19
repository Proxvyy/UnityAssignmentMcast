using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] private int damage = 1;

    [Header("Hit Effects (optional)")]
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private AudioClip hitSfx;
    [SerializeField, Range(0f, 2f)] private float hitSfxVolume = 1f;

    public int GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        if (hitSfx != null)
        {
            AudioSource.PlayClipAtPoint(hitSfx, transform.position, hitSfxVolume);
        }

        Destroy(gameObject);
    }
}
