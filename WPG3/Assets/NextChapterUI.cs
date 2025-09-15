using UnityEngine;
using UnityEngine.SceneManagement;

public class NextChapterUI : MonoBehaviour
{
    public GameObject nextChapterPanel; // drag panel dari Inspector
    public string nextSceneName;        // nama scene berikutnya

    private void Start()
    {
        nextChapterPanel.SetActive(false);
    }

    private void Update()
    {
        // kalau semua musuh sudah spawn dan sudah mati
        if (EnemyManager.aliveEnemies <= 0 && FindObjectOfType<EnemySpawner>().spawnedCount >= FindObjectOfType<EnemySpawner>().maxSpawnCount)
        {
            nextChapterPanel.SetActive(true);
        }
    }

    public void NextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
