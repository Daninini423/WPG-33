using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Image healthBarFill;
    [SerializeField] private TextMeshProUGUI healthText;

    void Start()
    {
        if (health != null)
        {
            health.OnHealthChanged += UpdateHealth;

            // update awal
            UpdateHealth(health.currentHealth, health.maxHealth);
        }
    }

    private void UpdateHealth(int current, int max)
    {
        if (healthBarFill == null) return;

        float fill = (float)current / max;
        healthBarFill.fillAmount = fill;
    }

    void OnDestroy()
    {
        if (health != null)
        {
            health.OnHealthChanged -= UpdateHealth;
        }
    }
}