//this empty line for UTF-8 BOM header
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CanInteractSetter : MonoBehaviour
{
    [SerializeField] private InteractiveObject interactiveObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactiveObject.SetCanInteractState(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactiveObject.SetCanInteractState(false);
        }
    }
}
