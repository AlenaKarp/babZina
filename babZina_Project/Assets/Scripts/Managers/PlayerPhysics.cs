//this empty line for UTF-8 BOM header
using System;
using UnityEngine;
using UnityTools.Runtime.Promises;
using UnityTools.Runtime.StatefulEvent;
using UnityTools.UnityRuntime.Timers;

public class PlayerPhysics : MonoBehaviour, IPlayerPhysics
{
    [Serializable]
    public enum TricksType
    {
        None = 0,
        Permanent = 1,
        Timing = 2,
        DoubleTiming = 3
    }

    public enum State
    {
        None = 0,
        Interact = 1,
        FailInteract = 2,
        Walking = 3,
        SuccessInteract = 4,
    }

    [SerializeField] private float speed;

    public Vector3 PlayerPosition => transform.position;
    public IStatefulEvent<bool> IsMovingForward => isMovingForward;
    public IStatefulEvent<State> CurrentState => currentState;

    private const float distanceAccuracy = 0.7f;
    private const float clickAccuracy = 0.7f;

    private Vector3? pointToGo;
    private IControlsManager controls;
    private StatefulEventInt<State> currentState = StatefulEventInt.CreateEnum(State.None);
    private StatefulEventInt<bool> isMovingForward = StatefulEventInt.Create(true);
    private IInteractiveObject currentIntractiveObject;
    private IAngryScaleManager angryScaleManager;

    internal void Init(IControlsManager controlsManager, IAngryScaleManager angryScaleManager)
    {
        if (controls != null)
        {
            controls.OnNewClickPosition -= ChangePointToGo;
            controls.OnInteractiveObjectClick -= OnInteract;
            controls.OnReset -= OnReset;
        }

        this.controls = controlsManager;

        controls.OnNewClickPosition += ChangePointToGo;
        controls.OnInteractiveObjectClick += OnInteract;
        controls.OnReset += OnReset;

        this.angryScaleManager = angryScaleManager;
    }

    private void FixedUpdate()
    {
        if (pointToGo.HasValue)
        {
            currentState.Set(State.Walking);

            transform.position = Vector3.MoveTowards(transform.position, pointToGo.Value, speed);

            if (Mathf.Abs(transform.position.x - pointToGo.Value.x) < distanceAccuracy)
            {
                pointToGo = null;

                currentState.Set(State.None);
            }
        }
    }

    private void ChangePointToGo(Vector3 position)
    {
        CancelInteraction();

        Vector3 newPointToGo = transform.position;
        newPointToGo.x = position.x;

        float delta = newPointToGo.x - transform.position.x;
        float movingSign = Mathf.Sign(delta);

        if (Mathf.Abs(delta) > clickAccuracy)
        {
            isMovingForward.Set(movingSign > 0);
            pointToGo = newPointToGo;
        }
    }

    private void OnReset()
    {
        pointToGo = null;
        currentState.Set(State.None);
    }

    private void OnInteract(Collider collider, Vector3 position)
    {
        if (collider.TryGetComponent<IInteractiveObject>(out IInteractiveObject interactiveObject) == false)
        {
            return;
        }

        if (interactiveObject.CurrentState.Value != IInteractiveObject.State.CanInteract)
        {
            return;
        }

        if (currentState.Value == State.Interact)
        {
            return;
        }

        pointToGo = null;

        currentIntractiveObject = interactiveObject;
        currentIntractiveObject.OnGameOver += OnGameOver;

        if (interactiveObject.TryInteract() == false)
        {
            CancelInteraction();
            return;
        }

        float movingSign = Mathf.Sign(position.x - transform.position.x);
        isMovingForward.Set(movingSign > 0);

        currentState.Set(State.Interact);

        if(interactiveObject is TimingInteractiveObject timingInteractiveObject)
        {
            Timer.Instance
            .WaitUnscaled(currentIntractiveObject.SecondsToInteract)
            .Then(() =>
            {
                SuccessInteract();
                return timingInteractiveObject.AddPointsPromise;
            })
            .Fail((exc) => SetTrickUnavailable())
            .Done(() => LosePoints(timingInteractiveObject.TrickType));
        }
        else
        {
            Timer.Instance
            .WaitUnscaled(currentIntractiveObject.SecondsToInteract)
            .Done(SuccessInteractWithLosePoints);
        }
    }

    private void OnGameOver()
    {
        currentState.Set(State.FailInteract);

        angryScaleManager.AddPoints();
    }

    private void SuccessInteractWithLosePoints()
    {
        LosePoints(currentIntractiveObject.TrickType);
        
        SuccessInteract();
    }

    private void SuccessInteract()
    {
        if (currentState.Value != State.Interact)
        {
            return;
        }

        currentState.Set(State.SuccessInteract);
        CancelInteraction();
    }

    private void CancelInteraction()
    {
        if (currentIntractiveObject != null)
        {
            currentIntractiveObject.OnGameOver -= OnGameOver;
            currentIntractiveObject = null;
        }
    }

    private void LosePoints(TricksType type)
    {
        angryScaleManager.LosePoints(type);
    }

    private void SetTrickUnavailable()
    {
        angryScaleManager.ReduceTrickCountToWin();
    }
}
