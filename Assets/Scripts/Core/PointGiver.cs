using UnityEngine;

public class PointGiver : MonoBehaviour
{
    [SerializeField] int points = 5;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager scoreManager = FindFirstObjectByType<ScoreManager>();
            if (scoreManager != null)
            {
                scoreManager.AddPoints(points);
            }

            Destroy(gameObject);
        }
    }
}
