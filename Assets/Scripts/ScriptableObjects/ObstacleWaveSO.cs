using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Waves/Obstacle Wave")]
public class ObstacleWaveSO : ScriptableObject
{
    [System.Serializable]
    public class SpawnEntry
    {
        public GameObject prefab;
        public int count = 1;
    }

    [Header("Spawn List")]
    public List<SpawnEntry> spawns = new List<SpawnEntry>();

    [Header("Path + Movement")]
    public GameObject pathPrefab;
    public float moveSpeed = 5f;
}
