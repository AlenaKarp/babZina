//this empty line for UTF-8 BOM header
using UnityEngine;
using UnityTools.Runtime.Promises;

public class ActivatorByTimer : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private bool activate;
    [SerializeField] private float seconds;

    IPromise timerWait;

    private void Start()
    {
        IPromise timerWait = UnityTools.UnityRuntime.Timers.Timer.Instance.WaitUnscaled(seconds);
        timerWait.Done(() => 
        {
            if(timerWait != null)
            {
                target.SetActive(activate);
                timerWait = null;
            }
        });
    }

    private void OnDestroy()
    {
        timerWait = null;
    }
}
