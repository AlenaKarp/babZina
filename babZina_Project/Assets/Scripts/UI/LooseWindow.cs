//this empty line for UTF-8 BOM header
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LooseWindow : MonoBehaviour
{
    internal event Action OnCloseWindow = () => { };

    [SerializeField] private Button replayButton;
    [SerializeField] private Button backButton;
    [SerializeField] private GameObject loadingScreen;

    private void Awake()
    {
        replayButton.onClick.AddListener(OnReplay);
        backButton.onClick.AddListener(OnMainMenu);
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnReplay()
    {
        Time.timeScale = 1;

        Scene scene = SceneManager.GetActiveScene();

        loadingScreen.SetActive(true);

        StartCoroutine(LoadAsync(scene.name));
    }

    private void OnMainMenu()
    {
        Time.timeScale = 1;

        loadingScreen.SetActive(true);

        StartCoroutine(LoadAsync(SceneNames.mainMenu));
    }

    IEnumerator LoadAsync(string loadingSceneName)
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
