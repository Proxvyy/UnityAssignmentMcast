using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public List<ObstacleWaveSO> waves = new List<ObstacleWaveSO>();
    public List<Path> scenePaths = new List<Path>();

    public float timeBetweenObstacles = 0.5f;
    public float timeBetweenWaves = 2f;

    void Start()
    {
        StartCoroutine(SpawnWavesLoop());
    }

    IEnumerator SpawnWavesLoop()
    {
        while (true)
        {
            foreach (ObstacleWaveSO wave in waves)
            {
                int index = waves.IndexOf(wave);

                if (index < 0 || index >= scenePaths.Count || scenePaths[index] == null)
                {
                    Debug.LogError("Scene path missing for wave: " + wave.name);
                    yield break;
                }

                yield return StartCoroutine(SpawnWave(wave, scenePaths[index]));
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }
    }

    IEnumerator SpawnWave(ObstacleWaveSO wave, Path path)
    {
        for (int i = 0; i < wave.numberOfObstacles; i++)
        {
            GameObject obstacleObj = Instantiate(wave.obstaclePrefab, Vector3.zero, Quaternion.identity);

            PathFollower follower = obstacleObj.GetComponent<PathFollower>();
            follower.path = path;
            follower.speed = wave.moveSpeed;

            yield return new WaitForSeconds(timeBetweenObstacles);
        }
    }
}
