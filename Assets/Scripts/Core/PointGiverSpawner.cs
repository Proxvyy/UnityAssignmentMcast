using System.Collections;
using UnityEngine;

public class PointGiverSpawner : MonoBehaviour
{
    [SerializeField] PointGiverSpawnSO settings;
    [SerializeField] int totalToSpawn = 10;

    int spawnedCount = 0;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        if (settings == null || settings.pointGiverPrefab == null)
        {
            Debug.LogError("PointGiverSpawner settings or prefab is missing.");
            yield break;
        }

        while (spawnedCount < totalToSpawn)
        {
            float wait = Random.Range(settings.minSpawnTime, settings.maxSpawnTime);
            yield return new WaitForSeconds(wait);

            Instantiate(settings.pointGiverPrefab, transform.position, Quaternion.identity);
            spawnedCount++;
        }
    }
}
