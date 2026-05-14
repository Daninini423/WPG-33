using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public System.Action<int, int> OnHealthChanged;
    private EnemyHitFeedback hitFeedback;
    private void Awake()
    {
        currentHealth = maxHealth;

        hitFeedback = GetComponent<EnemyHitFeedback>();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);

        // HIT EFFECT
        if (hitFeedback != null)
        {
            hitFeedback.TakeHit();
        }

        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }



    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    void Die()
    {
        EnemyManager.aliveEnemies--;

        if (CompareTag("Base"))
        {
            FindFirstObjectByType<GameOverManager>().ShowGameOver();
        }


        Destroy(gameObject);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}