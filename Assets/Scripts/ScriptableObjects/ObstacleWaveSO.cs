using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Waves/Obstacle Wave", fileName = "Wave")]
public class ObstacleWaveSO : ScriptableObject
{
    [System.Serializable]
    public class SpawnEntry
    {
        public GameObject prefab;
        public int count;
    }

    [Header("Spawn List")]
    [SerializeField] List<SpawnEntry> spawns = new List<SpawnEntry>();

    [Header("Path + Movement")]
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float moveSpeed = 5f;

    // Getters used by WaveSpawner
    public List<SpawnEntry> SpawnList { get { return spawns; } }
    public GameObject PathPrefab { get { return pathPrefab; } }
    public float MoveSpeed { get { return moveSpeed; } }
}
