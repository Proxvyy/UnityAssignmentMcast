using UnityEngine;
using UnityEngine.SceneManagement;

public class PointGiverProgressManager : MonoBehaviour
{
    public static PointGiverProgressManager instance;

    [Header("Progress")]
    [SerializeField] private int totalToResolve = 10;

    [Header("Scene Names")]
    [SerializeField] private string level2SceneName = "Level2";

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

        if (resolved >= totalToResolve)
        {
            hasLoadedNext = true;
            SceneManager.LoadScene(level2SceneName);
        }
    }

    public int GetCollected()
    {
        return collectedCount;
    }

    public int GetMissed()
    {
        return missedCount;
    }

    public void ResetProgress()
    {
        collectedCount = 0;
        missedCount = 0;
        hasLoadedNext = false;
    }
}
