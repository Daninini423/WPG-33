using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditScene : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource clickSound;

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
    // LOAD SCENE
    // =========================
    public void PlayGame()
    {
        StartCoroutine(PlayGameRoutine());
    }

    IEnumerator PlayGameRoutine()
    {
        yield return StartCoroutine(PlayButtonSound());

        SceneManager.LoadSceneAsync(6);
    }
}