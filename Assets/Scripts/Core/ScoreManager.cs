using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] private int score = 0;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        Debug.Log("ScoreManager Awake: instance set. Current score = " + score);
    }

    public void AddPoints(int amount)
    {
        score += amount;
        Debug.Log("ScoreManager AddPoints: +" + amount + " => score now " + score);
    }

    public int GetScore()
    {
        return score;
    }

    public void ResetScore()
    {
        score = 0;
        Debug.Log("ScoreManager ResetScore: score now " + score);
    }
}
