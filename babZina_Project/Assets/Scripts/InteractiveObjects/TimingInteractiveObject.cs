//this empty line for UTF-8 BOM header
using UnityEngine;
using UnityTools.Runtime.Promises;
using UnityTools.UnityRuntime.Timers;

public class TimingInteractiveObject : InteractiveObject
{
    public IPromise AddPointsPromise => addPointsDeferred;

    [SerializeField] private float secondsWithAddPoints;

    private Deferred addPointsDeferred;
    private bool canAddPoints = false;

    protected override void Awake()
    {
        base.Awake();

        addPointsDeferred = Deferred.GetFromPool();

        Timer.Instance.WaitUnscaled(secondsWithAddPoints)
            .Done(TryAddPoints);
    }

    protected override void ProcessDone()
    {
        if (CurrentState.Value != IInteractiveObject.State.InProcess)
        {
            return;
        }

        base.ProcessDone();

        canAddPoints = true;
    }

    private void TryAddPoints()
    {
        if(canAddPoints)
        {
            addPointsDeferred.Resolve();

            foreach(Observer observer in observers)
            {
                observer.SetDissapointState();
            }
        }
        else
        {
            addPointsDeferred.Reject(new System.Exception());
        }
    }
}
