using UnityEngine;

[CreateAssetMenu(menuName = "Spawning/PointGiver Spawn Settings")]
public class PointGiverSpawnSO : ScriptableObject
{
    public GameObject pointGiverPrefab;
    public float minSpawnTime = 0.5f;
    public float maxSpawnTime = 1.5f;
}
