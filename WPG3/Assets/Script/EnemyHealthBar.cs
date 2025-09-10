using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Health health; // script Health di enemy
    [SerializeField] private Image fillImage; // image merah

    private void Start()
    {
        if (health != null)
        {
            health.OnHealthChanged += UpdateHealthBar;
            UpdateHealthBar(health.currentHealth, health.maxHealth); // set awal
        }
    }

    private void UpdateHealthBar(int current, int max)
    {
        float fillAmount = (float)current / max;
        fillImage.fillAmount = fillAmount; // otomatis kurangi lebar merah
    }

    private void LateUpdate()
    {
        if (Camera.main != null)
            transform.LookAt(Camera.main.transform);
    }
    private void OnDestroy()
    {
        if (health != null)
            health.OnHealthChanged -= UpdateHealthBar;
    }
}
