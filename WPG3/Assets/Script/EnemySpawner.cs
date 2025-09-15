using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Prefab Musuh")]
    public GameObject enemyPrefab;  // prefab musuh (capsule yang sudah dijadikan prefab)

    [Header("Spawn Points")]
    public Transform[] spawnPoints; // tempat spawn (isi dengan spawn point yang sudah kamu buat di scene)

    [Header("Pengaturan Spawn")]
    public float spawnInterval = 3f;   // jeda waktu spawn musuh baru
    public int maxSpawnCount = 10;     // batas maksimal musuh yang akan di-spawn
    public int spawnedCount = 0;      // jumlah musuh yang sudah di-spawn

    private float timer;

    void Update()
    {
        if (spawnedCount >= maxSpawnCount) return; // kalau sudah mencapai batas, berhenti spawn

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
        if (spawnPoints.Length == 0) return;

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(enemyPrefab, spawnPoints[randomIndex].position, Quaternion.identity);

        spawnedCount++;
        EnemyManager.aliveEnemies++; // tambah jumlah musuh hidup
    }

}
