using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float timeToDestroy = 2f;
    [SerializeField] int damage = 10; // damage bullet

    [Header("VFX Settings")]
    public GameObject hitPrefab; // Slot baru untuk VFX Cipratan Air

    float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToDestroy)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ====================================================================
        // FITUR PELACAK: Memunculkan nama benda yang ditabrak di panel Console
        // ====================================================================
        Debug.Log("<color=yellow>MENGHINDAR!</color> Peluru menabrak: <b>" + collision.gameObject.name + "</b>");

        // Cek kalau kena musuh
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Health enemyHealth = collision.gameObject.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }

        // --- BAGIAN BARU: Memunculkan VFX Cipratan Air ---
        if (hitPrefab != null)
        {
            // 1. Ambil titik akurat di mana peluru menyentuh tembok/musuh
            ContactPoint contact = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;

            // 2. Lahirkan efek cipratan air di titik tersebut
            var hitVFX = Instantiate(hitPrefab, pos, rot);
            var psHit = hitVFX.GetComponent<ParticleSystem>();

            // 3. Hancurkan efeknya secara otomatis setelah animasinya selesai
            if (psHit != null)
            {
                Destroy(hitVFX, psHit.main.duration);
            }
            else
            {
                var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitVFX, psChild.main.duration);
            }
        }
        // --------------------------------------------------

        // Hancurkan bullet setelah tabrakan (kena apa pun)
        Destroy(gameObject);
    }
}