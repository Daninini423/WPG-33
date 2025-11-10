using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    public float speed = 1.5f;
    public float stopDistance = 0.5f;
    private Health baseHealth;
    private float attackTimer = 0f;
    public int attackDamage = 10;
    public float attackInterval = 1.5f;
    private bool isAttacking = false;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (target == null || isAttacking) return;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > stopDistance)
        {
            // 🔹 Jalan ke target
            Vector3 direction = (target.position - transform.position).normalized;
            direction.y = 0;
            transform.position += direction * speed * Time.deltaTime;

            // 🔹 Hadap ke arah target
            transform.rotation = Quaternion.LookRotation(direction);
        }
        else
        {
            // 🔹 Sudah sampai target → mulai serang
            isAttacking = true;
            anim.SetTrigger("Attack");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Base"))
        {
            baseHealth = other.GetComponent<Health>();
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

                // 🔹 Mainkan animasi serang setiap kali menyerang
                anim.SetTrigger("Attack");
            }
        }
    }
}
