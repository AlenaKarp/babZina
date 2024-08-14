//this empty line for UTF-8 BOM header
using System;
using UnityTools.Runtime.StatefulEvent;
using static PlayerPhysics;

public interface IInteractiveObject
{
    [Serializable]
    public enum State : byte
    {
        None = 0,
        CanInteract = 1,
        InProcess = 2,
        AfterInteract = 3,
    }

    TricksType TrickType { get; }

    event Action OnGameOver;
    float SecondsToInteract { get; }
    IStatefulEvent<State> CurrentState { get; }

    bool TryInteract();
}
