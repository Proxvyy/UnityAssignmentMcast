using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] string levelSceneName = "Level1";
    [SerializeField] string menuSceneName = "Menu";

    void Start()
    {
        ScoreManager scoreManager = FindFirstObjectByType<ScoreManager>();
        if (scoreManager != null)
        {
            scoreText.text = "Score: " + scoreManager.GetScore().ToString();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(levelSceneName);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }
}
