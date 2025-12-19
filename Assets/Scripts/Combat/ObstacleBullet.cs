using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ObstacleBullet : MonoBehaviour
{
    [SerializeField] private float speed = 7f;
    [SerializeField] private float destroyAfterSeconds = 5f;
    [SerializeField] private int damageAmount = 1;

    private Rigidbody2D rb;
    private Vector2 moveDir = Vector2.down;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        // Apply velocity when spawned/enabled
        rb.linearVelocity = moveDir * speed;
        Destroy(gameObject, destroyAfterSeconds);
    }

    public void SetDirection(Vector2 direction)
    {
        if (direction.sqrMagnitude > 0.001f)
        {
            moveDir = direction.normalized;
            if (rb != null)
            {
                rb.linearVelocity = moveDir * speed;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerHealth health = other.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.TakeDamage(damageAmount);
        }

        Destroy(gameObject);
    }
}
