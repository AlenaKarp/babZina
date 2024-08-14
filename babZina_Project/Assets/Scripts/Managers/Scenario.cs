//this empty line for UTF-8 BOM header
using System;
using UnityEngine;

public class Scenario : MonoBehaviour, IScenario
{
    public event Action<int, int> OnWin = (stars, successTricksCount) => { };
    public event Action OnTutorialWin = () => { };
    public event Action OnLoose = () => { };

    [SerializeField] private LevelSettings levelSettings;

    private IAngryScaleManager angryScaleManager;
    private ISaveManager saveManager;
    private int successTricksCounter = 0;
    private int failTricksCounter = 0;

    private void OnEnable()
    {
        successTricksCounter = 0;
        failTricksCounter = 0;
    }

    internal void Init(IAngryScaleManager angryScaleManager, ISaveManager saveManager)
    {
        if (this.angryScaleManager != null)
        {
            Unsubscribe();
        }

        this.angryScaleManager = angryScaleManager;
        this.saveManager = saveManager;

        Subscribe();
    }

    internal void PlayWinTutorialLevel()
    {
        saveManager.SaveProgress(0, 3);
        OnTutorialWin();
    }

    private void Subscribe()
    {
        angryScaleManager.Progress.OnValueChanged += OnProgressValueChanged;
        angryScaleManager.OnSuccessTrick += OnSuccessTrick;
        angryScaleManager.OnFailTrick += OnFailTrick;
    }

    private void Unsubscribe()
    {
        angryScaleManager.Progress.OnValueChanged -= OnProgressValueChanged;
        angryScaleManager.OnSuccessTrick -= OnSuccessTrick;
        angryScaleManager.OnFailTrick -= OnFailTrick;
    }

    private void OnFailTrick()
    {
        failTricksCounter++;
    }

    private void OnSuccessTrick()
    {
        successTricksCounter++;

        if (successTricksCounter >= levelSettings.maxTricksCount)
        {
            Win(GetStarCount());
        }
    }

    private void OnProgressValueChanged(int progress)
    {
        if (progress <= 0)
        {
            int levelStarCount = GetStarCount();

            if (levelStarCount == 0)
            {
                Loose();
                return;
            }

            Win(levelStarCount);
        }
    }

    private void Win(int starCount)
    {
        saveManager.SaveProgress(levelSettings.levelIndex, starCount);

        OnWin(starCount, successTricksCounter);
    }

    private void Loose()
    {
        OnLoose();
    }

    private int GetStarCount()
    {
        int starCount = 0;

        foreach (LevelSettings.LevelResult levelResult in levelSettings.levelResults)
        {
            if (successTricksCounter >= levelResult.successTricks
                && failTricksCounter <= levelResult.failedTricks)
            {
                if (levelResult.starCount > starCount)
                {
                    starCount = levelResult.starCount;
                }
            }
        }

        return starCount;
    }
}
