using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    int score = 0;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
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
