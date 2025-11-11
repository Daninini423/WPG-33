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
        // Jangan lanjut jalan kalau sedang menyerang
        if (target == null || isAttacking) return;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > stopDistance)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            direction.y = 0;
            transform.position += direction * speed * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(direction);
        }
        else
        {
            // kalau jarak cukup dekat, berhenti & mulai serang
            isAttacking = true;
            anim.SetTrigger("Attack");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Base"))
        {
            baseHealth = other.GetComponent<Health>();

            // 💥 langsung aktifkan mode menyerang
            isAttacking = true;
            anim.SetTrigger("Attack");
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
                anim.SetTrigger("Attack");
            }
        }
    }
}
