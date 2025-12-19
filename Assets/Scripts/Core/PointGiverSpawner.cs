using System.Collections;
using UnityEngine;

public class PointGiverSpawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private PointGiverSpawnSO settings;

    [Header("Spawn Control")]
    [SerializeField] private Transform spawnRail;
    [SerializeField] private int totalToSpawn = 10;
    [SerializeField] private float spawnY = 7f;

    private int spawnedCount = 0;

    void Start()
    {
        if (settings == null)
        {
            Debug.LogError("PointGiverSpawner: No PointGiverSpawnSO assigned.");
            return;
        }

        if (spawnRail == null)
        {
            Debug.LogError("PointGiverSpawner: No Spawn Rail assigned.");
            return;
        }

        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (spawnedCount < totalToSpawn)
        {
            float waitTime = Random.Range(
                settings.minSpawnTime,
                settings.maxSpawnTime
            );

            yield return new WaitForSeconds(waitTime);

            Vector3 spawnPos = new Vector3(
                spawnRail.position.x,
                spawnY,
                0f
            );

            Instantiate(
                settings.pointGiverPrefab,
                spawnPos,
                Quaternion.identity
            );

            spawnedCount++;
        }
    }
}
