using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;
    [SerializeField] private string gameOverSceneName = "GameOver";

    [Header("Audio")]
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private AudioClip explosionClip;
    [SerializeField, Range(0f, 2f)] private float hitVolume = 1f;
    [SerializeField, Range(0f, 2f)] private float explosionVolume = 1f;

    [Header("Death Effects")]
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float gameOverDelay = 0.6f;

    private AudioSource audioSource;
    private bool isDead = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (GameSession.instance != null)
        {
            maxHealth = GameSession.instance.GetMaxHealth();
            currentHealth = GameSession.instance.GetCurrentHealth();
        }
        else
        {
            currentHealth = maxHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
        {
            return;
        }

        currentHealth -= damage;

        if (audioSource != null && hitClip != null)
        {
            audioSource.PlayOneShot(hitClip, hitVolume);
        }

        if (GameSession.instance != null)
        {
            GameSession.instance.SetCurrentHealth(currentHealth);
            currentHealth = GameSession.instance.GetCurrentHealth();
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead)
        {
            return;
        }

        isDead = true;

        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        if (explosionClip != null)
        {
            AudioSource.PlayClipAtPoint(explosionClip, transform.position, explosionVolume);
        }

        if (GameSession.instance != null)
        {
            GameSession.instance.ResetSession();
        }

        Destroy(gameObject);

        StartCoroutine(LoadGameOverAfterDelay());
    }

    private IEnumerator LoadGameOverAfterDelay()
    {
        yield return new WaitForSeconds(gameOverDelay);
        SceneManager.LoadScene(gameOverSceneName);
    }

    public int GetHealth()
    {
        return currentHealth;
    }
}
