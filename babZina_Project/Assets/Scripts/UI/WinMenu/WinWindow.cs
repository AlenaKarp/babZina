//this empty line for UTF-8 BOM header
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinWindow : MonoBehaviour
{
    internal event Action OnCloseWindow = () => { };

    [SerializeField] private string nextLevelSceneName;
    [SerializeField] private List<Star> stars;
    [SerializeField] private List<Star> tricks;
    [SerializeField] private Button nextLevel;
    [SerializeField] private Button backButton;
    [SerializeField] private GameObject loadingScreen;

    private void Awake()
    {
        nextLevel.onClick.AddListener(OnNextLevel);
        backButton.onClick.AddListener(OnMainMenu);
    }

    internal void ShowWindow(int starProgress, int tricksCount)
    {
        Time.timeScale = 0;

        SetProgress(stars, starProgress);

        SetProgress(tricks, tricksCount);
    }

    private void SetProgress(List<Star> list, int progress)
    {
        for (int i = 0; i <= list.Count - 1; i++)
        {
            list[i].SetVisible(i <= progress - 1);
        }
    }

    private void OnNextLevel()
    {
        Time.timeScale = 1;

        loadingScreen.SetActive(true);

        StartCoroutine(LoadAsync(nextLevelSceneName));
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
