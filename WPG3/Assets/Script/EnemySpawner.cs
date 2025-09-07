using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Prefab Musuh")]
    public GameObject enemyPrefab;  // prefab musuh (capsule yang sudah dijadikan prefab)

    [Header("Spawn Points")]
    public Transform[] spawnPoints; // tempat spawn (isi dengan spawn point yang sudah kamu buat di scene)

    [Header("Pengaturan Spawn")]
    public float spawnInterval = 3f; // jeda waktu spawn musuh baru
    private float timer;

    void Update()
    {
        // hitung waktu
        timer += Time.deltaTime;

        // jika waktu sudah lebih dari spawnInterval, spawn musuh
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f; // reset timer
        }
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0) return; // kalau belum ada spawnpoint, keluar

        // pilih spawn point random
        int randomIndex = Random.Range(0, spawnPoints.Length);

        // buat musuh di posisi spawnpoint
        Instantiate(enemyPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
    }
}
