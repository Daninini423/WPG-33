using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Image fillImage;

    private void Start()
    {
        if (health != null)
        {
            health.OnHealthChanged += UpdateHealthBar;
            UpdateHealthBar(health.currentHealth, health.maxHealth);
        }
    }

    private void UpdateHealthBar(int current, int max)
    {
        if (isDestroyed || fillImage == null) return;

        fillImage.fillAmount = (float)current / max;
    }

    private void LateUpdate()
    {
        if (this == null) return;

        if (Camera.main != null)
        {
            transform.forward = Camera.main.transform.forward;
        }
    }

    private bool isDestroyed = false;

    private void OnDestroy()
    {
        isDestroyed = true;

        if (health != null)
            health.OnHealthChanged -= UpdateHealthBar;
    }


}