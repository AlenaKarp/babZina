//this empty line for UTF-8 BOM header
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityTools.Runtime.StatefulEvent;
using UnityTools.UnityRuntime.Timers;
using static PlayerPhysics;

public class InteractiveObject : MonoBehaviour, IInteractiveObject
{
    public event Action OnGameOver = () => {};
    public float SecondsToInteract => secondsToInteract;
    public IStatefulEvent<IInteractiveObject.State> CurrentState => currentState;
    public TricksType TrickType => trickType;

    [SerializeField] private PlayerPhysics playerPhysics;
    [SerializeField] private List<Observer> observers;
    [SerializeField] private float secondsToInteract;
    [SerializeField] private GameObject standartView;
    [SerializeField] private GameObject afterInteractView;
    [SerializeField] private List<GameObject> progressVFXs;
    [SerializeField] private TriggerInteractiveObject tooltip;
    [SerializeField] private TricksType trickType;

    private StatefulEventInt<IInteractiveObject.State> currentState = StatefulEventInt.CreateEnum(IInteractiveObject.State.None);
    private bool canInteract = false;

    private void Awake()
    {
        foreach (Observer observer in observers)
        {
            observer.IsDeadState.OnValueChanged += OnObserverDeadStateChanged;
        }

        currentState.OnValueChanged += OnCurrentStateValueChanged;
        playerPhysics.CurrentState.OnValueChanged += OnPlayerStateChanged;
    }

    private void OnDestroy()
    {
        foreach (Observer observer in observers)
        {
            observer.IsDeadState.OnValueChanged -= OnObserverDeadStateChanged;
        }

        currentState.OnValueChanged -= OnCurrentStateValueChanged;
        playerPhysics.CurrentState.OnValueChanged -= OnPlayerStateChanged;
    }

    private void OnPlayerStateChanged(State state)
    {
        if (currentState.Value == IInteractiveObject.State.InProcess
            && state == State.Walking)
        {
            StopStateInteract();
        }
    }

    private void StopStateInteract()
    {
        currentState.Set(canInteract ? IInteractiveObject.State.CanInteract : IInteractiveObject.State.None);
    }

    private void OnCurrentStateValueChanged(IInteractiveObject.State state)
    {
        foreach (GameObject go in progressVFXs)
        {
            go.SetActive(state == IInteractiveObject.State.InProcess);
        }
    }

    private void OnObserverDeadStateChanged(bool dead)
    {
        if (dead == true)
        {
            switch (currentState.Value)
            {
                case IInteractiveObject.State.InProcess:
                    OnGameOver();
                    currentState.Set(IInteractiveObject.State.CanInteract);
                    break;

                case IInteractiveObject.State.AfterInteract:
                    foreach (Observer observer in observers)
                    {
                        observer.SetDissapointState();
                    }
                    break;
            }
        }
    }

    public bool TryInteract()
    {
        if (currentState.Value != IInteractiveObject.State.CanInteract)
        {
            return false;
        }

        if (HasObserversWithDeadState() == true)
        {
            OnGameOver();

            return false;
        }

        currentState.Set(IInteractiveObject.State.InProcess);

        tooltip.Activate(false);

        Timer.Instance.WaitUnscaled(secondsToInteract)
            .Done(ProcessDone);

        return true;
    }

    private bool HasObserversWithDeadState()
    {
        foreach (Observer observer in observers)
        {
            if (observer.IsDeadState.Value == true)
            {
                return true;
            }
        }

        return false;
    }

    private void ProcessDone()
    {
        if (currentState.Value != IInteractiveObject.State.InProcess)
        {
            return;
        }

        currentState.Set(IInteractiveObject.State.AfterInteract);
        tooltip.Deactivate();

        standartView.SetActive(false);
        afterInteractView.SetActive(true);
    }

    internal void SetCanInteractState(bool value)
    {
        if (currentState.Value == IInteractiveObject.State.AfterInteract)
        {
            return;
        }

        canInteract = value;

        if (value == false)
        {
            currentState.Set(IInteractiveObject.State.None);
            return;
        }

        if (currentState.Value == IInteractiveObject.State.None)
        {
            currentState.Set(IInteractiveObject.State.CanInteract);
        }
    }
}
