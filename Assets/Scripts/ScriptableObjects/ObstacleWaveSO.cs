using UnityEngine;

[CreateAssetMenu(menuName = "Waves/Obstacle Wave", fileName = "NewObstacleWave")]
public class ObstacleWaveSO : ScriptableObject
{
    public GameObject obstaclePrefab;
    public GameObject pathPrefab;
    public float moveSpeed = 4f;
    public int numberOfObstacles = 5;
}
