using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Waves/Obstacle Wave")]
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
    public GameObject pathPrefab;     // <- MUST be public (this was your error)
    public float moveSpeed = 5f;      // <- MUST be public if WaveSpawner reads it
}
