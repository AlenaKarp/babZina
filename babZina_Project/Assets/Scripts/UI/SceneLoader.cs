//this empty line for UTF-8 BOM header
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private string loadingSceneName;

    internal void SetNewSceneName(string sceneName)
    {
        loadingSceneName = sceneName;
    }

    public void Loading()
    {
        loadingScreen.SetActive(true);

        StartCoroutine(LoadAsync());
    }

    IEnumerator LoadAsync()
    {
        AsyncOperation loadAsync = SceneManager.LoadSceneAsync(loadingSceneName);
        loadAsync.allowSceneActivation = false;

        while (!loadAsync.isDone)
        {
            if (loadAsync.progress >= .9f && !loadAsync.allowSceneActivation)
            {
                yield return new WaitForSeconds(2.2f);
                loadAsync.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
