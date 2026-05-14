using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class NextChapterUI : MonoBehaviour
{
    [Header("UI")]
    public GameObject nextChapterPanel;

    [Header("Audio")]
    public AudioSource backgroundMusic;
    public AudioSource clickSound;

#if UNITY_EDITOR
    [Header("Scene")]
    [SerializeField] private SceneAsset nextScene;
#endif

    private string nextSceneName;
    private bool canGoNext = false;

    private void Start()
    {
        nextChapterPanel.SetActive(false);

#if UNITY_EDITOR
        if (nextScene != null)
        {
            nextSceneName = nextScene.name;
        }
#endif
    }

    private void Update()
    {
        EnemySpawner spawner = FindObjectOfType<EnemySpawner>();

        if (spawner == null) return;

        if (EnemyManager.aliveEnemies <= 0 &&
            spawner.GetTotalSpawnedCount() >= spawner.GetTotalMaxCount())
        {
            // kalau ada boss, tunggu boss mati
            if (spawner.HasBoss() && !spawner.IsBossDead())
                return;

            if (!canGoNext)
            {
                canGoNext = true;

                // pause game
                Time.timeScale = 0f;

                // stop bgm
                if (backgroundMusic != null)
                {
                    backgroundMusic.Stop();
                }

                // show panel
                nextChapterPanel.SetActive(true);

                // show cursor
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    // =====================================
    // BUTTON SOUND
    // =====================================

    IEnumerator PlayButtonSound()
    {
        if (clickSound != null)
        {
            clickSound.Play();

            yield return new WaitForSecondsRealtime(clickSound.clip.length);
        }
    }

    // =====================================
    // NEXT SCENE
    // =====================================

    public void NextScene()
    {
        StartCoroutine(NextSceneRoutine());
    }

    IEnumerator NextSceneRoutine()
    {
        yield return StartCoroutine(PlayButtonSound());

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

    // =====================================
    // RESTART SCENE
    // =====================================

    public void TryAgain()
    {
        StartCoroutine(TryAgainRoutine());
    }

    IEnumerator TryAgainRoutine()
    {
        yield return StartCoroutine(PlayButtonSound());

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        EnemyManager.ResetManager();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}