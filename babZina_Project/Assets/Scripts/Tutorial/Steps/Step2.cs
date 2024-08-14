//this empty line for UTF-8 BOM header
using UnityEngine;

public class Step2 : TutorialStep
{
    [SerializeField] private InteractiveObject interactiveObject;

    private void Awake()
    {
        interactiveObject.CurrentState.OnValueChanged += OnInteractiveObjectValueChanged;
    }

    private void OnDestroy()
    {
        interactiveObject.CurrentState.OnValueChanged -= OnInteractiveObjectValueChanged;
    }

    private void OnInteractiveObjectValueChanged(IInteractiveObject.State state)
    {
        if (state == IInteractiveObject.State.AfterInteract)
        {
            FinishStep();
        }
    }
}
