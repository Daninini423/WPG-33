using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class LoadSceneButton : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] private SceneAsset sceneToLoad;
#endif

    // 1. Tambahkan SerializeField agar ikut ter-build dan tersimpan di WebGL
    [SerializeField, HideInInspector] private string sceneName;

    // 2. Pindahkan logika pengambilan nama ke OnValidate
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (sceneToLoad != null)
        {
            sceneName = sceneToLoad.name;
        }
        else
        {
            sceneName = "";
        }
    }
#endif

    private void Start()
    {
        // Start dibiarkan kosong karena OnValidate sudah mengurus pengambilan nama
    }

    public void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            // Pastikan Time.timeScale dikembalikan ke 1 jika sebelumnya di-pause
            Time.timeScale = 1f;
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene belum di-set! Pastikan kamu sudah drag scene ke Inspector.");
        }
    }
}