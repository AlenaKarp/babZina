//this empty line for UTF-8 BOM header
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TriggerInteractiveObject : MonoBehaviour
{
    [SerializeField] private InteractiveObject interactiveObject;
    [SerializeField] private List<GameObject> appearingObjects;
    [SerializeField] private List<GameObject> disappearingObjects;
    [SerializeField] private bool activeByDefault;

    private bool isActive = true;
    private bool isTrigger = false;

    private void Awake()
    {
        interactiveObject.CurrentState.OnValueChanged += OnStateValueChanged;

        Activate(activeByDefault);
    }

    private void OnDestroy()
    {
        interactiveObject.CurrentState.OnValueChanged -= OnStateValueChanged;
    }

    private void OnStateValueChanged(IInteractiveObject.State state)
    {
        switch (state)
        {
            case IInteractiveObject.State.CanInteract:
                if (isTrigger)
                {
                    Activate(true);
                }
                break;

            case IInteractiveObject.State.InProcess:
                Activate(false);
                break;

            case IInteractiveObject.State.AfterInteract:
                Deactivate();
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTrigger = true;
            Activate(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTrigger = false;
            Activate(false);
        }
    }

    internal void Activate(bool value)
    {
        if (isActive == false)
        {
            return;
        }

        foreach (GameObject go in appearingObjects)
        {
            go.SetActive(value);
        }

        foreach (GameObject go in disappearingObjects)
        {
            go.SetActive(value == false);
        }
    }

    internal void Deactivate()
    {
        isActive = false;

        Activate(false);
    }
}
