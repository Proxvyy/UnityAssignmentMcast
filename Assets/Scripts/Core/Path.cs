using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public List<Transform> waypoints = new List<Transform>();

    void Awake()
    {
        BuildWaypoints();
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        // Keeps the list correct in editor too
        BuildWaypoints();
    }
#endif

    public void BuildWaypoints()
    {
        waypoints.Clear();

        for (int i = 0; i < transform.childCount; i++)
        {
            waypoints.Add(transform.GetChild(i));
        }
    }
}
