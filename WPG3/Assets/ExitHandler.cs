using System.Collections;
using UnityEngine;

public class ExitHandler : MonoBehaviour
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
    // EXIT GAME
    // =========================
    public void ExitGame()
    {
        StartCoroutine(ExitRoutine());
    }

    IEnumerator ExitRoutine()
    {
        yield return StartCoroutine(PlayButtonSound());

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}