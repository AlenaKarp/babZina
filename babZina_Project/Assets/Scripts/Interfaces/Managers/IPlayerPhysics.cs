//this empty line for UTF-8 BOM header
using UnityEngine;
using UnityTools.Runtime.StatefulEvent;
using static PlayerPhysics;

public interface IPlayerPhysics
{
    Vector3 PlayerPosition { get; }
    IStatefulEvent<bool> IsMovingForward { get; }
    IStatefulEvent<State> CurrentState { get; }
}
