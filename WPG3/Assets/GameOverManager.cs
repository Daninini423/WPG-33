using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    bool isGameOver = false;

    void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false); // awalnya mati
    }

    public void ShowGameOver()
    {
        isGameOver = true;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
        // Hentikan waktu (opsional)
        Time.timeScale = 0f;

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
        EnemyManager.ResetManager(); // reset counter musuh
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
