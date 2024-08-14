//this empty line for UTF-8 BOM header
using UnityEngine;
using UnityEngine.InputSystem;

public class PointerRegistrator : MonoBehaviour
{
    [SerializeField] private LayerMask pointerCatcherMask;

    private const float raycastDistance = 30f;

    private ICameraManager cameraManager;
    private PointerInteractiveObject currentPointerInteractiveObject = null;

    internal void Init(ICameraManager cameraManager)
    {
        this.cameraManager = cameraManager;
    }

    private void Update()
    {
        if (cameraManager == null)
        {
            return;
        }

        Ray ray = cameraManager.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance, pointerCatcherMask))
        {
            if (hit.collider.TryGetComponent<PointerInteractiveObject>(out PointerInteractiveObject pointerInteractiveObject) == true)
            {
                if (currentPointerInteractiveObject != null
                    &&
                    pointerInteractiveObject.GetHashCode() == currentPointerInteractiveObject.GetHashCode())
                {
                    return;
                }

                SetNewInteractiveObject(pointerInteractiveObject);
                return;
            }
        }

        if (currentPointerInteractiveObject != null)
        {
            currentPointerInteractiveObject.SetInteractState(false);
        }

        currentPointerInteractiveObject = null;
    }

    private void SetNewInteractiveObject(PointerInteractiveObject newPointerInteractiveObject)
    {
        if (currentPointerInteractiveObject != null)
        {
            currentPointerInteractiveObject.SetInteractState(false);
        }

        currentPointerInteractiveObject = newPointerInteractiveObject;
        newPointerInteractiveObject.SetInteractState(true);
    }
}
