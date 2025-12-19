using UnityEngine;

public class ObstacleBullet : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float lifeTime = 5f;

    private Transform target;
    private Vector3 direction;

    public void SetTarget(Transform player)
    {
        target = player;
        direction = (target.position - transform.position).normalized;
    }

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.position = transform.position + direction * speed * Time.deltaTime;
    }
}
