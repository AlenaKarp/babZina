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

    private void Awake()
    {
        addPointsDeferred = Deferred.GetFromPool();

        Timer.Instance.WaitUnscaled(secondsWithAddPoints)
            .Done(TryAddPoints);
    }

    protected override void ProcessDone()
    {
        base.ProcessDone();

        canAddPoints = true;
    }

    private void TryAddPoints()
    {
        if(canAddPoints)
        {
            addPointsDeferred.Resolve();
        }
        else
        {
            addPointsDeferred.Reject(new System.Exception());
        }
    }
}
