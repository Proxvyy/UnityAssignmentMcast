using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;

    public float spawnInterval = 1.2f;
    public float minX = -2.5f;
    public float maxX = 2.5f;
    public float spawnY = 6f;

    float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            Spawn();
            timer = 0f;
        }
    }

    void Spawn()
    {
        float x = Random.Range(minX, maxX);
        Vector3 pos = new Vector3(x, spawnY, 0f);
        Instantiate(obstaclePrefab, pos, Quaternion.identity);
    }
}
