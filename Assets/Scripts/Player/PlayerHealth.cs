using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] int currentHealth;
    [SerializeField] string gameOverSceneName = "GameOver";

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        ScoreManager scoreManager = FindFirstObjectByType<ScoreManager>();
        if (scoreManager != null)
        {
            scoreManager.ResetScore();
        }

        SceneManager.LoadScene(gameOverSceneName);
    }

    public int GetHealth()
    {
        return currentHealth;
    }
}
