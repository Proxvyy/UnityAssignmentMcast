using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [Header("Waves")]
    public ObstacleWaveSO[] waves;
    public int startingWaveIndex = 0;
    public bool loopWaves = true;

    [Header("Timing")]
    public float timeBetweenSpawns = 0.5f;
    public float timeBetweenWaves = 1.0f;

    int currentWaveIndex;

    void Start()
    {
        if (waves == null || waves.Length == 0)
        {
            Debug.LogError("WaveSpawner: No waves assigned.");
            return;
        }

        currentWaveIndex = Mathf.Clamp(startingWaveIndex, 0, waves.Length - 1);
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            ObstacleWaveSO wave = waves[currentWaveIndex];

            if (wave == null)
            {
                Debug.LogError("WaveSpawner: Wave is null at index " + currentWaveIndex);
                yield break;
            }

            if (wave.pathPrefab == null)
            {
                Debug.LogError("WaveSpawner: Path Prefab is missing in wave " + wave.name);
                yield break;
            }

            // IMPORTANT: instantiate the path prefab so waypoints exist in the scene + Awake runs
            GameObject pathObj = Instantiate(wave.pathPrefab);
            pathObj.name = wave.pathPrefab.name + "_Runtime";

            Path path = pathObj.GetComponent<Path>();
            if (path == null)
            {
                Debug.LogError("WaveSpawner: Path Prefab has no Path script in wave " + wave.name);
                Destroy(pathObj);
                yield break;
            }

            path.BuildWaypoints();

            if (path.waypoints == null || path.waypoints.Count == 0)
            {
                Debug.LogError("WaveSpawner: Path has no waypoints in wave " + wave.name);
                Destroy(pathObj);
                yield break;
            }

            // Spawn all entries
            for (int i = 0; i < wave.spawns.Count; i++)
            {
                ObstacleWaveSO.SpawnEntry entry = wave.spawns[i];
                if (entry == null || entry.prefab == null || entry.count <= 0)
                    continue;

                for (int c = 0; c < entry.count; c++)
                {
                    GameObject obj = Instantiate(entry.prefab);

                    PathFollower follower = obj.GetComponent<PathFollower>();
                    if (follower == null)
                    {
                        Debug.LogError("WaveSpawner: Spawned object has no PathFollower: " + obj.name);
                        Destroy(obj);
                    }
                    else
                    {
                        follower.SetPath(path);
                        follower.SetSpeed(wave.moveSpeed);
                    }

                    yield return new WaitForSeconds(timeBetweenSpawns);
                }
            }


            yield return new WaitForSeconds(timeBetweenWaves);

            currentWaveIndex++;

            if (currentWaveIndex >= waves.Length)
            {
                if (loopWaves) currentWaveIndex = 0;
                else yield break;
            }
        }
    }
}
