using UnityEngine;
public class EnemySpawner : MonoBehaviour
{
    [Header("Prefab Musuh")]
    public GameObject enemyPrefab;   // musuh tipe 0
    public GameObject enemyPrefab1;  // musuh tipe 1
    public GameObject enemyPrefab2;  // musuh tipe 2

    [Header("Prefab Boss")]
    public GameObject bossPrefab;    // boss (muncul setelah semua musuh habis)

    [Header("Spawn Points")]
    public Transform[] spawnPoints;  // index 0-3 (Spawnpoint 1-4)

    [Header("Pengaturan Spawn Enemy 0")]
    public float spawnInterval0 = 0f;
    public int maxSpawnCount0 = 0;
    private int spawnedCount0 = 0;
    private float timer0 = 0f;

    [Header("Pengaturan Spawn Enemy 1")]
    public float spawnInterval1 = 0f;
    public int maxSpawnCount1 = 0;
    private int spawnedCount1 = 0;
    private float timer1 = 0f;

    [Header("Pengaturan Spawn Enemy 2")]
    public float spawnInterval2 = 0f;
    public int maxSpawnCount2 = 0;
    private int spawnedCount2 = 0;
    private float timer2 = 0f;

    [Header("Pengaturan Boss")]
    public float bossSpawnDelay = 2f; // delay setelah semua musuh mati
    private bool bossSpawned = false;
    private bool bossWaiting = false;
    private float bossTimer = 0f;

    private void Start()
    {
        spawnedCount0 = 0;
        spawnedCount1 = 0;
        spawnedCount2 = 0;
        EnemyManager.ResetManager();
        timer0 = Random.Range(0.5f, spawnInterval0);
        timer1 = Random.Range(0.5f, spawnInterval1);
        timer2 = Random.Range(0.5f, spawnInterval2);
    }

    void Update()
    {
        // Enemy 0
        if (spawnedCount0 < maxSpawnCount0)
        {
            timer0 += Time.deltaTime;
            if (timer0 >= spawnInterval0)
            {
                SpawnEnemy(enemyPrefab);
                spawnedCount0++;
                timer0 = 0f;
            }
        }

        // Enemy 1
        if (spawnedCount1 < maxSpawnCount1)
        {
            timer1 += Time.deltaTime;
            if (timer1 >= spawnInterval1)
            {
                SpawnEnemy(enemyPrefab1);
                spawnedCount1++;
                timer1 = 0f;
            }
        }

        // Enemy 2
        if (spawnedCount2 < maxSpawnCount2)
        {
            timer2 += Time.deltaTime;
            if (timer2 >= spawnInterval2)
            {
                SpawnEnemy(enemyPrefab2);
                spawnedCount2++;
                timer2 = 0f;
            }
        }

        // Cek kondisi spawn boss
        // Semua musuh sudah di-spawn dan semua musuh sudah mati
        bool semuaMusuhSudahSpawn = GetTotalSpawnedCount() >= GetTotalMaxCount();
        bool semuaMusuhMati = EnemyManager.aliveEnemies <= 0;

        if (bossPrefab != null && !bossSpawned && semuaMusuhSudahSpawn && semuaMusuhMati)
        {
            if (!bossWaiting)
            {
                bossWaiting = true;
                bossTimer = 0f;
                Debug.Log("Semua musuh mati! Boss akan muncul dalam " + bossSpawnDelay + " detik...");
            }

            bossTimer += Time.deltaTime;
            if (bossTimer >= bossSpawnDelay)
            {
                SpawnBoss();
                bossSpawned = true;
            }
        }
    }

    void SpawnEnemy(GameObject prefabToSpawn)
    {
        if (spawnPoints.Length == 0) return;
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(prefabToSpawn, spawnPoints[randomIndex].position, Quaternion.identity);
        EnemyManager.aliveEnemies++;
    }

    void SpawnBoss()
    {
        // Boss spawn di spawnPoints index 1 dan 2 (Spawnpoint 2 dan 3)
        // Pilih random antara index 1 atau 2
        if (spawnPoints.Length < 3)
        {
            Debug.LogWarning("Spawnpoint kurang dari 3! Boss tidak bisa spawn di index 1/2.");
            return;
        }

        int bossIndex = Random.Range(1, 3); // random antara index 1 atau 2
        Instantiate(bossPrefab, spawnPoints[bossIndex].position, Quaternion.identity);
        EnemyManager.aliveEnemies++;
        Debug.Log("Boss muncul di Spawnpoint " + (bossIndex + 1));
    }

    public int GetTotalSpawnedCount()
    {
        return spawnedCount0 + spawnedCount1 + spawnedCount2;
    }

    public int GetTotalMaxCount()
    {
        return maxSpawnCount0 + maxSpawnCount1 + maxSpawnCount2;
    }

    // Cek apakah boss sudah mati (untuk NextChapterUI)
    public bool IsBossDead()
    {
        return bossSpawned && EnemyManager.aliveEnemies <= 0;
    }

    public bool HasBoss()
    {
        return bossPrefab != null;
    }
}