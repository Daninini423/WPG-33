using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("Prefab Musuh")]
    public GameObject enemyPrefab;
    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;

    [Header("Prefab Boss")]
    public GameObject bossPrefab;

    [Header("Spawn Points")]
    public Transform[] spawnPoints;

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
    public float bossSpawnDelay = 4f;
    private bool bossSpawned = false;
    private bool bossWaiting = false;

    [Header("UI Boss Warning")]
    public GameObject bossWarningUI;
    public GameObject redFlashPanel;
    public float flashSpeed = 0.3f;

    [Header("Audio Pengaturan")]
    public AudioSource audioSource;      // Slot untuk komponen AudioSource
    public AudioClip bossWarningSFX;    // Slot untuk file suara (Alarm/Sirine)

    private void Start()
    {
        spawnedCount0 = 0;
        spawnedCount1 = 0;
        spawnedCount2 = 0;
        EnemyManager.ResetManager();
        timer0 = Random.Range(0.5f, spawnInterval0);
        timer1 = Random.Range(0.5f, spawnInterval1);
        timer2 = Random.Range(0.5f, spawnInterval2);

        if (bossWarningUI != null) bossWarningUI.SetActive(false);
        if (redFlashPanel != null) redFlashPanel.SetActive(false);

        // Memastikan audio source tidak otomatis bunyi di awal game
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
        }
    }

    void Update()
    {
        // Spawner Enemy 0
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

        // Spawner Enemy 1
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

        // Spawner Enemy 2
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
        bool semuaMusuhSudahSpawn = GetTotalSpawnedCount() >= GetTotalMaxCount();
        bool semuaMusuhMati = EnemyManager.aliveEnemies <= 0;

        if (bossPrefab != null && !bossSpawned && semuaMusuhSudahSpawn && semuaMusuhMati)
        {
            if (!bossWaiting)
            {
                bossWaiting = true;
                StartCoroutine(BossAppearanceSequence());
            }
        }
    }

    IEnumerator BossAppearanceSequence()
    {
        Debug.Log("Semua musuh mati! Efek Warning & Audio dimulai...");

        // 1. Jalankan Sound Effect (Alarm/Sirine)
        if (audioSource != null && bossWarningSFX != null)
        {
            audioSource.clip = bossWarningSFX;
            audioSource.loop = true; // Set true agar suara mengulang selama UI berkedip
            audioSource.Play();
        }

        // 2. Nyalakan Teks Warning
        if (bossWarningUI != null) bossWarningUI.SetActive(true);

        // 3. Efek Kedip Layar Merah
        float elapsed = 0f;
        while (elapsed < bossSpawnDelay)
        {
            if (redFlashPanel != null)
            {
                redFlashPanel.SetActive(!redFlashPanel.activeSelf);
            }
            yield return new WaitForSeconds(flashSpeed);
            elapsed += flashSpeed;
        }

        // 4. Matikan UI Warning & Stop Audio tepat saat Boss muncul
        if (bossWarningUI != null) bossWarningUI.SetActive(false);
        if (redFlashPanel != null) redFlashPanel.SetActive(false);

        if (audioSource != null)
        {
            audioSource.Stop(); // Hentikan suara alarm
        }

        // 5. Spawn Boss
        SpawnBoss();
        bossSpawned = true;
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
        if (spawnPoints.Length < 3)
        {
            Debug.LogWarning("Spawnpoint kurang dari 3! Boss tidak bisa spawn di index 1/2.");
            return;
        }

        int bossIndex = Random.Range(1, 3);
        Instantiate(bossPrefab, spawnPoints[bossIndex].position, Quaternion.identity);
        EnemyManager.aliveEnemies++;
        Debug.Log("Boss muncul di Spawnpoint " + (bossIndex + 1));
    }

    public int GetTotalSpawnedCount() { return spawnedCount0 + spawnedCount1 + spawnedCount2; }
    public int GetTotalMaxCount() { return maxSpawnCount0 + maxSpawnCount1 + maxSpawnCount2; }
    public bool IsBossDead() { return bossSpawned && EnemyManager.aliveEnemies <= 0; }
    public bool HasBoss() { return bossPrefab != null; }
}