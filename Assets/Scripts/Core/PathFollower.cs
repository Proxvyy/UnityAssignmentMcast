using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public Path path;
    public float speed = 4f;

    int index = 0;

    void Start()
    {
        if (path == null || path.waypoints == null || path.waypoints.Count == 0)
        {
            Debug.LogError("No path assigned to " + gameObject.name);
            enabled = false;
            return;
        }

        transform.position = path.waypoints[0].position;
        index = 1;
    }

    void Update()
    {
        if (index >= path.waypoints.Count)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 target = path.waypoints[index].position;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            index++;
        }
    }
}
