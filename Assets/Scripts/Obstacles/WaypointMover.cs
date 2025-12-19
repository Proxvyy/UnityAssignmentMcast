using UnityEngine;

public class WaypointMover : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float reachDistance = 0.1f;

    private Transform[] waypoints;
    private int currentIndex;

    public void InitialisePath(Transform pathRoot)
    {
        if (pathRoot == null)
        {
            waypoints = null;
            return;
        }

        int count = pathRoot.childCount;
        waypoints = new Transform[count];

        for (int i = 0; i < count; i++)
        {
            waypoints[i] = pathRoot.GetChild(i);
        }

        currentIndex = 0;
        transform.position = waypoints[0].position;
    }

    private void Update()
    {
        if (waypoints == null || waypoints.Length == 0)
        {
            return;
        }

        Transform target = waypoints[currentIndex];
        Vector3 direction = target.position - transform.position;

        if (direction.magnitude <= reachDistance)
        {
            currentIndex++;

            if (currentIndex >= waypoints.Length)
            {
                Destroy(gameObject);
                return;
            }

            target = waypoints[currentIndex];
            direction = target.position - transform.position;
        }

        transform.position = transform.position + direction.normalized * moveSpeed * Time.deltaTime;
    }

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }
}
