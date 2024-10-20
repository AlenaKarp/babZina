using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitcherAfterInteraction : MonoBehaviour
{
    [SerializeField] private InteractiveObject obj;

    private void Awake()
    {
        obj.CurrentState.OnValueChanged += OnValueChanged;
    }

    private void OnDestroy()
    {
        obj.CurrentState.OnValueChanged -= OnValueChanged;
    }

    private void OnValueChanged(IInteractiveObject.State state)
    {
        if (state == IInteractiveObject.State.AfterInteract)
        {
            gameObject.SetActive(false);
        }
    }
}
