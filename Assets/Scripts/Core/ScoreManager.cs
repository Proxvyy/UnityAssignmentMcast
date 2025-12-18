using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    int score = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddPoints(int points)
    {
        score += points;
    }

    public int GetScore()
    {
        return score;
    }

    public void ResetScore()
    {
        score = 0;
    }
}
