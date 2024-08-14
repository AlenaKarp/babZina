//this empty line for UTF-8 BOM header
using System;
using UnityEngine;
using UnityTools.UnityRuntime.Timers;

public class UIManager : MonoBehaviour, IUIManager
{
    public event Action OnWinWindowOpen = () => { };
    public event Action OnStackChanged = () => { };

    internal event Action OnInited = () => { };

    internal IAngryScaleManager AngryScaleManagerProvider => angryScaleManager;
    internal ISaveManager SaveManagerProvider => saveManager;

    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private LooseWindow looseWindow;
    [SerializeField] private WinWindow winWindow;
    [SerializeField] private float secondsDelayAfterFinishLevel = 1.5f;

    private IAngryScaleManager angryScaleManager;
    private IScenario scenario;
    private ISaveManager saveManager;

    private void Awake()
    {
        pauseMenu.OnCloseWindow += OnStackChangedAction;
    }

    internal void Init(IAngryScaleManager angryScaleManager, IScenario scenario, ISaveManager saveManager)
    {
        if (this.scenario != null)
        {
            this.scenario.OnTutorialWin -= OnTutorialWin;
            this.scenario.OnWin -= OnWin;
            this.scenario.OnLoose -= OnLoose;
        }

        this.angryScaleManager = angryScaleManager;
        this.saveManager = saveManager;
        this.scenario = scenario;

        scenario.OnTutorialWin += OnTutorialWin;
        scenario.OnWin += OnWin;
        scenario.OnLoose += OnLoose;

        OnInited();
    }

    private void OnLoose()
    {
        Timer.Instance.WaitUnscaled(secondsDelayAfterFinishLevel).Done(() =>
        {
            looseWindow.gameObject.SetActive(true);

            OnStackChanged();
        });
    }

    private void OnWin(int stars, int tricks)
    {
        Timer.Instance.WaitUnscaled(secondsDelayAfterFinishLevel).Done(() =>
        {
            PlayWin(stars, tricks);
        });
    }

    private void OnTutorialWin()
    {
        PlayWin(3, 3);
    }

    private void PlayWin(int stars, int tricks)
    {
        OnWinWindowOpen();

        winWindow.ShowWindow(stars, tricks);

        winWindow.gameObject.SetActive(true);

        OnStackChanged();
    }

    private void OnStackChangedAction() => OnStackChanged();

    internal void ShowPauseMenu()
    {
        pauseMenu.gameObject.SetActive(true);

        OnStackChanged();
    }
}
