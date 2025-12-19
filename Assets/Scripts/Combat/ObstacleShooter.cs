using UnityEngine;

public class ObstacleShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float minShootDelay = 1.0f;
    [SerializeField] private float maxShootDelay = 2.5f;

    private Transform player;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        ScheduleNextShot();
    }

    void ScheduleNextShot()
    {
        float delay = Random.Range(minShootDelay, maxShootDelay);
        Invoke("Shoot", delay);
    }

    void Shoot()
    {
        if (bulletPrefab == null)
        {
            return;
        }

        GameObject bulletObj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        ObstacleBullet bullet = bulletObj.GetComponent<ObstacleBullet>();

        if (bullet != null && player != null)
        {
            Vector2 dir = (player.position - transform.position);
            bullet.SetDirection(dir);
        }

        ScheduleNextShot();
    }

    void OnDestroy()
    {
        CancelInvoke();
    }
}
