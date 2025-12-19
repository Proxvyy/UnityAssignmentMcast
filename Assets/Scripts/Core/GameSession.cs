using UnityEngine;

public class GameSession : MonoBehaviour
{
    public static GameSession instance;

    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth = 100;

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

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void SetCurrentHealth(int value)
    {
        currentHealth = value;

        if (currentHealth < 0)
            currentHealth = 0;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    public void ResetSession()
    {
        currentHealth = maxHealth;
    }
}
