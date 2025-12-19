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

        // Spawn explosion particles
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        // Play explosion sound in the world (works even if player gets disabled)
        if (explosionClip != null)
        {
            AudioSource.PlayClipAtPoint(explosionClip, transform.position, explosionVolume);
        }

        // Reset session HP for next run
        if (GameSession.instance != null)
        {
            GameSession.instance.ResetSession();
        }

        // Disable player visuals + collisions so it "dies" immediately
        DisablePlayerObject();

        // IMPORTANT: Do NOT destroy the player before loading the scene,
        // because this coroutine runs on this component.
        StartCoroutine(LoadGameOverAfterDelay());
    }

    private void DisablePlayerObject()
    {
        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        int i = 0;
        while (i < colliders.Length)
        {
            colliders[i].enabled = false;
            i++;
        }

        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();
        int j = 0;
        while (j < renderers.Length)
        {
            renderers[j].enabled = false;
            j++;
        }

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.simulated = false;
        }
    }

    private IEnumerator LoadGameOverAfterDelay()
    {
        yield return new WaitForSeconds(gameOverDelay);
        SceneManager.LoadScene(gameOverSceneName);

        // Optional cleanup: if the player object still exists in this scene, destroy it.
        // (After LoadScene, this object will be destroyed anyway unless it was DontDestroyOnLoad)
        Destroy(gameObject);
    }

    public int GetHealth()
    {
        return currentHealth;
    }
}
