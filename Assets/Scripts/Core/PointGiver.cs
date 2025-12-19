using UnityEngine;

public class PointGiver : MonoBehaviour
{
    [SerializeField] private int points = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.AddPoints(points);
        }

        if (PointGiverProgressManager.instance != null)
        {
            PointGiverProgressManager.instance.RegisterCollected();
        }

        Destroy(gameObject);
    }
}
