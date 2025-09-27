using UnityEngine;
using TMPro; // TextMeshPro

public class EnemyCountUI : MonoBehaviour
{
    public enum DisplayMode { Remaining, AliveAndMax }

    [Header("References (drag di Inspector)")]
    [SerializeField] private TextMeshProUGUI enemyText; // drag TextMeshPro UI
    [SerializeField] private EnemySpawner spawner;      // drag object yang punya EnemySpawner

    [Header("Pilihan tampilan")]
    public DisplayMode mode = DisplayMode.Remaining;

    // caching agar tidak update string terus2an
    private int lastIntValue = int.MinValue;
    private string lastStringValue = "";

    private void Start()
    {
        // fallback: kalau lupa drag, coba ambil otomatis
        if (enemyText == null)
        {
            enemyText = GetComponentInChildren<TextMeshProUGUI>();
            if (enemyText == null)
                Debug.LogWarning("EnemyCountUI: enemyText belum diset dan tidak ditemukan di children.");
        }

        if (spawner == null)
        {
            spawner = FindObjectOfType<EnemySpawner>();
            if (spawner == null)
                Debug.LogWarning("EnemyCountUI: spawner belum diset dan tidak ditemukan di scene.");
        }

        // set awal
        ForceUpdateText();
    }

    private void Update()
    {
        if (enemyText == null) return;

        int maxTotal = spawner != null ? spawner.GetTotalMaxCount() : 0;
        int spawned = spawner != null ? spawner.GetTotalSpawnedCount() : 0;
        int alive = EnemyManager.aliveEnemies;

        if (mode == DisplayMode.Remaining)
        {
            // remaining = unspawned + currently alive
            int remaining = maxTotal - spawned + alive;
            if (remaining != lastIntValue)
            {
                lastIntValue = remaining;
                enemyText.text = "Enemies: " + remaining;
            }
        }
        else // AliveAndMax
        {
            string s = $"Enemies: {alive} / {maxTotal}";
            if (s != lastStringValue)
            {
                lastStringValue = s;
                enemyText.text = s;
            }
        }
    }

    // pakai jika mau refresh manual
    public void ForceUpdateText()
    {
        lastIntValue = int.MinValue;
        lastStringValue = "";
        Update();
    }
}
