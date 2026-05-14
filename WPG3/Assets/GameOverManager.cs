using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject gameOverPanel;

    [Header("Audio")]
    public AudioSource backgroundMusic;
    public AudioSource clickSound;

    [Header("Scene")]
    [SerializeField] private SceneAsset quitScene;

    private string quitSceneName;

    private bool isGameOver = false;

    private void Start()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

#if UNITY_EDITOR
        if (quitScene != null)
        {
            quitSceneName = quitScene.name;
        }
#endif
    }

    // =========================
    // SHOW GAME OVER PANEL
    // =========================
    public void ShowGameOver()
    {
        // Hindari dipanggil berkali-kali
        if (isGameOver) return;

        isGameOver = true;

        // Stop BGM
        if (backgroundMusic != null)
        {
            backgroundMusic.Stop();
        }

        // Tampilkan panel
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // Pause game
        Time.timeScale = 0f;

        // Tampilkan cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // =========================
    // BUTTON SOUND
    // =========================
    IEnumerator PlayButtonSound()
    {
        if (clickSound != null)
        {
            clickSound.Play();

            yield return new WaitForSecondsRealtime(clickSound.clip.length);
        }
    }

    // =========================
    // RESTART SCENE
    // =========================
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

    // =========================
    // QUIT GAME
    // =========================
    public void QuitGame()
    {
        StartCoroutine(QuitRoutine());
    }

    IEnumerator QuitRoutine()
    {
        yield return StartCoroutine(PlayButtonSound());

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (!string.IsNullOrEmpty(quitSceneName))
        {
            SceneManager.LoadScene(quitSceneName);
        }
        else
        {
            Debug.LogError("Quit Scene belum diset!");
        }
    }
}