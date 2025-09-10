using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Slider healthSlider;

    void Start()
    {
        if (health != null)
        {
            // set slider awal
            healthSlider.minValue = 0;
            healthSlider.maxValue = health.maxHealth;
            healthSlider.value = health.currentHealth;

            // subscribe ke event health
            health.OnHealthChanged += UpdateSlider;
        }
    }

    private void UpdateSlider(int current, int max)
    {
        healthSlider.value = current;
    }
}
