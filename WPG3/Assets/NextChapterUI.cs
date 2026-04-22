using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class NextChapterUI : MonoBehaviour
{
    public GameObject nextChapterPanel;

#if UNITY_EDITOR
    [SerializeField] private SceneAsset nextScene;
#endif

    private string nextSceneName;
    private bool canGoNext = false;

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
        EnemySpawner spawner = FindObjectOfType<EnemySpawner>();

        if (EnemyManager.aliveEnemies <= 0 &&
            spawner.GetTotalSpawnedCount() >= spawner.GetTotalMaxCount())
        {
            nextChapterPanel.SetActive(true);
            canGoNext = true;
        }

        if (canGoNext && Input.GetKeyDown(KeyCode.Space))
        {
            NextScene();
        }
    }

    public void NextScene()
    {
        // 🔥 RESET sebelum pindah scene (INI PENTING BANGET)
        Time.timeScale = 1f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

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