using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform target;   // target tujuan (Cube 1)
    public float speed = 3f;   // kecepatan jalan musuh
    public float stopDistance = 0.5f; // jarak berhenti dari target

    private bool hasReachedTarget = false;

    void Update()
    {
        if (target == null || hasReachedTarget) return;

        // hitung jarak ke target
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > stopDistance)
        {
            // bergerak ke arah target
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            // opsional: rotasi menghadap target
            transform.LookAt(target);
        }
        else
        {
            // sudah sampai → berhenti
            hasReachedTarget = true;
        }
    }
}
