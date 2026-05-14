using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource clickSound;

    private void Start()
    {
        // Reset semua kondisi gameplay
        Time.timeScale = 1f;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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
    // PLAY GAME
    // =========================
    public void PlayGame()
    {
        StartCoroutine(PlayGameRoutine());
    }

    IEnumerator PlayGameRoutine()
    {
        yield return StartCoroutine(PlayButtonSound());

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        SceneManager.LoadSceneAsync(1);
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

        Debug.Log("Game keluar");

        Application.Quit();
    }
}