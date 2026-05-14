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

    private string targetSceneName;

    private void Start()
    {
#if UNITY_EDITOR
        if (targetScene != null)
        {
            targetSceneName = targetScene.name;
        }
#endif

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
            Debug.LogError("Target Scene belum diset!");
        }
    }
}