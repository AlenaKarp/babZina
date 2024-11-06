//this empty line for UTF-8 BOM header
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickRegistrator : MonoBehaviour
{
    public event Action<Vector3> OnNewClickPosition = (position) => { };
    public event Action<Collider, Vector3> OnInteractiveObjectClick = (collider, position) => { };

    [SerializeField] private InputAction movingInputAction;
    [SerializeField] private InputAction interactionInputAction;
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
        movingInputAction.Enable();
        interactionInputAction.Enable();

        interactionInputAction.performed += OnInteractionPerformed;
    }

    private void OnDisable()
    {
        interactionInputAction.performed -= OnInteractionPerformed;

        movingInputAction.Disable();
        interactionInputAction.Disable();
    }

    private void Update()
    {
        if(movingInputAction.ReadValue<float>() > 0)
        {
            SetNewClickPosition();
        }
    }

    private void OnInteractionPerformed(InputAction.CallbackContext context)
    {
        if (TryRaycastFromMouse(interactClickCatcherMask, out RaycastHit hit))
        {
            OnInteractiveObjectClick(hit.collider, hit.point);
        }
    }

    private void SetNewClickPosition()
    {
        if (TryRaycastFromMouse(movingClickCatcherMask, out RaycastHit hit))
        {
            OnNewClickPosition(hit.point);
        }
    }

    private bool TryRaycastFromMouse(LayerMask mask, out RaycastHit raycastHit)
    {
        Ray ray = cameraManager.ScreenPointToRay(Mouse.current.position.ReadValue());

        return Physics.Raycast(ray, out raycastHit, raycastDistance, mask);
    }
}
