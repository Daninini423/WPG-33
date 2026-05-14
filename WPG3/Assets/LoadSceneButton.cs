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

    private string sceneName;

    private void Start()
    {
#if UNITY_EDITOR
        if (sceneToLoad != null)
        {
            sceneName = sceneToLoad.name;
        }
#endif
    }

    public void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene belum di-set!");
        }
    }
}