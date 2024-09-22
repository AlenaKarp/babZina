//this empty line for UTF-8 BOM header
using UnityEngine;
using UnityTools.UnityRuntime.Timers;

public class ActivatorByTimer : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private bool activate;
    [SerializeField] private float seconds;

    private void Start()
    {
        Timer.Instance.WaitUnscaled(seconds)
            .Done(() =>
            {
                target.SetActive(activate);
            });
    }
}
