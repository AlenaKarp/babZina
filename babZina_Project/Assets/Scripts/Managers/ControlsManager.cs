//this empty line for UTF-8 BOM header
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlsManager : MonoBehaviour, IControlsManager
{
    public event Action<Vector3> OnNewClickPosition = (position) => { };
    public event Action<Collider, Vector3> OnInteractiveObjectClick = (collider, position) => { };

    [SerializeField] private ClickRegistrator clickRegistrator;
    [SerializeField] private PointerRegistrator pointerRegistrator;

    private IUIManager uiManager;

    private void OnEnable()
    {
        clickRegistrator.OnInteractiveObjectClick += OnInteractiveObjectClickAction;
        clickRegistrator.OnNewClickPosition += OnNewClickPositionAction;
    }

    private void OnDisable()
    {
        clickRegistrator.OnInteractiveObjectClick -= OnInteractiveObjectClickAction;
        clickRegistrator.OnNewClickPosition -= OnNewClickPositionAction;
    }

    private void OnNewClickPositionAction(Vector3 position) => OnNewClickPosition(position);

    private void OnInteractiveObjectClickAction(Collider collider, Vector3 position) => OnInteractiveObjectClick(collider, position);

    internal void Init(ICameraManager cameraManager, IUIManager uiManager)
    {
        clickRegistrator.Init(cameraManager);
        pointerRegistrator.Init(cameraManager);

        if (this.uiManager != null)
        {
            this.uiManager.OnStackChanged -= OnStackChanged;
        }

        this.uiManager = uiManager;
        this.uiManager.OnStackChanged += OnStackChanged;
    }

    private void OnStackChanged()
    {
        clickRegistrator.gameObject.SetActive(clickRegistrator.gameObject.activeInHierarchy == false);
    }
}
