using UnityEngine;
using UnityEngine.SceneManagement;

public class PointGiverProgressManager : MonoBehaviour
{
    public static PointGiverProgressManager Instance;

    [SerializeField] int totalToResolve = 10;
    [SerializeField] string level2SceneName = "Level2";

    int resolvedCount = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterResolved()
    {
        resolvedCount++;

        if (resolvedCount >= totalToResolve)
        {
            SceneManager.LoadScene(level2SceneName);
        }
    }
}
