using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public GameObject obstaclePrefab;
        public Transform pathRoot;
        public int obstaclesToSpawn = 5;
        public float spawnDelay = 1f;
    }

    [Header("Waves (use 4 waves here)")]
    [SerializeField] private List<Wave> waves = new List<Wave>();

    [Header("Between Waves")]
    [SerializeField] private float delayBetweenWaves = 2f;

    private void Start()
    {
        StartCoroutine(RunWavesLoop());
    }

    private IEnumerator RunWavesLoop()
    {
        while (true)
        {
            foreach (Wave wave in waves)
            {
                yield return StartCoroutine(SpawnWave(wave));
                yield return new WaitForSeconds(delayBetweenWaves);
            }
        }
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        if (wave == null)
        {
            yield break;
        }

        if (wave.obstaclePrefab == null)
        {
            yield break;
        }

        if (wave.pathRoot == null)
        {
            yield break;
        }

        if (wave.pathRoot.childCount == 0)
        {
            yield break;
        }

        Vector3 spawnPos = wave.pathRoot.GetChild(0).position;

        for (int i = 0; i < wave.obstaclesToSpawn; i++)
        {
            GameObject obstacle = Instantiate(wave.obstaclePrefab, spawnPos, Quaternion.identity);

            WaypointMover mover = obstacle.GetComponent<WaypointMover>();
            if (mover != null)
            {
                mover.InitialisePath(wave.pathRoot);
            }

            yield return new WaitForSeconds(wave.spawnDelay);
        }
    }
}
