//this empty line for UTF-8 BOM header
using System;
using UnityTools.Runtime.StatefulEvent;

public interface IAngryScaleManager
{
    event Action OnSuccessTrick;
    event Action OnFailTrick;
    event Action OnCloseTrick;

    IStatefulEvent<int> Progress { get; }

    void LosePoints(PlayerPhysics.TricksType type);
    void AddPoints();
    void ReduceTrickCountToWin();
}
