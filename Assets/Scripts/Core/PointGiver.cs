using UnityEngine;

public class PointGiver : MonoBehaviour
{
    [SerializeField] private int points = 5;

    [Header("Audio")]
    [SerializeField, Range(0f, 2f)] private float pickupVolume = 1.2f;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

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

        if (audioSource != null)
        {
            audioSource.PlayOneShot(audioSource.clip, pickupVolume);
        }

        Destroy(gameObject, 0.15f);
    }
}
