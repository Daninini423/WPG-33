using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    bool isGameOver = false;
    void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }
    public void ShowGameOver()
    {
        isGameOver = true;
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
        Time.timeScale = 0f;

        // Tampilkan cursor saat game over
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.Space))
        {
            TryAgain();
        }
    }
    void TryAgain()
    {
        Time.timeScale = 1f;

        // Sembunyikan cursor saat restart
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        EnemyManager.ResetManager();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}