using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    ScoreManager scoreManager;

    void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        scoreManager = FindFirstObjectByType<ScoreManager>();
    }

    void Update()
    {
        if (scoreManager == null)
        {
            scoreManager = FindFirstObjectByType<ScoreManager>();
        }

        if (scoreManager != null)
        {
            scoreText.text = "Score: " + scoreManager.GetScore().ToString();
        }
    }
}
