//this empty line for UTF-8 BOM header
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickRegistrator : MonoBehaviour
{
    public event Action<Vector3> OnNewClickPosition = (position) => { };
    public event Action<Collider, Vector3> OnInteractiveObjectClick = (collider, position) => { };

    [SerializeField] private InputAction inputAction;
    [SerializeField] private LayerMask movingClickCatcherMask;
    [SerializeField] private LayerMask interactClickCatcherMask;

    private const float raycastDistance = 30f;
    private ICameraManager cameraManager;

    internal void Init(ICameraManager cameraManager)
    {
        this.cameraManager = cameraManager;
    }

    private void OnEnable()
    {
        inputAction.Enable();
        inputAction.performed += OnActionPerformed;
    }

    private void OnDisable()
    {
        inputAction.performed -= OnActionPerformed;
        inputAction.Disable();
    }

    private void OnActionPerformed(InputAction.CallbackContext context)
    {
        Ray ray = cameraManager.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance, movingClickCatcherMask))
        {
            OnNewClickPosition(hit.point);
        }

        if (Physics.Raycast(ray, out hit, raycastDistance, interactClickCatcherMask))
        {
            OnInteractiveObjectClick(hit.collider, hit.point);
        }
    }
}
