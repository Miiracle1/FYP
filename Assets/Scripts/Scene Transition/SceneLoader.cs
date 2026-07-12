using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    private bool isLoading = false;

    public event Action onLoadingProgress; // event to raise gamemode loading
    public event Action onLoadingCompleted; // event to raise finish loading

    /***************************************************************************************************************************************************************************************/
    //Unity Methods

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    /***************************************************************************************************************************************************************************************/
    //Methods

    public void LoadScene(string sceneName)
    {
        Debug.Log("Scene loader is loading = " + isLoading);
        if (!isLoading)
        {
            StartCoroutine(LoadSceneSequence(sceneName));
            Debug.Log("Startting to load scene");
        }
    }

    private IEnumerator LoadSceneSequence(string targetScene)
    {
        isLoading = true;
        onLoadingProgress?.Invoke();

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(targetScene);

        Debug.Log("RN loading scene is " + targetScene);

        if (loadOperation == null)
        {
            Debug.LogError($"Failed to load scene: {targetScene}");
            isLoading = false;
            yield break;
        }

        if (loadOperation.isDone)
        {
            isLoading = false;
            Debug.Log("Loading Completed");
            yield break;
        }

        yield return loadOperation;

        if (loadOperation.isDone)
        {
            isLoading = false;
            Debug.Log("Loading Completed");
            yield break;
        }

        Debug.Log("Laoding should be done");

        //onLoadingCompleted?.Invoke();
        isLoading = false;
    }

    public void ForceReset()
    {
        isLoading = false;
    }
}
