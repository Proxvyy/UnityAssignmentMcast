using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [Header("Waves (ScriptableObjects)")]
    [SerializeField] List<ObstacleWaveSO> waves = new List<ObstacleWaveSO>();
    [SerializeField] int startingWaveIndex = 0;
    [SerializeField] bool loopWaves = true;

    [Header("Timing")]
    [SerializeField] float timeBetweenSpawns = 0.5f;   // delay between each spawned object
    [SerializeField] float timeBetweenWaves = 1.0f;    // delay after finishing a wave

    [Header("Render (Optional)")]
    [SerializeField] float spawnZ = -10f; // keep spawned things above road

    Coroutine spawnRoutine;

    void Start()
    {
        if (waves == null || waves.Count == 0)
        {
            Debug.LogError("WaveSpawner: No waves assigned.");
            return;
        }

        if (startingWaveIndex < 0) startingWaveIndex = 0;
        if (startingWaveIndex >= waves.Count) startingWaveIndex = 0;

        spawnRoutine = StartCoroutine(SpawnWaveLoop());
    }

    IEnumerator SpawnWaveLoop()
    {
        int waveIndex = startingWaveIndex;

        while (true)
        {
            ObstacleWaveSO wave = waves[waveIndex];

            if (wave == null)
            {
                Debug.LogError("WaveSpawner: Wave is missing at index " + waveIndex);
                yield break;
            }

            yield return StartCoroutine(SpawnSingleWave(wave));
            yield return new WaitForSeconds(timeBetweenWaves);

            waveIndex++;

            if (waveIndex >= waves.Count)
            {
                if (loopWaves)
                {
                    waveIndex = 0;
                }
                else
                {
                    yield break;
                }
            }
        }
    }

    IEnumerator SpawnSingleWave(ObstacleWaveSO wave)
    {
        if (wave.PathPrefab == null)
        {
            Debug.LogError("WaveSpawner: PathPrefab missing in wave " + wave.name);
            yield break;
        }

        Path path = wave.PathPrefab.GetComponent<Path>();
        if (path == null)
        {
            Debug.LogError("WaveSpawner: PathPrefab has no Path component in wave " + wave.name);
            yield break;
        }

        if (path.waypoints == null || path.waypoints.Count == 0)
        {
            Debug.LogError("WaveSpawner: Path has no waypoints in wave " + wave.name);
            yield break;
        }

        // Spawn each entry in the wave spawn list (Obstacle, PointGiver, etc.)
        List<ObstacleWaveSO.SpawnEntry> spawnList = wave.SpawnList;
        if (spawnList == null || spawnList.Count == 0)
        {
            Debug.LogError("WaveSpawner: SpawnList is empty in wave " + wave.name);
            yield break;
        }

        int i;
        for (i = 0; i < spawnList.Count; i++)
        {
            ObstacleWaveSO.SpawnEntry entry = spawnList[i];

            if (entry.prefab == null)
            {
                Debug.LogError("WaveSpawner: Missing prefab in wave " + wave.name);
                continue;
            }

            int c;
            for (c = 0; c < entry.count; c++)
            {
                GameObject spawned = Instantiate(entry.prefab);

                // Spawn at first waypoint position
                Vector3 startPos = path.waypoints[0].position;
                spawned.transform.position = new Vector3(startPos.x, startPos.y, spawnZ);

                // Force the follower to use this wave path and speed
                PathFollower follower = spawned.GetComponent<PathFollower>();
                if (follower != null)
                {
                    follower.SetPath(path);
                    follower.SetSpeed(wave.MoveSpeed);
                }
                else
                {
                    Debug.LogError("WaveSpawner: " + spawned.name + " has no PathFollower component.");
                }

                yield return new WaitForSeconds(timeBetweenSpawns);
            }
        }
    }
}
