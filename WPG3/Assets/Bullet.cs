using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float timeToDestroy = 3f; // default 3 detik
    float timer;

    void Update()
    {
        // waktu jalan terus
        timer += Time.deltaTime;
        if (timer >= timeToDestroy)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // kalau kena enemy → hancurkan enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject); // hancurkan enemy
        }

        // hancurkan peluru
        Destroy(gameObject);
    }
}
