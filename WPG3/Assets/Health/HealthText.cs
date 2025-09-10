using UnityEngine;
using UnityEngine.UI;

public class HealthText : MonoBehaviour
{
    [SerializeField] private Health health;   // Reference ke Health script
    [SerializeField] private Text healthText; // Reference ke UI Text


    void Start()
    {
        if (health == null)
        {
            health = GetComponent<Health>(); // fallback kalau lupa isi di Inspector
        }

        if (health != null)
        {
            // Subscribe ke event perubahan health
            health.OnHealthChanged += UpdateHealthUI;

            // Refresh UI pertama kali
            UpdateHealthUI(health.currentHealth, health.maxHealth);
        }
        else
        {
            Debug.LogWarning("HealthText: Tidak ada komponen Health!");
        }
    }

    private void UpdateHealthUI(int current, int max)
    {
        healthText.text = current + "/" + max;
    }
}
