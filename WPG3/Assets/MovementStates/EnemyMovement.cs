using UnityEngine;


public class EnemyMovement : MonoBehaviour
{
    public Transform target;

    [Header("Movement")]
    public float speed = 1.5f;
    public float stopDistance = 0.5f;

    [Header("Attack")]
    public int attackDamage = 10;
    public float attackInterval = 1.5f;

    [Header("Sound Settings")]
    public float maxSoundDistance = 12f;
    public float footstepInterval = 0.5f;

    private Health baseHealth;
    private float attackTimer;
    private float footstepTimer;

    private bool isAttacking = false;

    private Transform player;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        // Kalau tidak ada target atau sedang attack
        if (target == null || isAttacking) return;

        float distance = Vector3.Distance(transform.position, target.position);

        // Bergerak ke target
        if (distance > stopDistance)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            direction.y = 0;

            transform.position += direction * speed * Time.deltaTime;

            transform.rotation = Quaternion.LookRotation(direction);

            HandleFootsteps();
        }
        else
        {
            // Mulai attack
            isAttacking = true;

            anim.SetTrigger("Attack");


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Base"))
        {
            baseHealth = other.GetComponent<Health>();

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

                PlaySoundIfNear("Crounch");
            }
        }
    }

    // =========================
    // FOOTSTEP
    // =========================
    void HandleFootsteps()
    {
        footstepTimer += Time.deltaTime;

        if (footstepTimer >= footstepInterval)
        {
            PlaySoundIfNear("Ulat");

            footstepTimer = 0f;
        }
    }

    // =========================
    // PLAY SOUND IF PLAYER NEAR
    // =========================
    void PlaySoundIfNear(string soundName)
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);

        // Kalau player terlalu jauh
        if (dist > maxSoundDistance) return;

        SoundEffectManager.Play(soundName);
    }
}