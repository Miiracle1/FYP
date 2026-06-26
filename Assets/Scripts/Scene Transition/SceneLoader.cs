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
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        { 
            Destroy(gameObject);
            return;
        }
    }

    /***************************************************************************************************************************************************************************************/
    //Methods

    public void LoadScene(string sceneName)
    { 
        if (!isLoading)
        {
            StartCoroutine(LoadSceneSequence(sceneName));
        }
    }

    private IEnumerator LoadSceneSequence(string targetScene)
    {
        isLoading = true;
        onLoadingProgress?.Invoke();

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(targetScene);

        while (!loadOperation.isDone)
        {
            yield return null;
        }

        onLoadingCompleted?.Invoke();
        isLoading = false;
    }
}
