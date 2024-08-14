//this empty line for UTF-8 BOM header
using System;
using UnityTools.Runtime.StatefulEvent;

public interface IAngryScaleManager
{
    event Action OnSuccessTrick;
    event Action OnFailTrick;

    IStatefulEvent<int> Progress { get; }

    void AddPoints(PlayerPhysics.TricksType type);
    void DeprivePoints();
}
