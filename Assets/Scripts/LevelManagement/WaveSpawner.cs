using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [Header("Waves")]
    [SerializeField] List<ObstacleWaveSO> waves = new List<ObstacleWaveSO>();
    [SerializeField] int startingWaveIndex = 0;
    [SerializeField] bool loopWaves = true;

    [Header("Timing")]
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float timeBetweenWaves = 1.0f;

    int currentWaveIndex = 0;

    // cache path lookups so we don't search every spawn
    Dictionary<string, Path> pathCache = new Dictionary<string, Path>();

    void Start()
    {
        if (waves == null || waves.Count == 0)
        {
            Debug.LogError("WaveSpawner has no waves assigned.");
            return;
        }

        if (startingWaveIndex < 0 || startingWaveIndex >= waves.Count)
        {
            startingWaveIndex = 0;
        }

        currentWaveIndex = startingWaveIndex;
        StartCoroutine(RunWaves());
    }

    IEnumerator RunWaves()
    {
        while (true)
        {
            ObstacleWaveSO wave = waves[currentWaveIndex];

            yield return StartCoroutine(SpawnWave(wave));

            yield return new WaitForSeconds(timeBetweenWaves);

            currentWaveIndex++;

            if (currentWaveIndex >= waves.Count)
            {
                if (loopWaves)
                {
                    currentWaveIndex = 0;
                }
                else
                {
                    yield break;
                }
            }
        }
    }

    IEnumerator SpawnWave(ObstacleWaveSO wave)
    {
        if (wave == null || wave.spawns == null || wave.spawns.Count == 0)
        {
            yield break;
        }

        Path scenePath = GetScenePathForWave(wave);

        for (int i = 0; i < wave.spawns.Count; i++)
        {
            ObstacleWaveSO.SpawnEntry entry = wave.spawns[i];

            if (entry.prefab == null || entry.count <= 0)
            {
                continue;
            }

            for (int j = 0; j < entry.count; j++)
            {
                SpawnOne(entry.prefab, scenePath, wave.moveSpeed);
                yield return new WaitForSeconds(timeBetweenSpawns);
            }
        }
    }

    void SpawnOne(GameObject prefabToSpawn, Path scenePath, float moveSpeed)
    {
        GameObject obj = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);

        PathFollower follower = obj.GetComponent<PathFollower>();
        if (follower != null)
        {
            if (scenePath != null)
            {
                follower.SetPath(scenePath);
            }

            follower.SetSpeed(moveSpeed);
        }
    }

    Path GetScenePathForWave(ObstacleWaveSO wave)
    {
        if (wave == null || wave.pathPrefab == null)
        {
            return null;
        }

        string pathName = wave.pathPrefab.name;

        if (pathCache.ContainsKey(pathName) && pathCache[pathName] != null)
        {
            return pathCache[pathName];
        }

        // Find the Path component on the Path_WaveX object IN THE SCENE
        Path[] allPaths = FindObjectsOfType<Path>();
        for (int i = 0; i < allPaths.Length; i++)
        {
            if (allPaths[i] != null && allPaths[i].gameObject.name == pathName)
            {
                pathCache[pathName] = allPaths[i];
                return allPaths[i];
            }
        }

        Debug.LogError("Could not find scene Path object named: " + pathName);
        return null;
    }
}
