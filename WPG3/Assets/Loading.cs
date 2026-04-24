using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    [SerializeField] int timeToWait = 5;

    void Start()
    {
        StartCoroutine(WaitForTime());
    }

    IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(timeToWait);
        LoadNextScene();
    }

    void LoadNextScene()
    {
        Time.timeScale = 1f;
        int targetScene = PlayerPrefs.GetInt("TargetScene", 2);
        SceneManager.LoadScene(targetScene);
    }

    // Dipanggil dari scene lain sebelum masuk Loading
    public static void LoadViaLoading(int targetSceneIndex)
    {
        PlayerPrefs.SetInt("TargetScene", targetSceneIndex);
        SceneManager.LoadScene(1); // index Loading Screen
    }
}