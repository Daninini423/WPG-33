using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform target;   // target tujuan (misalnya Base)
    public float speed = 3f;   // kecepatan jalan musuh
    public float stopDistance = 0.5f; // jarak berhenti dari target
    private Health baseHealth;
    private float attackTimer = 0f;
    public int attackDamage = 10;
    public float attackInterval = 1.5f;
    private bool hasReachedTarget = false;

    void Update()
    {
        if (target == null || hasReachedTarget) return;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > stopDistance)
        {
            // bergerak ke arah target
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            transform.LookAt(target); // opsional
        }
        else
        {
            hasReachedTarget = true;
            // Di sini bisa juga langsung jalankan animasi attack
        }
    }

    // 🔹 Deteksi trigger saat menabrak Base
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Base"))
        {
            hasReachedTarget = true;
            baseHealth = other.GetComponent<Health>(); // cari script Health di Base
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Base") && baseHealth != null)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackInterval)
            {
                baseHealth.TakeDamage(attackDamage);
                attackTimer = 0f;
            }
        }
    }
}
