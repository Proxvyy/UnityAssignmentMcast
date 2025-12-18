using UnityEngine;

public class PathFollower : MonoBehaviour
{
    Path path;
    float speed;

    int index = 0;

    public void SetPath(Path newPath)
    {
        path = newPath;
        index = 0;

        if (path != null && path.waypoints != null && path.waypoints.Count > 0)
        {
            transform.position = path.waypoints[0].position;
            index = 1;
        }
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    void Update()
    {
        if (path == null || path.waypoints == null || path.waypoints.Count == 0)
        {
            return;
        }

        if (index >= path.waypoints.Count)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 target = path.waypoints[index].position;

        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            index++;
        }
    }
}
