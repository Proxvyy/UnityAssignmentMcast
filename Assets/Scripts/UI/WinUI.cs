using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WinUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text finalScoreText;

    [Header("Scene Names")]
    [SerializeField] private string menuSceneName = "Menu";

    void Start()
    {
        int score = 0;

        if (ScoreManager.instance != null)
        {
            score = ScoreManager.instance.GetScore();
        }

        if (finalScoreText != null)
        {
            finalScoreText.text = "Score: " + score;
        }
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }
}
