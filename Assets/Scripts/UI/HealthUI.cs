using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    TextMeshProUGUI healthText;
    PlayerHealth playerHealth;

    void Awake()
    {
        healthText = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
    }

    void Update()
    {
        if (playerHealth == null)
        {
            playerHealth = FindFirstObjectByType<PlayerHealth>();
        }

        if (playerHealth != null)
        {
            healthText.text = "HP: " + playerHealth.GetHealth().ToString();
        }
    }
}
