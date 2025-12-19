using UnityEngine;
using UnityEngine.SceneManagement;

public class PointGiverProgressManager : MonoBehaviour
{
    public static PointGiverProgressManager instance;

    [Header("Progress")]
    [SerializeField] private int totalToResolve = 10;

    [Header("Scene Names (must match exactly)")]
    [SerializeField] private string level1SceneName = "Level1";
    [SerializeField] private string level2SceneName = "Level2";
    [SerializeField] private string winSceneName = "Win";

    private int collectedCount = 0;
    private int missedCount = 0;
    private bool hasLoadedNext = false;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == level1SceneName || scene.name == level2SceneName)
        {
            ResetProgress();
        }
    }

    public void RegisterCollected()
    {
        if (hasLoadedNext) return;

        collectedCount++;
        CheckCompletion();
    }

    public void RegisterMissed()
    {
        if (hasLoadedNext) return;

        missedCount++;
        CheckCompletion();
    }

    private void CheckCompletion()
    {
        int resolved = collectedCount + missedCount;

        if (resolved < totalToResolve)
            return;

        hasLoadedNext = true;

        string current = SceneManager.GetActiveScene().name;

        if (current == level1SceneName)
        {
            SceneManager.LoadScene(level2SceneName);
        }
        else if (current == level2SceneName)
        {
            SceneManager.LoadScene(winSceneName);
        }
    }

    public void ResetProgress()
    {
        collectedCount = 0;
        missedCount = 0;
        hasLoadedNext = false;
    }
}
