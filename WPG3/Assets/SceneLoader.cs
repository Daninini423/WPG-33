using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Fungsi umum untuk berpindah scene
    public void LoadScene(string sceneName)
    {
        // Pastikan time scale normal saat pindah
        Time.timeScale = 1f;

        // Muat scene tujuan
        SceneManager.LoadScene(sceneName);
    }

    // Fungsi untuk keluar dari game (kalau kamu mau pakai di tombol "Exit")
    public void QuitGame()
    {
        Debug.Log("Keluar dari game...");
        Application.Quit();
    }
}
