using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// 1. UnityEditor dibungkus agar HANYA dibaca saat berada di dalam software Unity
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameOverManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject gameOverPanel;

    [Header("Audio")]
    public AudioSource backgroundMusic;
    public AudioSource clickSound;

    [Header("Scene")]
    // 2. SceneAsset juga dibungkus karena ini bagian dari UnityEditor
#if UNITY_EDITOR
    [SerializeField] private SceneAsset quitScene;
#endif

    // 3. Tambahkan SerializeField agar nama string ini tersimpan saat di-build
    [SerializeField, HideInInspector] private string quitSceneName;

    private bool isGameOver = false;

    // 4. Pindahkan logika pengambilan nama ke OnValidate. 
    // Fungsi ini otomatis berjalan di Editor setiap kali kamu memasukkan scene ke slot quitScene.
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (quitScene != null)
        {
            quitSceneName = quitScene.name;
        }
        else
        {
            quitSceneName = ""; // Kosongkan jika tidak ada scene yang dimasukkan
        }
    }
#endif

    private void Start()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        // Logika di Start() sebelumnya dihapus karena sudah ditangani oleh OnValidate()
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