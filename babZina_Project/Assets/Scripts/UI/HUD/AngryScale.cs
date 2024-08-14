//this empty line for UTF-8 BOM header
using UnityEngine;

public class AngryScale : MonoBehaviour
{
    [SerializeField] Bar bar;
    [SerializeField] UIManager uiManager;

    IAngryScaleManager angryScaleManager;

    private void Awake()
    {
        uiManager.OnInited += OnInited;
    }

    private void OnDestroy()
    {
        uiManager.OnInited -= OnInited;
    }

    private void Start()
    {
        OnInited();
    }

    private void OnInited()
    {
        if (angryScaleManager != null)
        {
            angryScaleManager.Progress.OnValueChanged -= OnProgressChanged;
        }

        this.angryScaleManager = uiManager.AngryScaleManagerProvider;

        angryScaleManager.Progress.OnValueChanged += OnProgressChanged;
    }

    private void OnProgressChanged(int progressPercents)
    {
        float progress = (float)progressPercents / 100f;

        bar.SetProgress(progress);
    }
}
