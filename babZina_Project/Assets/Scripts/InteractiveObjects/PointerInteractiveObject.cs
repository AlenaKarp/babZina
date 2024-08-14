//this empty line for UTF-8 BOM header
using System.Collections.Generic;
using UnityEngine;

public class PointerInteractiveObject : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToShow;
    [SerializeField] private InteractiveObject interactiveObject;

    private bool lastVisibility = false;

    private void Awake()
    {
        interactiveObject.CurrentState.OnValueChanged += OnStateValueChanged;
    }

    private void OnDestroy()
    {
        interactiveObject.CurrentState.OnValueChanged -= OnStateValueChanged;
    }

    private void OnStateValueChanged(IInteractiveObject.State state)
    {
        if (state == IInteractiveObject.State.AfterInteract)
        {
            interactiveObject.CurrentState.OnValueChanged -= OnStateValueChanged;
        }

        if (state is IInteractiveObject.State.CanInteract or IInteractiveObject.State.None)
        {
            SetInteractState(lastVisibility);
        }
        else
        {
            SetInteractState(false);
        }
    }

    internal void SetInteractState(bool value)
    {
        if (interactiveObject.CurrentState.Value == IInteractiveObject.State.AfterInteract)
        {
            foreach (GameObject go in objectsToShow)
            {
                go.SetActive(false);
            }

            return;
        }

        foreach (GameObject go in objectsToShow)
        {
            go.SetActive(value);
        }

        lastVisibility = value;
    }
}
