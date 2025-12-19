using UnityEngine;

public class ObstacleShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float minShootDelay = 1f;
    [SerializeField] private float maxShootDelay = 2.5f;

    private Transform player;

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        ScheduleNextShot();
    }

    private void ScheduleNextShot()
    {
        float delay = Random.Range(minShootDelay, maxShootDelay);
        Invoke(nameof(Shoot), delay);
    }

    private void Shoot()
    {
        if (bulletPrefab == null || player == null)
        {
            ScheduleNextShot();
            return;
        }

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        ObstacleBullet bulletScript = bullet.GetComponent<ObstacleBullet>();
        if (bulletScript != null)
        {
            bulletScript.SetTarget(player);
        }

        ScheduleNextShot();
    }
}
