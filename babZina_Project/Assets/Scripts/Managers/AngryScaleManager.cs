//this empty line for UTF-8 BOM header
using System;
using UnityEngine;
using UnityTools.Runtime.StatefulEvent;

public class AngryScaleManager : MonoBehaviour, IAngryScaleManager
{
    public event Action OnSuccessTrick = () => { };
    public event Action OnFailTrick = () => { };
    public IStatefulEvent<int> Progress => currentProgress;

    [SerializeField] private AngryScaleConfig config;
    [SerializeField] private bool useOnThisLevel = true;

    StatefulEventInt<int> timer = StatefulEventInt.Create(0);
    StatefulEventInt<int> currentProgress = StatefulEventInt.Create(70);

    private void OnEnable()
    {
        if (useOnThisLevel == false)
        {
            return;
        }

        timer.OnValueChanged += OnTimer;

        currentProgress.Set(config.startValue);
    }

    private void OnDisable()
    {
        timer.OnValueChanged -= OnTimer;
    }

    private void Update()
    {
        timer.Set((int)Time.time);
    }

    private void OnTimer(int secondsFromStart)
    {
        int newValue = currentProgress.Value + config.pointDeltaInSecond; 

        currentProgress.Set(Math.Clamp(newValue, 0, 100));
    }

    public void LosePoints(PlayerPhysics.TricksType type)
    {
        int progress = currentProgress.Value - GetScoreByType(type);

        currentProgress.Set(Math.Clamp(progress, 0, 100));

        OnSuccessTrick();
    }

    public void AddPoints()
    {
        int progress = currentProgress.Value + config.trickFailScore;

        currentProgress.Set(Math.Clamp(progress, 0, 100));

        OnFailTrick();
    }

    private int GetScoreByType(PlayerPhysics.TricksType type)
    {
        foreach (AngryScaleConfig.TrickScore trickScore in config.tricksScore)
        {
            if (trickScore.type == type)
            {
                return trickScore.score;
            }
        }

        throw new Exception("Wrong trick score data!");
    }
}
