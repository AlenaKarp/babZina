//this empty line for UTF-8 BOM header
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractionProgressBar : MonoBehaviour
{
    [SerializeField] private Image sliderImage;
    [SerializeField] private InteractiveObject interactiveObject;
    [SerializeField] private GameObject slider;

    private float? startInteractTime = null;

    private void Awake()
    {
        interactiveObject.CurrentState.OnValueChanged += OnInterctiveObjectStateChanged;
        slider.SetActive(false);
    }

    private void OnDestroy()
    {
        interactiveObject.CurrentState.OnValueChanged -= OnInterctiveObjectStateChanged;
    }

    private void FixedUpdate()
    {
        if (startInteractTime.HasValue == false)
        {
            return;
        }

        float timeDelta = Time.time - startInteractTime.Value;
        sliderImage.fillAmount = (float)timeDelta / interactiveObject.SecondsToInteract;
    }

    private void OnInterctiveObjectStateChanged(IInteractiveObject.State state)
    {
        slider.SetActive(state == IInteractiveObject.State.InProcess);

        switch (state)
        {
            case IInteractiveObject.State.InProcess:
                startInteractTime = Time.time;
                break;

            case IInteractiveObject.State.AfterInteract:
                this.gameObject.SetActive(false);
                break;

            default:
                startInteractTime = null;
                break;
        }
    }
}
