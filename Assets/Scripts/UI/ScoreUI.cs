using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    void Start()
    {
        if (scoreText == null)
        {
            Debug.LogError("ScoreUI: scoreText is NOT assigned in Inspector.");
        }
        else
        {
            Debug.Log("ScoreUI Start: scoreText assigned OK.");
        }
    }

    void Update()
    {
        if (scoreText == null)
            return;

        if (ScoreManager.instance == null)
        {
            scoreText.text = "Score: ?";
            return;
        }

        scoreText.text = "Score: " + ScoreManager.instance.GetScore();
    }
}
