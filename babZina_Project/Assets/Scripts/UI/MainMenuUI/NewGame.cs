//this empty line for UTF-8 BOM header
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGame : MonoBehaviour
{
    internal event Action OnNewGameMenu = () => { };
    internal SaveManager SaveManagerProvider => saveManager;

    [SerializeField] private SaveManager saveManager;
    [SerializeField] private Button cover;
    [SerializeField] private Button zoom;
    [SerializeField] private List<GameObject> menuVariants;

    private void Awake()
    {
        zoom.onClick.AddListener(SetNewGameMenu);
        cover.onClick.AddListener(SetMainMenu);

        SetZoomActive(true);
    }

    private void OnEnable()
    {
        int? maxLevelWithProgress = saveManager.GetLastLevelWithProgress();

        int lastOpenedLevel = maxLevelWithProgress.HasValue
            ? maxLevelWithProgress.Value
            : 0;

        for (int i = 0; i < menuVariants.Count; i++)
        {
            menuVariants[i].SetActive(i == lastOpenedLevel);
        }
    }

    private void SetNewGameMenu()
    {
        SetZoomActive(false);

        OnNewGameMenu();
    }

    private void SetMainMenu()
    {
        SetZoomActive(true);

        OnNewGameMenu();
    }

    private void SetZoomActive(bool isZoomActive)
    {
        zoom.gameObject.SetActive(isZoomActive);
        cover.gameObject.SetActive(isZoomActive == false);
    }
}
