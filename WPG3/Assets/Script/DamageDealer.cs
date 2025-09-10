using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damageAmount = 10;
    public float damageCooldown = 1f; // 1 detik sekali serang
    private float lastDamageTime;


    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Base"))
        {
            if (Time.time - lastDamageTime >= damageCooldown)
            {
                Health health = collision.gameObject.GetComponent<Health>();
                if (health != null)
                {
                    health.TakeDamage(damageAmount);
                }
                lastDamageTime = Time.time;
            }
        }
    }
}
