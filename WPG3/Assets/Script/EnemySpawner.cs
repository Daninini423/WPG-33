using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Prefab Musuh")]
    public GameObject enemyPrefab;   // musuh tipe 0
    public GameObject enemyPrefab1;  // musuh tipe 1
    public GameObject enemyPrefab2;  // musuh tipe 2

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
    private void Start()
    {
        // kasih random offset supaya musuh tidak spawn barengan
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
    }

    void SpawnEnemy(GameObject prefabToSpawn)
    {
        if (spawnPoints.Length == 0) return;

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(prefabToSpawn, spawnPoints[randomIndex].position, Quaternion.identity);

        EnemyManager.aliveEnemies++; 
    }

    // fungsi tambahan untuk NextChapterUI cek total musuh
    public int GetTotalSpawnedCount()
    {
        return spawnedCount0 + spawnedCount1 + spawnedCount2;
    }

    public int GetTotalMaxCount()
    {
        return maxSpawnCount0 + maxSpawnCount1 + maxSpawnCount2;
    }
}
