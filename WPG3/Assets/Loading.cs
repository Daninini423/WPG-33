using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Loading : MonoBehaviour
{
    [Header("Loading Settings")]
    [SerializeField] private int timeToWait = 5;

#if UNITY_EDITOR
    [Header("Target Scene")]
    [SerializeField] private SceneAsset targetScene;
#endif

    // 1. Tambahkan SerializeField agar nama string ini ikut tersimpan ke dalam Build WebGL
    [SerializeField, HideInInspector] private string targetSceneName;

    // 2. Gunakan OnValidate untuk mengambil nama scene secara otomatis saat kamu memasukkan scene di Inspector (sebelum di-build)
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (targetScene != null)
        {
            targetSceneName = targetScene.name;
        }
        else
        {
            targetSceneName = "";
        }
    }
#endif

    private void Start()
    {
        // 3. Logika pengambilan nama di Start dihapus karena sudah diurus oleh OnValidate
        StartCoroutine(WaitForTime());
    }

    IEnumerator WaitForTime()
    {
        yield return new WaitForSecondsRealtime(timeToWait);

        LoadNextScene();
    }

    void LoadNextScene()
    {
        Time.timeScale = 1f;

        if (!string.IsNullOrEmpty(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            Debug.LogError("Target Scene belum diset! Pastikan Scene sudah dimasukkan di Inspector.");
        }
    }
}