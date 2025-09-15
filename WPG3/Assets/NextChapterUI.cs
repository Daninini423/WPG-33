using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class NextChapterUI : MonoBehaviour
{
    public GameObject nextChapterPanel;

#if UNITY_EDITOR
    [SerializeField] private SceneAsset nextScene; // drag scene di Inspector
#endif

    private string nextSceneName;
    private bool canGoNext = false; // supaya tombol hanya aktif setelah UI muncul

    private void Start()
    {
        nextChapterPanel.SetActive(false);

#if UNITY_EDITOR
        if (nextScene != null)
            nextSceneName = nextScene.name;
#endif
    }

    private void Update()
    {
        // kalau semua musuh sudah spawn dan mati ? tampilkan panel
        if (EnemyManager.aliveEnemies <= 0 &&
            FindObjectOfType<EnemySpawner>().spawnedCount >= FindObjectOfType<EnemySpawner>().maxSpawnCount)
        {
            nextChapterPanel.SetActive(true);
            canGoNext = true;
        }

        // cek input keyboard (misalnya tekan Space untuk lanjut)
        if (canGoNext && Input.GetKeyDown(KeyCode.Space))
        {
            NextScene();
        }
    }

    public void NextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("Next scene belum diset!");
        }
    }
}
